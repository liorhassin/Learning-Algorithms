using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class BreadthFirstSearch : IGraphAlgorithms
{
    //Base parameters for algorithm
    private Queue<Node> _queue;
    private Node _currentNode;
    private HashSet<Node> _visitedSet;
    private int _status;
    
    //Base parameters for prev function
    //TODO - Complete prev with parameters to store status or class that stores
    //TODO - 1 Node and List of Edges, each saves the previous status of changed node and edges.
    //TODO - Current runs, First: Update node being checked color,
    //TODO -               Second: Update edges colors + next nodes
    //TODO - Possible fix for issue, add to edge bool if visited or not. so the current Next run
    //TODO - Will check 1 edge at a time.
    public void InitializeAlgorithm(Graph graph)
    {
        _visitedSet = new HashSet<Node>();
        _currentNode = null;
        _queue = new Queue<Node>();
        int size = graph.GetNumberOfNodes();
        for (int i = 0; i < size; i++) { // pi to max int.
            graph.GetNode(i).SetPi(int.MaxValue);
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
            _currentNode.SetNodeMaterialColor(2);
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
                    edge.SetEdgeMaterialColor(2);
                }
                if (edge.GetColorStatus() != 2)
                {
                    edge.SetEdgeMaterialColor(3);
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
                    edge.SetEdgeMaterialColor(2);
                }
                if (edge.GetColorStatus() != 2)
                {
                    edge.SetEdgeMaterialColor(3);
                }
            }
            _currentNode.SetNodeMaterialColor(2);
            _status = (_status + 1) % 2;
        }
        else if(_status == 0) //Changing Node color to signal this is the next Node being checked.
        {
            if (_queue.Any())
            {
                _currentNode = _queue.Dequeue();
                _currentNode.SetNodeMaterialColor(1);
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