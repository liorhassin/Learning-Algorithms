using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Nodes chosen will be in alphabetical order
 */
public class DepthFirstSearch : IGraphAlgorithms
{
    private Stack<Node> _nodeStack;
    private HashSet<Node> _visitedSet;
    private int _status; // 1 -> check current node edges, 0 -> find next node.
    public void InitializeAlgorithm(Graph graph)
    {
        _nodeStack = new Stack<Node>();
        _visitedSet = new HashSet<Node>();
        for (int i = 0; i < graph.GetNumberOfNodes(); i++) { // pi to max int.
            graph.GetNode(i).SetPi(int.MaxValue);
        }
        _nodeStack.Push(graph.GetNode(0)); //Nodes are already sorted in alphabetical order.(take A)
        _nodeStack.Peek().SetPi(0);
        _status = 1;
    }

    public void FinishAlgorithm()
    {
        while (_nodeStack.Any())
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        if (!_nodeStack.Any()) //Algorithm is done if stack is empty.
        {
            return;
        }
        
        if (_status == 1)
        {
            _nodeStack.Peek().SetNodeMaterialColor(1); //color current node.
            _visitedSet.Add(_nodeStack.Peek()); // Mark as visited node
            _status = (_status + 1) % 2;
        }
        else
        {
            Node findNext = FindAlphabeticUnvisitedNode(_nodeStack.Peek());
            if (findNext == null)
            {
                _nodeStack.Peek().SetNodeMaterialColor(2);
                _nodeStack.Pop(); // This node is done, No adjacent unvisited nodes found.
                return;
            }
            findNext.SetPi(_nodeStack.Peek().GetPi() + 1);
            _nodeStack.Push(findNext);
            _status = (_status + 1) % 2;
        }
    }

    public void PrevStep()
    {
        throw new System.NotImplementedException();
    }

    /**
     * Method will find the next node meeting the requirements of both not visited and alphabetic.
     * Color edge leading to the current node.
     */
    private Node FindAlphabeticUnvisitedNode(Node node)
    {
        Node findNextNode = null;
        Edge currentEdge = null;
        foreach (Edge edge in node.GetEdges())
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

        if (currentEdge != null)
        {
            currentEdge.SetEdgeMaterialColor(2);
        }
        return findNextNode;
    }
}
