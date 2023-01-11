﻿using UnityEngine;

public class GraphManager : MonoBehaviour
{
	private static Graph _graph;
	public TextAsset file; //Later change to json files.(or gml)
	public GameObject[] nodePreFab; //Filled with prefabs of possible nodes.
	public GameObject[] edgePreFab; //Filled with prefabs of possible edges.
	//public Material[] materialColors; //Filled with prefabs of possible node colors.
	public Camera mainCamera; //Main camera presented to the player.
	private IGraphAlgorithms _graphAlgorithms;

	private void Start()
	{
		_graph = new Graph(nodePreFab[0], edgePreFab[0], false, true, 6);
		//Load default graph if none is specified.
		_graph.GenerateDefaultGraph();
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
}
