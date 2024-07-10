using System;
using SplashKitSDK;

namespace _3_1_NameTester
{
    public class Program
    {
        // Menu Options Enumerable
        public enum MenuOption {
            TestName,
            GuessThatNumber,
            Quit,
        }

        // Main Function
        public static void Main()
        {
            // initialize variables
            MenuOption userSelection;

            // main loop
            do {
                // get selected option
                userSelection = ReadUserOption();

                // run requested function
                switch (userSelection) {
                    case MenuOption.TestName:
                        TestName();
                        break;
                    case MenuOption.GuessThatNumber:
                        RunGuessThatNumber();
                        break;
                }
            } while (userSelection != MenuOption.Quit); // stop on quit
        }

        // Read Guess
        private static int ReadGuess(int min, int max) {
            // initialize variables
            int guess = -1;

            // loop until valid guess selected
            do {
                Console.Write($"Select a number between {min} and {max}: ");
                int.TryParse(Console.ReadLine(), out guess);
            } while (guess < min || guess > max);

            return guess;
        }

        // Read User Option
        private static MenuOption ReadUserOption() {
            // initialize variables
            int option = 0;
            int[] validOptions = new int[] { 1, 2, 3, };

            // loop until valid option selected
            do {
                Console.Write(
                    ".~~~~~~~~~~~~~~~~~~~~~~~.\n"
                    + "|   3.1 - Name Tester   |\n"
                    + "|-----------------------|\n"
                    + "| 1. Test Name          |\n"
                    + "| 2. Guess That Number  |\n"
                    + "| 3. Quit               |\n"
                    + "|_______________________|\n"
                );
                int.TryParse(Console.ReadLine(), out option);
            } while (!validOptions.Contains(option));
            
            // return selected option
            return (MenuOption) (option - 1);
        }

        // Run GuessThatNumber
        private static void RunGuessThatNumber() {
            // initialize variables
            int guess = -1;
            int lowGuess = 1;
            int highGuess = 100;
            int target = new Random().Next(100) + 1;

            while (guess != target) {
                guess = ReadGuess(lowGuess, highGuess);
                if (guess < target) {
                    Console.Write("Too Low, Try Again - ");
                    lowGuess = guess;
                }
                else if (guess > target) {
                    Console.Write("Too High, Try Again - ");
                    highGuess = guess;
                }
            }
            Console.WriteLine($"You Guessed the Target {target}");
        }

        // Test Name
        private static void TestName() {
            // initialize variables
            string name = "";

            // get user's name (must not be empty or pure whitespace)
            while (string.IsNullOrWhiteSpace(name)) {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
            }
            Console.WriteLine($"Hello {name}");

            // custom messages for different names
            if (name.ToLower() == "shaun") {
                Console.WriteLine("Amazing Name");
            }
            else if (name.ToLower() == "andrew") {
                Console.WriteLine("Cool Name");
            }
            else if (name.ToLower() == "jim") {
                Console.WriteLine("Nice Name");
            }
            else {
                Console.WriteLine("Ok Name");
            }
        }
    }
}
