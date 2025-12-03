namespace Speed.SimpleSimulator.DomainEfficient
{
    internal class SimulatorEfficient : ISimulator
    {
        private readonly int _numberOfThreadsToUse;
        private readonly Func<SimulationEfficient> _simulationFactory;

        private readonly int _splinesPerNode;
        private readonly int _splineInputUpperBound;
        private readonly int _numNodes;

        public SimulatorEfficient(
            Func<SimulationEfficient> simulationFactory,
            int numNodes,
            int splinesPerNode,
            int splineInputUpperBound)
        {
            _simulationFactory = simulationFactory;
            _numberOfThreadsToUse = Environment.ProcessorCount - 1;
            _numNodes = numNodes;
            _splinesPerNode = splinesPerNode;
            _splineInputUpperBound = splineInputUpperBound;
        }

        public void RunSimulator(int numberOfSimulationsoRun, int numberOfIterationsPerSim)
        {
            var random = new Random();

            var megaSplineArray = new double[_numNodes * _splinesPerNode * _splineInputUpperBound];

            for (var n = 0; n < _numNodes; ++n)
            {
                var offsetNode = n * _splinesPerNode * _splineInputUpperBound;
                for (var sp = 0; sp < _splinesPerNode; ++sp)
                {
                    var offsetSpline = sp * _splineInputUpperBound;
                    for (var str = 0; str < _splineInputUpperBound; ++str)
                    {
                        if (str >= _splineInputUpperBound)
                            continue;

                        megaSplineArray[offsetNode + offsetSpline + str] = random.NextDouble() - 0.5;
                    }
                }
            }

            var nodeOffsets = Enumerable.Range(0, _numNodes).Select(x => x * _splinesPerNode * _splineInputUpperBound).ToArray();

            Parallel.For(0, _numberOfThreadsToUse, threadIndex =>
            {
                Simulate(threadIndex, numberOfSimulationsoRun, numberOfIterationsPerSim, megaSplineArray, nodeOffsets);
            });
        }

        private void Simulate(
            int threadIndex,
            int numberOfSimulationsoRun,
            int numberOfIterationsPerSim,
            double[] megaSplineArray,
            int[] nodeOffsets)
        {
            var simulation = _simulationFactory();

            var threadsPerSim = numberOfSimulationsoRun / _numberOfThreadsToUse;
            var startSimulation = threadIndex * threadsPerSim;
            var endSimulation = Math.Min(numberOfSimulationsoRun, startSimulation + threadsPerSim + _numberOfThreadsToUse);

            for (var i = startSimulation; i < endSimulation; i++)
            {
                simulation.Simulate(numberOfIterationsPerSim, megaSplineArray, nodeOffsets);
            }
        }
    }
}
