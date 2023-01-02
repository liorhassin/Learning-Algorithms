using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

  private GameObject _edgePreFab;

  private List<GameObject>  _edges  = new List<GameObject> ();
  private List<SpringJoint> _joints = new List<SpringJoint>();  
  
  void Start(){
    transform.GetChild(0).GetComponent<TextMesh>().text = name;
  }
  
  void Update(){    
    int i = 0;
    foreach (GameObject edge in _edges){
      edge.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
      SpringJoint sj = _joints[i];
      GameObject target = sj.connectedBody.gameObject;
      edge.transform.LookAt(target.transform);
      Vector3 ls = edge.transform.localScale;
      ls.z = Vector3.Distance(transform.position, target.transform.position);
      edge.transform.localScale = ls;
      edge.transform.position = new Vector3((transform.position.x+target.transform.position.x)/2,
					    (transform.position.y+target.transform.position.y)/2,
					    (transform.position.z+target.transform.position.z)/2);
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
