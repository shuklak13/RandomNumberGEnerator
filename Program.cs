using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RandomNumberGenerator
{
    class Program {

        static Random rnd = new Random();
        static String input;
        private static void Main(string[] args)
        {
            while (true) {
                Console.Write("\nType 1 for Uniform RNG, 2 for Gaussian RNG, or 3 for List Randomizer:   ");
                input = Console.ReadLine();

                if (Regex.IsMatch(input, @"3(\s)*"))
                    ListRandomizer();
                else if (Regex.IsMatch(input, @"2(\s)*") || Regex.IsMatch(input, @"1(\s)*")) {

                    //The reason I read in as a string is so the user can skip by clicking enter
                    Console.Write("Quantity of Numbers to Generate (optional, defaults to 1):   ");
                    string qtyString = Console.ReadLine();
                    int qty = (CheckIfNumber(qtyString)) ? Convert.ToInt32(qtyString) : 1;

                    if (Regex.IsMatch(input, @"2(\s)*"))
                        RNG_Gaussian(qty);
                    if (Regex.IsMatch(input, @"1(\s)*"))
                        RNG_Uniform(qty);
                } else {
                    Console.Write("Input unrecognized. Try again.\n\n");
                    continue;
                }
                Console.ReadKey();  //pause
            }
        }

        private static bool CheckIfNumber(string input) {
            int value;
            return int.TryParse(input, out value);
        }

        private static void RNG_Uniform(int qty) {
            Console.Write("Lower Bound (inclusive) (optional, defaults to 1):   ");
            string minString = Console.ReadLine();
            int min = (CheckIfNumber(minString)) ? Convert.ToInt32(minString) : 1;

            Console.Write("Upper Bound (inclusive):   ");
            int max = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("Random Number(s):");
            for (int i = 0; i < qty; i++)
                Console.WriteLine(Draw_Uniform(min, max));
        }
        private static int Draw_Uniform(int min, int max) {
            //if for some reason input is a double, it will be truncated to an integer
            return rnd.Next(min, max + 1);
        }

        private static void RNG_Gaussian(int qty) {
            Console.Write("Mean (optional, defaults to 0):   ");
            string meanString = Console.ReadLine();
            double mean = (CheckIfNumber(meanString)) ? Convert.ToDouble(meanString) : 0;

            Console.Write("Standard Deviation (recommendation: half of what 95% of data is within):   ");
            double sd = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("Random Number(s):");
            for (int i = 0; i < qty; i++)
                Console.WriteLine(Draw_Gaussian(mean, sd));
        }
        //Box-Muller Transformation (converts a pair of uniforms into a normal)
        private static double Draw_Gaussian(double mean, double stdDev) {
            double u1 = 1.0 - rnd.NextDouble();
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); 
            return mean + stdDev * randStdNormal; 
        }

        private static void ListRandomizer() {
            Console.WriteLine("Type each element of the list, separated by a space.");
            Console.WriteLine("End the list with a triple dash '---'\n");
            List<string> list = new List<string>();
            while (true) {
                string elem = Console.ReadLine();
                if (!elem.Equals("---"))
                    list.Add(elem);
                else
                    break;
            }
            Shuffle(list).ForEach(Console.WriteLine);
        }
        private static List<string> Shuffle(List<string> list) {
            for (var i = 0; i < list.Count; i++)
                Swap(ref list, i, rnd.Next(i, list.Count));
            return list;
        }
        private static void Swap(ref List<string> list, int a, int b) {
            string temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }
    }
}
