using UnityEngine;

public class GraphManager : MonoBehaviour
{
	private static Graph _graph;
	public TextAsset file; //Later change to json files.(or gml)
	public GameObject[] nodePreFab; //Filled with prefabs of possible nodes. will be changed to single element
	public GameObject[] edgePreFab; //Filled with prefabs of possible edges. will be changed to single element
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
		_graph = new Graph(nodePreFab[0], edgePreFab[4], false, true, 6);
		_graph.GenerateDefaultGraph();
		_graph.InstantiateGraph();
		_graphAlgorithms = new BreadthFirstSearch();
		//_graphAlgorithms = new DepthFirstSearch();
		_graph.InitializeAlgorithm(_graphAlgorithms);
		_graph.PreCalculateAlgorithm(_graphAlgorithms);
	}

	public void LoadNextAlgorithmState()
	{
		_graph.LoadNextAlgorithmState(_graphAlgorithms);
	}

	public void LoadPrevAlgorithmState()
	{
		_graph.LoadPrevAlgorithmState(_graphAlgorithms);
	}

	public void PreCalculateAlgorithm()
	{
		_graph.PreCalculateAlgorithm(_graphAlgorithms);
	}
}
