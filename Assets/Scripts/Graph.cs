using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Graph
{
	private GameObject _graphSpawnPoint;
	private GameObject _nodePrefab;
    private GameObject _edgePrefab;
    
    private List<Node> _nodes; // For algorithms later on
    private List<Edge> _edges; // ..
    
    private char _currentLetter; // Starting letter for node names.
    private bool _isWeighted;
    private bool _isDirected;
    private int _numberOfNodes;
    private int _spawnSize;
 
    public Graph(GameObject nodePrefab, GameObject edgePrefab, bool isWeighted, bool isDirected, int numOfNodes){
        _nodePrefab = nodePrefab;
        _edgePrefab = edgePrefab;
        _currentLetter = 'A';
        _isWeighted = isWeighted;
        _isDirected = isDirected;
        _numberOfNodes = numOfNodes;
        _spawnSize = _numberOfNodes * 15;
        _nodes = new List<Node>();
        _edges = new List<Edge>();
        
        //Find spawn point for graph
        _graphSpawnPoint = GameObject.Find("GraphSpawnPoint");
    }
    

    /**
     * Method used to randomly generate a graph of size n(numberOfNodes)
     * Used helper methods BuildNodes, BuildEdges.
     */
    public void GenerateRandomGraph(string chosenGraph, int numberOfNodes,
        int maximumNumberOfEdges, bool canIncludeSubGraphs, GameObject node, GameObject edge)
    {
	    if (this._nodes.Any()) // If any node exists graph is already built.
	    {
		    return;
	    }

	    //First initialize all nodes for the graph.
	    _numberOfNodes = numberOfNodes;
	    for (int i = 0; i < _numberOfNodes; i++)
	    {
		    AddNode(CreateNode());
	    }
	    /*
	     * Both methods next will decide which prefab is being loaded and if we add weight to each edge or not.
	     */
	    if (!_isDirected) //Change prefab to undirected.
        {
            
        }
	    if (_isWeighted) //Generate weight for edges.
        {
            
        }
    }
    
    /**
     * Method used to build the nodes of the graph.
	 */
    private Node CreateNode()
    {
	    // GameObject tempNode = UnityEngine.Object.Instantiate(_nodePrefab,
		   //  new Vector3(Random.Range(-_spawnSize, _spawnSize), 
			  //                  Random.Range(-_spawnSize, _spawnSize), 
			  //                   Random.Range(-_spawnSize, _spawnSize)), 
					// 				Quaternion.identity);
	    GameObject tempNode = Object.Instantiate(_nodePrefab, Vector3.zero, Quaternion.identity);
	    tempNode.transform.parent = _graphSpawnPoint.transform;
	    tempNode.SetActive(false);
	    
	    //Temp option is to create sj to fix each node to the anchor.
	    //Another option is to fix just the first node to the anchor with fixed joint.
	    
	    // SpringJoint sj = tempNode.AddComponent<SpringJoint> ();
	    // sj.anchor = sj.transform.InverseTransformPoint(tempNode.transform.position);
	    // sj.autoConfigureConnectedAnchor = false;
	    // sj.enableCollision = true;
	    // sj.connectedBody = _graphSpawnPoint.GetComponent<Rigidbody>();
	    // sj.damper = 10;
	    // Vector3 toPosition = _graphSpawnPoint.transform.position;
	    // Object.Instantiate(_edgePrefab, new Vector3(toPosition.x, toPosition.y, toPosition.z), Quaternion.identity);

	    tempNode.name = _currentLetter.ToString();
	    _currentLetter++;
	    return tempNode.GetComponent<Node>();
    }
    
    private void AddNode(Node node)
	{
		node.gameObject.SetActive(true);
		_nodes.Add(node);
	}

    public Node GetNode(int index)
    {
	    return _nodes[index];
    }

    private Edge CreateEdge(Node from, Node to, int weight, GameObject edgePrefab)
	{
		Edge tempEdge = new Edge(from, to, weight, edgePrefab);
		from.AddEdge(tempEdge);
		return tempEdge;
	}

    private void AddEdge(Edge edge)
	{
	    _edges.Add(edge);
	}

    public void GenerateDefaultGraph()
    {
	    AddNode(CreateNode()); // A
	    AddNode(CreateNode()); // B
	    AddNode(CreateNode()); // C
	    AddNode(CreateNode()); // D
	    AddNode(CreateNode()); // E
	    AddNode(CreateNode()); // F
	    
	    AddEdge(CreateEdge(_nodes[0], _nodes[1], 7, _edgePrefab)); // A-B
	    AddEdge(CreateEdge(_nodes[0], _nodes[2], 9, _edgePrefab)); // A-C
	    AddEdge(CreateEdge(_nodes[2], _nodes[3], 14, _edgePrefab)); // C-D
	    AddEdge(CreateEdge(_nodes[3], _nodes[4], 2, _edgePrefab)); // D-E
	    AddEdge(CreateEdge(_nodes[4], _nodes[5], 9, _edgePrefab)); // E-F
	    AddEdge(CreateEdge(_nodes[5], _nodes[0], 10, _edgePrefab)); // F-A
	    AddEdge(CreateEdge(_nodes[1], _nodes[3], 15, _edgePrefab)); // B-D
	    AddEdge(CreateEdge(_nodes[3], _nodes[0], 41, _edgePrefab)); // D-A
    }
    
    public int GetNumberOfNodes()
	{
	    return _numberOfNodes;
	}
    
    /**
     * Initialize the required parameters for the chosen algorithm.
     */
    public void InitializeAlgorithm(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.InitializeAlgorithm(this);
    }

    /**
     * Initialzing all states of algorithm. 
     */
    public void PreCalculateAlgorithm(IGraphAlgorithms graphAlgorithms)
    {
	    graphAlgorithms.PreCalculateAlgorithm();
    }

    /**
     * Controlled by button -> moves 1 step ahead in algorithm.
     */
    public void LoadNextAlgorithmState(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.LoadNextAlgorithmState();
    }

    /**
     * Controlled by button -> moves 1 step back in algorithm.
     */
    public void LoadPrevAlgorithmState(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.LoadPrevAlgorithmState();
    }
    
    
    //TODO - Fix node spawn point to get better spawning result and good spacing

    /**
     * After all nodes and edges are created, Fully initialize graph(Spawn objects to visualize the graph in 3D).
     */
    public void InstantiateGraph()
    {
	    foreach (Node node in _nodes)
	    {
		    Vector3 spawnPoint;
		    //First node is getting connected into 0,0,0 spawning point to anchor entire graph
		    //Creating anchoring FixedJoint:
		    if (node == _nodes[0])
		    {
			    spawnPoint = Vector3.zero;
			    FixedJoint sj = node.AddComponent<FixedJoint>();
			    sj.anchor = sj.transform.InverseTransformPoint(node.transform.position);
			    sj.autoConfigureConnectedAnchor = false;
			    sj.enableCollision = true;
			    sj.connectedBody = _graphSpawnPoint.GetComponent<Rigidbody>();
			    Vector3 toPosition = _graphSpawnPoint.transform.position;
			    GameObject anchor = Object.Instantiate(_edgePrefab, new Vector3(toPosition.x, toPosition.y, toPosition.z), Quaternion.identity);
			    foreach (Transform t in anchor.transform)
			    {
				    if (t != null)
				    {
					    t.GetComponent<Renderer>().enabled = false;
				    }
			    }
		    }
		    else
		    {
			    spawnPoint = new Vector3(Random.Range(-_spawnSize/2 - 50, _spawnSize/2),
				    Random.Range(-_spawnSize/2, _spawnSize/2 + 50),
				    Random.Range(-_spawnSize/2 - 25, _spawnSize/2 + 25));
		    }
		    node.transform.position = spawnPoint;
		    node.gameObject.SetActive(true);
		    InstantiateEdges(node);
	    }
    }

    private void InstantiateEdges(Node node)
    {
	    foreach (Edge edge in node.GetEdges())
	    {
		    edge.GetEdgeObject().gameObject.SetActive(true);
	    }
    }
    
    //TODO - Maybe add after graph generation is looking good
    //TODO - Goal is to stop nodes from moving this method will be called after a few seconds.
    private void MakeGraphStatic()
    {
	    
    }
}
