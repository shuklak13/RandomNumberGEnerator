using System;
using System.Text.RegularExpressions;

namespace RandomNumberGenerator
{
    class Program {

        static Random rnd = new Random();
        private static void Main(string[] args)
        {
            while (true) {
                Console.Write("\nDistribution (defaults to Uniform; 'g' for Gaussian):   ");
                bool dist = Regex.IsMatch(Console.ReadLine(), @"g(\s)*");   //true=Normal, false=Uniform

                Console.Write("Lower Bound (inclusive) or Mean:   ");
                double minMean = Convert.ToDouble(Console.ReadLine());

                Console.Write("Upper Bound (inclusive) or Standard Deviation:   ");
                double maxSD = Convert.ToDouble(Console.ReadLine());

                Console.Write("Quantity of Numbers (optional, defaults to 1):   ");
                string input = Console.ReadLine();
                int numIterations = (CheckIfNumber(input)) ? Convert.ToInt32(input) : 1;

                Console.WriteLine();

                Console.WriteLine("Random Number(s):");
                for (int i = 0; i < numIterations; i++)
                    Console.WriteLine(dist ? RandomNormal(minMean, maxSD) : RandomUniform(minMean, maxSD));

                Console.ReadKey();  //pause
            }
        }

        private static bool CheckIfNumber(string input) {
            int value;
            return int.TryParse(input, out value);
        }

        private static int RandomUniform(double min, double max) {
            //if for some reason input is a double, it will be truncated to an integer
            return rnd.Next((int)min, (int) (max + 1));
        }

        //Box-Muller Transformation (converts a pair of uniforms into a normal)
        private static double RandomNormal(double mean, double stdDev) {
            double u1 = 1.0 - rnd.NextDouble();
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); 
            return mean + stdDev * randStdNormal; 
        }
    }
}
