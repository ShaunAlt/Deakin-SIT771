using System;
using SplashKitSDK;

namespace _6_2_RobotDodgeChanges
{
    public class Program
    {
        public static void Main()
        {
            // create game
            Window w = new Window("Robot Dodge", 800, 550);
            RobotDodge r = new RobotDodge(w);

            while (
                    (!SplashKit.WindowCloseRequested(w))
                    && (!r.quit)
            )
            {
                // process events
                SplashKit.ProcessEvents();

                // handle events
                r.HandleInput();

                // update game
                r.Update();

                // draw game
                r.Draw();
            }

            // print results
            Console.WriteLine(
                "Results:\n"
                + $"| - Level: {r.level}\n"
                + $"| - Score: {r.score}\n"
            );
        }
    }
}
