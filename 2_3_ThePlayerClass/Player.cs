using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _2_3_ThePlayerClass
{
    public class Player
    {
        // initialize variables
        private Bitmap _PlayerBitmap;
        public double X { get; private set; }
        public double Y { get; private set; }
        public int Width { get { return _PlayerBitmap.Width; } }
        public int Height { get { return _PlayerBitmap.Height; } }

        // class constructor
        public Player(Window gameWindow) {
            _PlayerBitmap = new Bitmap(
                "Player",
                "Resources/images/Player.png"
            );
            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;
        }

        // draw player
        // public void Draw(Window gameWindow) {
        //     gameWindow.ClearScreen();
        //     gameWindow.DrawBitmap(_PlayerBitmap, X, Y);
        //     gameWindow.Refresh(60);
        // }
        public void Draw() {
            SplashKit.DrawBitmap(_PlayerBitmap, X, Y);
        }
    }
}