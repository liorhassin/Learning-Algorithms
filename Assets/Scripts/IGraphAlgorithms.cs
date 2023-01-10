public interface IGraphAlgorithms
{
    /**
     * Initialize all the variables needed for the algorithm.
     */
    public void InitializeAlgorithm(Graph graph);

    /**
     * Will run the algorithm from current state, with a few seconds between each step.
     */
    public void FinishAlgorithm();

    /**
     * Goes one step ahead into the algorithm.
     */
    public void NextStep();
    
    /**
     * Goes to the previous step in the algorithm.
     */
    public void PrevStep();
}