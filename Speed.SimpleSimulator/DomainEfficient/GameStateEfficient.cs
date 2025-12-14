namespace Speed.SimpleSimulator.DomainEfficient
{
    internal class GameStateEfficient
    {
        public int[] SplineInputs;

        public int SplineInputUpperBound;

        public int Iteration;

        public int CurrentNodeIndex;

        public int LastSplineIndex;

        public void Advance(bool outcome, int numNodes)
        {
            var win = outcome ? 1 : 0;
            var loss = 1 - win;

            LastSplineIndex += win;

            if (LastSplineIndex == SplineInputs.Length)
                LastSplineIndex = 0;

            var val = SplineInputs[LastSplineIndex];
            SplineInputs[LastSplineIndex] = val == SplineInputUpperBound - 1
                ? 0 
                : val++;

            CurrentNodeIndex += win + (loss * 2);

            if (CurrentNodeIndex >= numNodes)
                CurrentNodeIndex -= numNodes;

            Iteration++;
        }
    }
}
