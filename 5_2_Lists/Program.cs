using System;
using SplashKitSDK;

namespace _5_2_Lists
{
    public class Program
    {
        // private variables
        private static List<double> _values = new List<double>();

        // public variables
        public enum UserOption
        {
            NewValue,
            Print,
            Sum,
            Quit,
        }

        // add value to list
        public static void AddValueToList()
        {
            _values.Add(ReadDouble("Enter Value:"));
        }

        // print list values
        public static void Print()
        {
            Console.WriteLine("Stored Values:");
            for (int i = 0; i < _values.Count; i++)
            {
                Console.WriteLine($"\tValue {i+1}: {_values[i]}");
            }
        }

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

        // read user option
        public static UserOption ReadUserOption()
        {
            Console.Write(
                "Menu Options:\n"
                + "\t0 - Add a Value\n"
                + "\t1 - Print Values\n"
                + "\t2 - Calculate Sum\n"
                + "\t3 - Quit\n"
            );
            int option = 3;
            Int32.TryParse(Console.ReadLine(), out option);
            return (UserOption) (option);
        }

        // print list sum
        public static void Sum()
        {
            double sum = 0;
            for (int i = 0; i < _values.Count; i++)
            {
                sum += _values[i];
            }
            Console.WriteLine($"Sum of Values: {sum}");
        }

        public static void Main()
        {
            UserOption option;
            do
            {
                option = ReadUserOption();
                switch (option)
                {
                    case UserOption.NewValue:
                        AddValueToList();
                        break;
                    case UserOption.Print:
                        Print();
                        break;
                    case UserOption.Sum:
                        Sum();
                        break;
                }
            }
            while (option != UserOption.Quit);
        }
    }
}
