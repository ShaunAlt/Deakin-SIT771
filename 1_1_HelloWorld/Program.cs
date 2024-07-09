using System;
using SplashKitSDK;

namespace HelloWorld
{
    public class Program
    {
        public static void Main()
        {
            // first C#
            Console.WriteLine("Hello, World!");

            // create SplashKit window
            Window w = new Window("My First Program", 200, 100);

            // write text to window
            w.DrawText("Hello World", Color.Black, 10, 45);
            w.Refresh(60);

            // hold window open for 5 seconds
            SplashKit.Delay(10000);

            // exit the application
            Environment.Exit(0); 
        }
    }
}
