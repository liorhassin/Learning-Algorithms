using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
  private GameObject _currentNodePrefab;
  private List<Edge> _edges = new List<Edge> ();
  private int _pi;
  private int _colorStatus = 0;

  private void Start()
  {
    transform.GetChild(0).GetComponent<TextMesh>().text = name;
  }
  
  private void Update()
  {
    Vector3 currentVec3 = this.transform.position;

    //Iterate through all the edges and make sure they stretch to the correct position.
    foreach (Edge edge in _edges)
    {
      GameObject edgeObject = edge.GetEdgeObject();
      Transform currentEdgeTransform = edgeObject.transform; //Refer to edge current transform(pos)
      currentEdgeTransform.position = new Vector3(currentVec3.x, currentVec3.y, currentVec3.z); //Change position of edge
      
      SpringJoint sj = edge.GetSpringJoint(); //Get the spring joint
      GameObject target = sj.connectedBody.gameObject; //Get the target of the spring joint
      
      Transform targetTransform = target.transform; //Get the transform of the target
      currentEdgeTransform.LookAt(target.transform); //Rotate edge to look at target
      
      Vector3 localScale = currentEdgeTransform.localScale; //Get the local scale of the edge
      localScale.z = Vector3.Distance(currentVec3, target.transform.position); //Set the z scale to the distance between the two nodes
      currentEdgeTransform.localScale = localScale; //Set the local scale of the edge

      Vector3 targetVec3Pos = targetTransform.position; //Get the position of the target
      edgeObject.transform.position = new Vector3((currentVec3.x+targetVec3Pos.x)/2,
        (currentVec3.y+targetVec3Pos.y)/2,
        (currentVec3.z+targetVec3Pos.z)/2); //Set the position of the edge to the middle of the two nodes
    }
  }
  
  public void AddEdge(Edge newEdge){
    _edges.Add(newEdge);
  }

  public void SetNodeMaterialColor(int status)
  {
    Material renderer = GetComponent<Renderer>().material;
    switch(status)
    {
      case 0:
        renderer.color = Color.white;
        renderer.SetColor("_EmissionColor", Color.white);
        break;
      case 1:
        renderer.color = Color.yellow;
        renderer.SetColor("_EmissionColor", Color.yellow);
        break;
      case 2:
        renderer.color = Color.green;
        renderer.SetColor("_EmissionColor", Color.green);
        break;
    }
    _colorStatus = status;
  }

  public int GetPi()
  {
    return _pi;
  }

  public void SetPi(int pi)
  {
    _pi = pi;
  }

  public List<Edge> GetEdges()
  {
    return _edges;
  }
}
