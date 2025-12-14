namespace Speed.SimpleSimulator.DomainEfficient
{
    internal class SimulationEfficient
    {
        private readonly int _splinesPerNode;
        private readonly int _splineInputUpperBound;

        public SimulationEfficient(int splinesPerNode, int splineInputUpperBound)
        {
            _splinesPerNode = splinesPerNode;
            _splineInputUpperBound = splineInputUpperBound;
        }

        public void Simulate(
            int numberOfIterations,
            double[] megaSplineArray,
            int[] nodeOffsets)
        {
            var gameState = new GameStateEfficient
            {
                SplineInputs = new int[_splinesPerNode],
                SplineInputUpperBound = _splineInputUpperBound,
                Iteration = 0,
                CurrentNodeIndex = 0,
                LastSplineIndex = 0
            };

            var random = new FastRandom(12345 + numberOfIterations);

            while (gameState.Iteration < numberOfIterations)
            {
                var nodeId = gameState.CurrentNodeIndex;

                var currentSplineOffset = nodeOffsets[nodeId];

                var value = 0.0;
                var inputs = gameState.SplineInputs;

                for (var i = 0; i < _splinesPerNode; i++)
                {
                    value += megaSplineArray[currentSplineOffset + inputs[i]];
                    currentSplineOffset += _splineInputUpperBound;
                }

                var probability = 0.5 * (value / (1.0 + Math.Abs(value)) + 1.0);
                var outcome = random.NextDouble() < probability;

                gameState.Advance(outcome, nodeOffsets.Length);
            }
        }
    }
}
