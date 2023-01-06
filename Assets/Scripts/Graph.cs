using UnityEngine;

public class Graph : MonoBehaviour{
 
 
    private GameObject _nodePrefab;
    private GameObject _edgePrefab;
    private char _currentLetter;
    private bool _isWeighted;
    private bool _isDirected;
 
    public Graph(GameObject nodePrefab, GameObject edgePrefab, bool isWeighted, bool isDirected){
        _nodePrefab = nodePrefab;
        _edgePrefab = edgePrefab;
        _currentLetter = 'A';
        _isWeighted = isWeighted;
        _isDirected = isDirected;
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
     * Method used to create the number of nodes the player chose.
     */
    private void BuildNodes()
    {
        
    }
    
    /**
     * Method used to create the spring joints between the nodes.
     */
    private void BuildEdges()
    {
        
    }

    /**
     * Method used to update Node color(Currently checking, Checking neighbours, Finished with node).
     */
    private void SetNodeColor()
    {
        
    }

    /**
     * Method used to update Edge color(Currently checking, Checking neighbours, Finished with node).
     */
    private void SetEdgeColor()
    {
        
    }
}
