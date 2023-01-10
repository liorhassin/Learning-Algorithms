using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BreadthFirstSearch : IGraphAlgorithms
{
    public void InitializeAlgorithm(Graph graph)
    {
        int size = graph.GetNumberOfNodes();
        bool[] visited = new bool[size];
        for (int i = 0; i < size; i++) { // Initialize visited array and pi to max int.
            visited[i] = false;
            graph.GetNode(i).SetPi(Int32.MaxValue);
        }
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(graph.GetNode(0)); // Set node A as starting node for now, later player will be able to choose.
                                                  // Later change to GetStartNode from graph method(new)
        graph.GetNode(0).SetPi(0);
        while (queue.Any())
        {
            Node currentNode = queue.Dequeue();
            currentNode.SetNodeMaterialColor();
            byte[] encode = Encoding.ASCII.GetBytes(currentNode.GetNodeLetter());
            visited[encode[0] - 65] = true;
            foreach(Edge edge in currentNode.GetEdges())
            {
                encode = Encoding.ASCII.GetBytes(edge.GetTo().GetNodeLetter());
                if (!visited[encode[0] - 65])
                {
                    queue.Enqueue(edge.GetTo());
                }
                if (edge.GetFrom().GetPi() + 1 < edge.GetTo().GetPi())
                {
                    edge.GetTo().SetPi(edge.GetFrom().GetPi() + 1);
                    edge.SetEdgeMaterialColor();
                }
            }
        }
    }

    public void FinishAlgorithm()
    {
        throw new NotImplementedException();
    }

    public void NextStep()
    {
        throw new NotImplementedException();
    }

    public void PrevStep()
    {
        throw new NotImplementedException();
    }
}