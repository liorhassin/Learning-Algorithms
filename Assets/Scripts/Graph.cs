using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Graph : MonoBehaviour
{

  public TextAsset file;
  public GameObject nodePreFab;
  public GameObject edgePreFab; 
  public float width;
  public float length;
  public float height;
  
    private void Start()
    {
	    //Load default graph if none is specified.
		if (file==null){ 
			GenerateDefaultGraph();
		}
		else {
			LoadGmlFromFile(file);
	    }
    }
	
    private void GenerateDefaultGraph()
    {
	    GameObject A = Instantiate(nodePreFab, new Vector3(Random.Range(-width/2, width/2), Random.Range(-length/2, length/2), Random.Range(-height/2, height/2)), Quaternion.identity);
	    GameObject B = Instantiate(nodePreFab, new Vector3(Random.Range(-width/2, width/2), Random.Range(-length/2, length/2), Random.Range(-height/2, height/2)), Quaternion.identity);
	    GameObject C = Instantiate(nodePreFab, new Vector3(Random.Range(-width/2, width/2), Random.Range(-length/2, length/2), Random.Range(-height/2, height/2)), Quaternion.identity);
	    GameObject D = Instantiate(nodePreFab, new Vector3(Random.Range(-width/2, width/2), Random.Range(-length/2, length/2), Random.Range(-height/2, height/2)), Quaternion.identity);
	    GameObject E = Instantiate(nodePreFab, new Vector3(Random.Range(-width/2, width/2), Random.Range(-length/2, length/2), Random.Range(-height/2, height/2)), Quaternion.identity);      
	    
	    A.transform.parent = transform;
	    B.transform.parent = transform;
	    C.transform.parent = transform;
	    D.transform.parent = transform;
	    E.transform.parent = transform;
	    
	    //Update node name.
	    A.name = "Node A"; 
	    B.name = "Node B";
	    C.name = "Node C";
	    D.name = "Node D";
	    E.name = "Node E";
	    
	    Node nodeA = A.GetComponent<Node>();
	    Node nodeB = B.GetComponent<Node>();
	    Node nodeC = C.GetComponent<Node>();
	    Node nodeD = D.GetComponent<Node>();
	    Node nodeE = E.GetComponent<Node>();   
	    
	    //Connect nodes(Create new edges)
	    nodeA.SetEdgePrefab(edgePreFab);
	    nodeA.AddEdge(nodeB);
	    nodeA.AddEdge(nodeC);
	    nodeC.SetEdgePrefab(edgePreFab);
	    nodeC.AddEdge(nodeD);
	    nodeD.SetEdgePrefab(edgePreFab);
	    nodeD.AddEdge(nodeE);
	    nodeD.AddEdge(nodeA);
    }

    //Method taken from forums, not in used right now.
    private void LoadGmlFromFile(TextAsset f){ 
		string[] lines = f.text.Split('\n'); 
		int currentobject = -1; // 0 = graph, 1 = node, 2 = edge
		int stage = -1; // 0 waiting to open, 1 = waiting for attribute, 2 = waiting for id, 3 = waiting for label, 4 = waiting for source, 5 = waiting for target
	    Node n = null;
	    Dictionary<string,Node> nodes = new Dictionary<string,Node>();
	    foreach (string line in lines){
			string l = line.Trim();
			string [] words = l.Split(' ');
			foreach (string word in words)
			{
				if (word == "graph" && stage == -1)
				{
					currentobject = 0;
				}

				if (word == "node" && stage == -1)
				{
					currentobject = 1;
					stage = 0;
				}

				if (word == "edge" && stage == -1)
				{
					currentobject = 2;
					stage = 0;
				}

				if (word == "[" && stage == 0 && currentobject == 2)
				{
					stage = 1;
				}

				if (word == "[" && stage == 0 && currentobject == 1)
				{
					stage = 1;
					GameObject go = Instantiate(nodePreFab,
						new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-length / 2, length / 2),
							Random.Range(-height / 2, height / 2)), Quaternion.identity);
					n = go.GetComponent<Node>();
					n.transform.parent = transform;
					n.SetEdgePrefab(edgePreFab);
					continue;
				}

				if (word == "]")
				{
					stage = -1;
				}

				if (word == "id" && stage == 1 && currentobject == 1)
				{
					stage = 2;
					continue;
				}

				if (word == "label" && stage == 1 && currentobject == 1)
				{
					stage = 3;
					continue;
				}

				if (stage == 2)
				{
					nodes.Add(word, n);
					stage = 1;
					break;
				}

				if (stage == 3)
				{
					n.name = word;
					stage = 1;
					break;
				}

				if (word == "source" && stage == 1 && currentobject == 2)
				{
					stage = 4;
					continue;
				}

				if (word == "target" && stage == 1 && currentobject == 2)
				{
					stage = 5;
					continue;
				}

				if (stage == 4)
				{
					n = nodes[word];
					stage = 1;
					break;
				}

				if (stage == 5)
				{
					n.AddEdge(nodes[word]);
					stage = 1;
					break;
				}
			}
	    } 
    }
    
}
