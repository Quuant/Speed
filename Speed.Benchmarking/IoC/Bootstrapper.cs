using Autofac;
using Speed.SimpleSimulator.Autofac;

namespace Speed.Benchmarking.Autofac
{
    internal class Bootstrapper
    {
        public static IContainer Bootstrap(int numNodes, int splinesPerNode, int splineInputUpperBound)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new SimulatorModule(numNodes, splinesPerNode, splineInputUpperBound));

            return builder.Build();
        }
    }
}
