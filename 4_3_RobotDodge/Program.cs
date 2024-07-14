using System;
using SplashKitSDK;

namespace _4_3_RobotDodge
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

                // // clear screen
                // SplashKit.ClearScreen(Color.White);

                // // handle player inputs
                // p.HandleInput();

                // // keep player on window
                // p.StayOnWindow(w);

                // // draw player
                // p.Draw();

                // // refresh window
                // w.Refresh(60);
            }
        }
    }
}
