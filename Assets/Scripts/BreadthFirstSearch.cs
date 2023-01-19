using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BreadthFirstSearch : IGraphAlgorithms
{
    private Queue<Node> _queue;
    private Node _currentNode;
    private HashSet<Node> _visitedSet;
    private int _status;
    private LinkedList<BFSState> _algorithmState;
    private int _algorithmStateStatus;

    public void InitializeAlgorithm(Graph graph)
    {
        _visitedSet = new HashSet<Node>();
        _currentNode = null;
        _algorithmState = new LinkedList<BFSState>();
        _algorithmStateStatus = -1;
        _queue = new Queue<Node>();
        for (int i = 0; i < graph.GetNumberOfNodes(); i++) { // pi to max int.
            graph.GetNode(i).SetPi(int.MaxValue);
        }
        _currentNode = graph.GetNode(0); // Requirement for finish algorithm while-loop.
        _queue.Enqueue(_currentNode); // Requirement for finish algorithm while-loop.
        _currentNode.SetPi(0);
        _status = 0;
        AddState(_currentNode, null, -1, -1, -1); // Save starting node with the original color.
    }
    
    public void PreCalculateAlgorithm()
    {
        while (_queue.Any())
        {
            NextStep();
        }

        //ResetStatePreview();
        foreach (BFSState s in _algorithmState)
        {
            Debug.Log(s.GetCurrentNode());
        }
        
    }

    /**
     * Helper method to pre calculate the algorithm.
     */
    private void NextStep()
    {
        if (_status == 1) //Checking Edges(Node is already colored indicating we are checking it.
        {
            /*if (_currentNode.GetColorStatus() != 2)
            {
                _currentNode.SetNodeMaterialColor(2);
                AddState(_currentNode, null, 1, -1, _currentNode.GetPi()); // Add state of the node currently being checked.
                return;
            }*/
            Edge currentEdge = FindNextAlphabeticUnvisitedEdge();
            if (currentEdge == null) // Current node adjacent nodes are all visited.
            {
                _currentNode.SetNodeMaterialColor(3); //Node is done.
                _queue.Dequeue();
                AddState(_currentNode, null, 2, -1, _currentNode.GetPi()); // Add state of finished node with the color changing to finished.
                _currentNode = null;
                _status = (_status + 1) % 2;
                return;
            }
            currentEdge.GetTo().SetNodeMaterialColor(1);
            _queue.Enqueue(currentEdge.GetTo()); // Add node to queue
            _visitedSet.Add(currentEdge.GetTo());
            currentEdge.SetEdgeMaterialColor(2); // Take edge
            AddState(_currentNode, currentEdge, 1, 0, _currentNode.GetPi()); // Save currently changed edge and node.(node is added to queue, edge is taken).
        }
        else if(_status == 0) //Changing Node color to signal this is the next Node being checked.
        {
            if (!_queue.Any())
            {
                return;
            }
            _currentNode = _queue.Peek();
            /*if (_currentNode.GetColorStatus() == 1)
            {
                _currentNode.SetNodeMaterialColor(2);
                AddState(_currentNode, null, 1, -1, _currentNode.GetPi()); // Saving the state of next node to be checked.
            }
            else
            {
                _currentNode.SetNodeMaterialColor(1);
                AddState(_currentNode, null, 0, -1, _currentNode.GetPi()); // Saving the state of next node to be checked.
            }*/
            _currentNode.SetNodeMaterialColor(2);
            AddState(_currentNode, null, 1, -1, _currentNode.GetPi());
            _visitedSet.Add(_currentNode);
            _status = (_status + 1) % 2;
        }
    }

    /**
     * Helper method to find the next alphabetic unvisited node.
     * Returns the edge leading to the node.
     */
    private Edge FindNextAlphabeticUnvisitedEdge()
    {
        Edge currentEdge = null;
        Node findNextNode = null;
        foreach (Edge edge in _currentNode.GetEdges())
        {
            if (findNextNode == null && !_visitedSet.Contains(edge.GetTo()))
            {
                currentEdge = edge;
                findNextNode = edge.GetTo();
            }
            else if (findNextNode != null && !_visitedSet.Contains(edge.GetTo()) && findNextNode.name.ToCharArray()[0] > edge.GetTo().name.ToCharArray()[0])
            {
                currentEdge = edge;
                findNextNode = edge.GetTo();
            }
        }

        return currentEdge;
    }

    public void LoadNextAlgorithmState()
    {
        if (_algorithmStateStatus < _algorithmState.Count)
        {
            _algorithmState.ElementAt(_algorithmStateStatus).LoadNextState();
            _algorithmStateStatus++;
        }
    }

    public void LoadPrevAlgorithmState()
    {
        if (_algorithmStateStatus > 0)
        {
            _algorithmState.ElementAt(_algorithmStateStatus).LoadNextState();
            _algorithmStateStatus--;
        }
    }
    
    public void ResetStatePreview()
    {
        while (_algorithmStateStatus > 0)
        {
            LoadPrevAlgorithmState();
        }
    }

    public void LoadFinalPreview()
    {
        while (_algorithmStateStatus < _algorithmState.Count)
        {
            LoadNextAlgorithmState();
        }
    }

    public void PrevStep()
    {
        throw new NotImplementedException();
    }

    private void AddState(Node node, Edge edge, int nodePrevColor, int edgePrevColor, int nodePrevPi)
    {
        BFSState bfsState = new BFSState(node, edge, nodePrevColor, edgePrevColor, nodePrevPi);
        _algorithmState.AddLast(bfsState);
        _algorithmStateStatus++;
    }

    
    /**
     * Adding required parameters for BFS state management:
     * prev colors and next colors.
     */
    private class BFSState : GraphState
    {
        private readonly int _prevNodeColor;
        private readonly int _prevEdgeColor;
        private readonly int _prevNodePi;
        private readonly int _nextNodeColor;
        private readonly int _nextEdgeColor;
        private readonly int _nextNodePi;

        public BFSState(Node node, Edge edge, int prevNodeColor, int prevEdgeColor, int prevNodePi) : base(node, edge)
        {
            _nextNodeColor = GetCurrentNode().GetColorStatus();
            _nextNodePi = GetCurrentNode().GetPi();
            if (edge != null)
            {
                _nextEdgeColor = GetCurrentEdge().GetColorStatus();
            }
            else
            {
                _nextEdgeColor = 0;
            }
            _prevNodeColor = prevNodeColor;
            _prevEdgeColor = prevEdgeColor;
            _prevNodePi = prevNodePi;
        }

        public override void LoadNextState()
        {
            GetCurrentNode().SetNodeMaterialColor(_nextNodeColor);
            GetCurrentNode().SetPi(_nextNodePi);
            if (GetCurrentEdge() != null)
                GetCurrentEdge().SetEdgeMaterialColor(_nextEdgeColor);
        }

        public override void LoadPrevState()
        {
            GetCurrentNode().SetNodeMaterialColor(_prevNodeColor);
            GetCurrentNode().SetPi(_prevNodePi);
            if(GetCurrentEdge() != null)
                GetCurrentEdge().SetEdgeMaterialColor(_prevEdgeColor);
        }
    }
}