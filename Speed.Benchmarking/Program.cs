using Autofac;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Speed.Benchmarking.Autofac;
using Speed.SimpleSimulator.Domain;

[MemoryDiagnoser]
public class SimBenchmark
{
    private Simulation _cachedSimulation;
    private int _numIterationsPerSim;

    [GlobalSetup]
    public void Setup()
    {
        var container = Bootstrapper.Bootstrap(
            numNodes: 20,
            splinesPerNode: 20,
            splineInputUpperBound: 100);

        using var scope = container.BeginLifetimeScope();

        var factory = container.Resolve<Func<Simulation>>();
        _cachedSimulation = factory();

        _numIterationsPerSim = 200 * 20;
    }

    [Benchmark]
    public void RunSingleSimulation()
    {
        _cachedSimulation.Simulate(_numIterationsPerSim);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<SimBenchmark>();
    }
}
