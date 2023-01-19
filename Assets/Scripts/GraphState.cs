/**
     * Each state represent current node looking at,
     * which parameters were changed and if edge was taken or not.
     * Acts as a base class with most common needed parameters.
     * Class can be inherited in order to add more depth needed parameters for each algorithm.
     */
public abstract class GraphState
{
    private Node _currentNode;
    private Edge _currentEdge;

    protected GraphState(Node node, Edge edge)
    {
        SetCurrentNode(node);
        SetCurrentEdge(edge);
    }
    
    /**
     * Define how next state is being loaded.
     */
    public abstract void LoadNextState();

    /**
     * Define how previous state is being loaded.
     */
    public abstract void LoadPrevState();

    public Node GetCurrentNode()
    {
        return _currentNode;
    }

    public Edge GetCurrentEdge()
    {
        return _currentEdge;
    }

    private void SetCurrentNode(Node node)
    {
        _currentNode = node;
    }

    private void SetCurrentEdge(Edge edge)
    {
        _currentEdge = edge;
    }
};