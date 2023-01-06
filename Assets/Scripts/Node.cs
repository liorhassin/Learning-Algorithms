using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

  private GameObject _edgePreFab;
  private int Pi;
  private List<GameObject>  _edges  = new List<GameObject> ();
  private List<SpringJoint> _joints = new List<SpringJoint>();  
  
  void Start()
  {
    Pi = 0;
    transform.GetChild(0).GetComponent<TextMesh>().text = name;
  }
  
  void Update()
  {
    Vector3 currentVec3 = this.transform.position;
    int i = 0;
    
    //Iterate through all the edges and make sure they stretch to the correct position.
    foreach (GameObject edge in _edges)
    {
      Transform currentEdgeTransform = edge.transform; //Refer to edge current transform(pos)
      currentEdgeTransform.position = new Vector3(currentVec3.x, currentVec3.y, currentVec3.z); //Change position of edge
      SpringJoint sj = _joints[i]; //Get the spring joint
      GameObject target = sj.connectedBody.gameObject; //Get the target of the spring joint
      
      Transform targetTransform = target.transform; //Get the transform of the target
      currentEdgeTransform.LookAt(target.transform); //Rotate edge to look at target
      Vector3 ls = currentEdgeTransform.localScale; //Get the local scale of the edge
      ls.z = Vector3.Distance(currentVec3, target.transform.position); //Set the z scale to the distance between the two nodes
      currentEdgeTransform.localScale = ls; //Set the local scale of the edge

      Vector3 targetVec3Pos = targetTransform.position; //Get the position of the target
      edge.transform.position = new Vector3((currentVec3.x+targetVec3Pos.x)/2,
					    (currentVec3.y+targetVec3Pos.y)/2,
					    (currentVec3.z+targetVec3Pos.z)/2); //Set the position of the edge to the middle of the two nodes
      i++;
    }
  }

  public void SetEdgePrefab(GameObject edgePreFab){
    this._edgePreFab = edgePreFab;
  }
  
  public void AddEdge(Node n){
    SpringJoint sj = gameObject.AddComponent<SpringJoint> ();  
    sj.autoConfigureConnectedAnchor = false;
    sj.anchor = new Vector3(0,0.5f,0);
    sj.connectedAnchor = new Vector3(0,0,0);    
    sj.enableCollision = true;
    sj.connectedBody = n.GetComponent<Rigidbody>();
    GameObject edge = Instantiate(this._edgePreFab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    _edges.Add(edge);
    _joints.Add(sj);
  }
    
}
