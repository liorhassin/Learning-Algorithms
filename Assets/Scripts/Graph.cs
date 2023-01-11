using System.Collections.Generic;
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
    private void GenerateRandomGraph(string chosenGraph, int numberOfNodes,
        int maximumNumberOfEdges, bool canIncludeSubGraphs, GameObject node, GameObject edge)
    {
        if (!_isDirected) //Change prefab to undirected.
        {
            
        }
        /*
         * Here comes the code to randomly generate the graph it self(Edges and Nodes)
         */
        if (_isWeighted) //Generate weight for edges.
        {
            
        }
    }
    
    /**
     * Method used to build the nodes of the graph.
	 */
    private Node CreateNode()
    {
	    GameObject tempNode = UnityEngine.Object.Instantiate(_nodePrefab,
		    new Vector3(Random.Range(-_spawnSize/2, _spawnSize/2), 
			                   Random.Range(-_spawnSize/2, _spawnSize/2), 
			                    Random.Range(-_spawnSize/2, _spawnSize/2)), 
									Quaternion.identity);
	    tempNode.transform.parent = _graphSpawnPoint.transform;
	    tempNode.name = "Node " + _currentLetter;
	    _currentLetter++;
	    return tempNode.GetComponent<Node>();
    }
    
    private void AddNode(Node node)
	{
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
    
    public void InitializeAlgorithm(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.InitializeAlgorithm(this);
    }

    public void FinishAlgorithm(IGraphAlgorithms graphAlgorithms)
    {
	    graphAlgorithms.FinishAlgorithm();
    }

    public void NextStepAlgorithm(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.NextStep();
    }

    public void PrevStepAlgorithm(IGraphAlgorithms graphAlgorithm)
    {
	    graphAlgorithm.PrevStep();
    }
}
