using Autofac;
using Speed.SimpleSimulator.Autofac;

namespace Speed.Sandbox.Autofac
{
    internal class Bootstrapper
    {
        public static IContainer Bootstrap(int numNodes, int splinesPerNode, int splineInputUpperBound)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule(new SimulatorModule(numNodes, splinesPerNode, splineInputUpperBound));
            builder.RegisterModule(new SimulatorEfficientModule(numNodes, splinesPerNode, splineInputUpperBound));

            return builder.Build();
        }
    }
}
