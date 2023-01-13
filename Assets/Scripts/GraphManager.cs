using UnityEngine;

public class GraphManager : MonoBehaviour
{
	private static Graph _graph;
	public TextAsset file; //Later change to json files.(or gml)
	public GameObject[] nodePreFab; //Filled with prefabs of possible nodes.
	public GameObject[] edgePreFab; //Filled with prefabs of possible edges.
	public Camera mainCamera; //Main camera presented to the player.
	private IGraphAlgorithms _graphAlgorithms;
	
	//User parameter for random graph generation:
	private int _highestDegree;
	private int _numOfNodes;
	private bool _isDirected;
	private bool _isWeighted;
	private int _nodePreFab;
	private int _edgePrefab;
	private int[] _colorStatus = new int[5];
	/**
	 * _colorStatus[i]:
	 * i=0 -> default node color.
	 * i=1 -> default edge color.
	 * i=2 -> visited node color.
	 * i=3 -> visited edge color.
	 * i=4 -> finished node color.
	 * i=5 -> finished edge color.
	 */

	private void Start()
	{
		_graph = new Graph(nodePreFab[0], edgePreFab[0], false, true, 6);
		_graph.GenerateDefaultGraph();
		_graph.InstantiateGraph();
		_graphAlgorithms = new BreadthFirstSearch();
		_graph.InitializeAlgorithm(_graphAlgorithms);
	}

	public void NextStepAlgorithm()
	{
		_graph.NextStepAlgorithm(_graphAlgorithms);
	}

	public void PrevStepAlgorithm()
	{
		_graph.PrevStepAlgorithm(_graphAlgorithms);
	}

	public void FinishAlgorithm()
	{
		_graph.FinishAlgorithm(_graphAlgorithms);
	}

	/**
	 * TODO - Generation of graph and initialization when chosen to
	 */
	public void GenerateGraph()
	{
		
	}

	public void InstantiateGraph()
	{
		
	}

	public void LoadDefaultPreset()
	{
		
	}

	public void LoadPresets(int index)
	{
		
	}
}
