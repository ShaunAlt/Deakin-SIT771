using System;
using SplashKitSDK;

namespace WorkingWithObjects
{
    public class Program
    {
        public static void Main()
        {
            // using pre-defined assignment code
            Window shapesWindow = CreateShapesWindow();
            shapesWindow.Refresh(); // show window

            // custom window creation + drawing
            Window customWindow = CreateCustomWindow();
            customWindow.Refresh(); // show window

            SplashKit.Delay(5000); // time delay (ms)
        }

        // Custom Window Creation + Drawing
        public static Window CreateCustomWindow()
        {
            // create the window
            Window w = new Window(
                "Custom Window (Shaun Altmann 221260334)",
                120,
                90
            );

            // clear window + draw shapes
            w.Clear(Color.Green);
            w.DrawRectangle(Color.White, 10, 10, 100, 70);
            w.DrawRectangle(Color.White, 10, 30, 15, 30);
            w.DrawRectangle(Color.White, 95, 30, 15, 30);
            w.DrawLine(Color.White, 60, 10, 60, 80);
            w.DrawCircle(Color.White, 60, 45, 5);

            return w;
        }

        // Pre-Defined Assignment Code
        public static Window CreateShapesWindow()
        {
            // create first window
            Window shapesWindow;
            shapesWindow = new Window("Shapes by ...", 800, 600);

            // clear window and draw shapes
            shapesWindow.Clear(Color.White);
            shapesWindow.FillEllipse(Color.BrightGreen, 0, 400, 800, 400);
            shapesWindow.FillRectangle(Color.Gray, 300, 300, 200, 200);
            shapesWindow.FillTriangle(Color.Red, 250, 300, 400, 150, 550, 300);

            return shapesWindow;
        }
    }
}
