namespace Speed.SimpleSimulator.DomainEfficient
{
    internal ref struct GameStateEfficient
    {
        public Span<int> SplineInputs;

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

            ref var val = ref SplineInputs[LastSplineIndex];
            val++;
            if (val == SplineInputUpperBound)
                val = 0;

            CurrentNodeIndex += win + (loss * 2);

            if (CurrentNodeIndex >= numNodes)
                CurrentNodeIndex -= numNodes;

            Iteration++;
        }
    }
}
