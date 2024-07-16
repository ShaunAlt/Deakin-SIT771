using System;
using SplashKitSDK;

namespace _5_4_ManyRobots
{
    public class Program
    {
        public static void Main()
        {
            Window w = new Window("Robot Dodge", 500, 400);
            RobotDodge r = new RobotDodge(w);
            // Player p = new Player(w);

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

                // w.Refresh(60);
                // w.Refresh(60);
                // SplashKit.Delay(100);
            }
        }
    }
}
