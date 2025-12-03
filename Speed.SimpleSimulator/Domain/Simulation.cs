namespace Speed.SimpleSimulator.Domain
{
    internal class Simulation
    {
        private readonly SimulationNode[] _nodes;
        private readonly int _splinesPerNode;
        private readonly int _splineInputUpperBound;

        public Simulation(SimulationNode[] nodes, int splinesPerNode, int splineInputUpperBound)
        {
            _nodes = nodes;
            _splinesPerNode = splinesPerNode;
            _splineInputUpperBound = splineInputUpperBound;
        }

        public void Simulate(int numberOfIterations)
        {
            var gameState = new GameState(_splinesPerNode, _splineInputUpperBound);

            while (gameState.Iteration < numberOfIterations) 
            {
                var node = _nodes[gameState.CurrentNodeIndex];

                var outcome = node.Simulate(gameState);

                gameState.Advance(outcome, _nodes.Length);
            }
        }
    }
}
