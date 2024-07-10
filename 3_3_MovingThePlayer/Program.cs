using System;
using SplashKitSDK;

namespace _3_3_MovingThePlayer
{
    public class Program
    {
        public static void Main()
        {
            Window w = new Window("Moving the Player", 500, 400);
            Player p = new Player(w);

            // Speed Boost has been added

            while (
                    (!SplashKit.WindowCloseRequested(w))
                    && (!p.Quit)
            ) {
                // process events
                SplashKit.ProcessEvents();

                // clear screen
                SplashKit.ClearScreen(Color.White);

                // handle player inputs
                p.HandleInput();

                // keep player on window
                p.StayOnWindow(w);

                // draw player
                p.Draw();

                // refresh window
                w.Refresh(60);
            }
        }
    }
}
