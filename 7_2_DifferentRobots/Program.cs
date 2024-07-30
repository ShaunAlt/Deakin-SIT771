using System;
using SplashKitSDK;

namespace _7_2_DifferentRobots
{
    public class Program
    {
        public static void Main()
        {
            Window w = new Window("Robot Dodge", 500, 400);
            RobotDodge r = new RobotDodge(w);

            // Speed Boost has been added

            while (
                    (!SplashKit.WindowCloseRequested(w))
                    && (!r.Quit)
            ) {
                // process events
                SplashKit.ProcessEvents();

                // handle events
                r.HandleInput();

                // update game
                r.Update();

                // draw game
                r.Draw();
            }
        }
    }
}
