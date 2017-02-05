using System;

namespace RandomNumberGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] input;

            do {
                Console.WriteLine("In space-separated format, please provide the following arguments:");
                Console.WriteLine("\tLower Bound (inclusive)");
                Console.WriteLine("\tUpper Bound (inclusive)");
                Console.WriteLine("\tQuantity of Numbers (optional)");
                Console.WriteLine();
                input = Array.ConvertAll(Console.ReadLine().ToString().Split(' '), int.Parse);

                if (input.Length != 2 && input.Length != 3)
                    Console.WriteLine("Please give 2 or 3 inputs.");
            } while (input.Length != 2 && input.Length != 3);

            int min = input[0];
            int max = input[1];
            int numIterations = (input.Length == 3) ? input[2] : 1;

            Console.WriteLine();
            Console.WriteLine("Random Number(s):");
            for (int i=0; i<numIterations; i++)
                Console.WriteLine(rnd.Next(min, max+1));

            Console.ReadKey();  //this stops the window from instantly closing
        }
    }
}
