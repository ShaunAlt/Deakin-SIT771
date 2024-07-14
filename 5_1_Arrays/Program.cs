using System;
using System.Linq;
using SplashKitSDK;

namespace _5_1_Arrays
{
    public class Program
    {
        // get valid `double`
        public static double ReadDouble(string prompt)
        {
            Console.Write($"{prompt}\n\t>>> ");
            while (true)
            {
                try
                {
                    return Double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.Write("Please enter a valid double\n\t>>> ");
                }
            }
        }

        // get valid `int`
        public static int ReadInteger(string prompt)
        {
            Console.Write($"{prompt}\n\t>>> ");
            while (true)
            {
                try
                {
                    return Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.Write("Please enter a valid integer\n\t>>> ");
                }
            }
        }

        // mainloop
        public static void Main()
        {
            // get the number of values to read
            int numberOfValues = ReadInteger("How Many Values to Store?");

            // read the values
            double[] values = new double[numberOfValues];
            for (int i = 0; i < numberOfValues; i++)
            {
                values[i] = ReadDouble($"Enter Value #{i + 1}:");
            }

            // print the values + sum
            Console.WriteLine("Stored Values:");
            for (int i = 0; i < numberOfValues; i++)
            {
                Console.WriteLine($"\tValue {i+1}: {values[i]}");
            }
            double sum = values.Sum();
            Console.WriteLine($"Sum of Values: {sum}");
        }
    }
}
