using Autofac;
using Speed.SimpleSimulator.Domain;

namespace Speed.SimpleSimulator.Autofac
{
    public class SimulatorModule : Module
    {
        private readonly int _numNodes;
        private readonly int _splinesPerNode;
        private readonly int _splineInputUpperBound;

        public SimulatorModule(int numNodes, int splinesPerNode, int splineInputUpperBound)
        {
            _numNodes = numNodes;
            _splinesPerNode = splinesPerNode;
            _splineInputUpperBound = splineInputUpperBound;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Simulator>()
                .As<ISimulator>();

            builder
                .RegisterType<Simulation>()
                .WithParameter("splinesPerNode", _splinesPerNode)
                .WithParameter("splineInputUpperBound", _splineInputUpperBound);

            builder.RegisterType<SimulationNode>();

            builder.Register(c =>
            {
                return Enumerable.Range(0, _numNodes).Select(x => c.Resolve<SimulationNode>()).ToArray();
            })
            .SingleInstance();

            builder
                .RegisterType<ProbabilityModel>()
                .WithParameter("splinesPerNode", _splinesPerNode)
                .WithParameter("splineInputUpperBound", _splineInputUpperBound);

            builder.RegisterType<Random>();
        }
    }
}
