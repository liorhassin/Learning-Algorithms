public interface IGraphAlgorithms
{
    /**
     * Initialize all the variables needed for the algorithm.
     */
    public void InitializeAlgorithm(Graph graph);

    /**
     * Will run the algorithm from current state.
     * Possible addition will be to support time gap between steps when auto completing algorithm.
     */
    public void PreCalculateAlgorithm();

    /**
     * Goes one step ahead into the algorithm.
     */
    public void LoadNextAlgorithmState();
    
    /**
     * Goes to the previous step in the algorithm.
     */
    public void LoadPrevAlgorithmState();
}