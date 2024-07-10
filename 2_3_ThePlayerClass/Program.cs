using System;
using SplashKitSDK;

namespace _2_3_ThePlayerClass
{
    public class Program
    {
        public static void Main()
        {
            Window w = new Window("The Player Class", 500, 400);
            Player p = new Player(w);
            SplashKit.ClearScreen(Color.White);
            p.Draw();
            w.Refresh(60);
            SplashKit.Delay(5000);
        }
    }
}
