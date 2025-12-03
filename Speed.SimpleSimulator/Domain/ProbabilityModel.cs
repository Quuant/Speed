using MathNet.Numerics;

namespace Speed.SimpleSimulator.Domain
{
    internal class ProbabilityModel
    {
        double[][] _splines;

        public ProbabilityModel(Random random, int splinesPerNode, int splineInputUpperBound)
        {
            var splines = new List<double[]>();

            for (var i = 0; i < splinesPerNode; i++)
                splines.Add(Enumerable.Range(0, splineInputUpperBound).Select(_ => random.NextDouble() - 0.5).ToArray());

            _splines = splines.ToArray();
        }

        public double Probability(GameState gameState)
        {
            var value = 0.0;

            for (var i = 0; i < gameState.SplineInputs.Length; i++)
            {
                value += _splines[i][gameState.SplineInputs[i]];
            }

            return SpecialFunctions.Logistic(value);
        }
    }
}
