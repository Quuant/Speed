namespace Speed.SimpleSimulator.Domain
{
    internal class SimulationNode
    {
        private readonly Random _random;
        private readonly ProbabilityModel _probabilityModel;

        public SimulationNode(Random random, ProbabilityModel probabilityModel)
        {
            _random = random;
            _probabilityModel = probabilityModel;
        }

        public bool Simulate(GameState gameState)
        {
            var probability = _probabilityModel.Probability(gameState);

            return _random.NextDouble() < probability;
        }
    }
}
