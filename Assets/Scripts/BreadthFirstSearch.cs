using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BreadthFirstSearch : IGraphAlgorithms
{
    private Queue<Node> _queue;
    private Node _currentNode;
    private HashSet<Node> _visitedSet;
    private bool[] _visited;
    private int _status; //Status will change from 0 to 1 and indicate if next step
                         //is checking next node or checking edges of node in queue.
    private byte[] _encode;
    
    public void InitializeAlgorithm(Graph graph)
    {
        _visitedSet = new HashSet<Node>();
        _currentNode = null;
        _queue = new Queue<Node>();
        int size = graph.GetNumberOfNodes();
        _visited = new bool[size];
        for (int i = 0; i < size; i++) { // Initialize visited array and pi to max int.
            _visited[i] = false;
            graph.GetNode(i).SetPi(Int32.MaxValue);
        }
        
        // Set node A as starting node for now, later player will be able to choose.
        // Later change to GetStartNode from graph method(new)
        graph.GetNode(0).SetPi(0);
        _queue.Enqueue(graph.GetNode(0));
        _status = 0;
    }

    public void FinishAlgorithm()
    {
        while (_queue.Any())
        {
            _currentNode = _queue.Dequeue();
            _currentNode.SetNodeMaterialColor();
            foreach(Edge edge in _currentNode.GetEdges())
            {
                if (!_visitedSet.Contains(edge.GetTo()))
                {
                    _queue.Enqueue(edge.GetTo());
                    _visitedSet.Add(edge.GetTo());
                }
                if (edge.GetFrom().GetPi() + 1 < edge.GetTo().GetPi())
                {
                    edge.GetTo().SetPi(edge.GetFrom().GetPi() + 1);
                    edge.SetEdgeMaterialColor();
                }
            }
        }
    }

    public void NextStep()
    {
        if (_status == 1) //Checking Edges(Node is already colored indicating we are checking it.
        {
            foreach(Edge edge in _currentNode.GetEdges())
            {
                if (!_visitedSet.Contains(edge.GetTo()))
                {
                    _queue.Enqueue(edge.GetTo());
                    _visitedSet.Add(edge.GetTo());
                }
                if (edge.GetFrom().GetPi() + 1 < edge.GetTo().GetPi())
                {
                    edge.GetTo().SetPi(edge.GetFrom().GetPi() + 1);
                    edge.SetEdgeMaterialColor();
                }
            }
            
            _status = (_status + 1) % 2;
        }
        else if(_status == 0) //Changing Node color to signal this is the next Node being checked.
        {
            if (_queue.Any())
            {
                _currentNode = _queue.Dequeue();
                _currentNode.SetNodeMaterialColor();
                _visitedSet.Add(_currentNode);
                _status = (_status + 1) % 2;
            }
        }
    }

    public void PrevStep()
    {
        throw new NotImplementedException();
    }
}