namespace Speed.SimpleSimulator.Domain
{
    internal class GameState
    {
        public GameState(int splinesPerNode, int splineUpperBound) 
        {
            SplineInputs = new int[splinesPerNode];
            SplineInputUpperBound = splineUpperBound;
            Iteration = 0;
            CurrentNodeIndex = 0;
            LastSplineIndex = 0;
        }

        public int[] SplineInputs { get; }

        public int SplineInputUpperBound { get;}

        public int Iteration { get; private set; }

        public int CurrentNodeIndex { get; private set; }

        public int LastSplineIndex { get; private set; }

        public void Advance(bool outcome, int numNodes)
        {
            LastSplineIndex += (outcome ? 1 : 0);
            LastSplineIndex %= SplineInputs.Length;

            SplineInputs[LastSplineIndex] += 1;
            SplineInputs[LastSplineIndex] %= SplineInputUpperBound;

            if (outcome)
                CurrentNodeIndex = (CurrentNodeIndex + 1) % numNodes;
            else
                CurrentNodeIndex = (CurrentNodeIndex + 2) % numNodes;

            Iteration++;
        }
    }
}
