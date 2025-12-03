using Autofac;
using Speed.Sandbox.Autofac;
using Speed.SimpleSimulator;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int numSimulations = 10_000;

        int numNodes = 20;
        int splinesPerNode = 20;
        int splineInputUpperBound = 100;
        int numberOfIterationsPerSim = 200 * numNodes;

        var container = Bootstrapper.Bootstrap(numNodes, splinesPerNode, splineInputUpperBound);
        using var scope = container.BeginLifetimeScope();

        var simulator = container.Resolve<ISimulator>();

        var stopWatch = new Stopwatch();

        for (var i = 0; i < 20; ++i)
        {
            stopWatch.Restart();
            simulator.RunSimulator(numSimulations, numberOfIterationsPerSim);
            stopWatch.Stop();
            Console.WriteLine($"{i}: {stopWatch.ElapsedMilliseconds}ms");
        }
    }
}
