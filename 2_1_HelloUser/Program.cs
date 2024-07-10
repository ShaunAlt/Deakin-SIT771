using System;
using SplashKitSDK;

namespace _2_1_HelloUser
{
    public class Program
    {
        public static void Main()
        {
            // initialize variables
            string name = ""; // user name
            string inputTest; // height input value
            int heightInCM = 0; // user height (cm)
            float heightInMeters; // user height (m)
            float weightInKG = 0; // user weight (kg)
            float bmi; // user's BMI

            // get user's name (must not be empty or pure whitespace)
            while (string.IsNullOrWhiteSpace(name)) {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
            }
            Console.WriteLine($"Hello {name}");

            // get user's height (cm) (must be positive integer)
            while (heightInCM <= 0) {
                Console.Write("Enter your height (to the nearest CM): ");
                inputTest = Console.ReadLine();
                int.TryParse(inputTest, out heightInCM);
            }

            // convert height from cm to m
            heightInMeters = heightInCM / 100.0f;

            // get user's weight (kg) (must be positive float)
            while (weightInKG <= 0) {
                Console.Write("Enter your weight (in KG): ");
                float.TryParse(Console.ReadLine(), out weightInKG);
            }

            // calculate BMI
            bmi = weightInKG / (heightInMeters * heightInMeters);

            // output user details
            Console.Write(
                $"Your Details:\n| - Name: {name}\n"
                + $"| - Height: {heightInCM}cm ({heightInMeters:N2}m)\n"
                + $"| - Weight: {weightInKG:N2}kg\n"
                + $"| - BMI: {bmi:N2}\n"
            );
        }
    }
}
