using Autofac;
using Speed.SimpleSimulator.DomainEfficient;

namespace Speed.SimpleSimulator.Autofac
{
    public class SimulatorEfficientModule : Module
    {
        private readonly int _numNodes;
        private readonly int _splinesPerNode;
        private readonly int _splineInputUpperBound;

        public SimulatorEfficientModule(int numNodes, int splinesPerNode, int splineInputUpperBound)
        {
            _numNodes = numNodes;
            _splinesPerNode = splinesPerNode;
            _splineInputUpperBound = splineInputUpperBound;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SimulatorEfficient>()
                .WithParameter("numNodes", _numNodes)
                .WithParameter("splinesPerNode", _splinesPerNode)
                .WithParameter("splineInputUpperBound", _splineInputUpperBound)
                .As<ISimulator>();

            builder
                .RegisterType<SimulationEfficient>()
                .WithParameter("splinesPerNode", _splinesPerNode)
                .WithParameter("splineInputUpperBound", _splineInputUpperBound);

            builder.RegisterType<Random>();
        }
    }
}
