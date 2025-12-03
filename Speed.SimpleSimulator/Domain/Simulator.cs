namespace Speed.SimpleSimulator.Domain
{
    internal class Simulator : ISimulator
    {
        private readonly int _numberOfThreadsToUse;
        private readonly Func<Simulation> _simulationFactory;

        public Simulator(Func<Simulation> simulationFactory)
        {
            _simulationFactory = simulationFactory;
            _numberOfThreadsToUse = Environment.ProcessorCount - 1;
        }

        public void RunSimulator(int numberOfSimulationsoRun, int numberOfIterationsPerSim)
        {
            Parallel.For(0, _numberOfThreadsToUse, threadIndex =>
            {
                Simulate(threadIndex, numberOfSimulationsoRun, numberOfIterationsPerSim);
            });
        }

        private void Simulate(int threadIndex, int numberOfSimulationsoRun, int numberOfIterationsPerSim)
        {
            var simulation = _simulationFactory();

            var threadsPerSim = numberOfSimulationsoRun / _numberOfThreadsToUse;
            var startSimulation = threadIndex * threadsPerSim;
            var endSimulation = Math.Min(numberOfSimulationsoRun, startSimulation + threadsPerSim + _numberOfThreadsToUse);

            for (var i = startSimulation; i < endSimulation; i++)
            {
                simulation.Simulate(numberOfIterationsPerSim);
            }
        }
    }
}
