using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _7_2_DifferentRobots
{
    public class Player
    {
        // constants
        const int SPEED = 3;
        const int GAP = 10;

        // initialize variables
        private Bitmap _PlayerBitmap;
        public double X { get; private set; }
        public double Y { get; private set; }
        public int Width { get { return _PlayerBitmap.Width; } }
        public int Height { get { return _PlayerBitmap.Height; } }
        public bool Quit { get; private set; }
        public Point2D pos // current position of the player
        {
            get
            {
                return new Point2D()
                {
                    X = X,
                    Y = Y
                };
            }
        }

        // class constructor
        public Player(Window gameWindow) {
            _PlayerBitmap = new Bitmap(
                "Player",
                "Resources/images/Player.png"
            );
            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;
            Quit = false;
        }

        // check collisions
        public bool CollidesWith(Robot r)
        {
            return _PlayerBitmap.CircleCollision(X, Y, r.CollisionCircle);
        }

        // draw player
        public void Draw() {
            SplashKit.DrawBitmap(_PlayerBitmap, X, Y);
        }

        // handle keypad presses
        public void HandleInput() {
            // handle space bar press
            int s = SPEED;
            if (SplashKit.KeyDown(KeyCode.SpaceKey)) {s *= 2;}

            // handle arrow key presses
            if (SplashKit.KeyDown(KeyCode.LeftKey)) {
                X -= s;
            }
            if (SplashKit.KeyDown(KeyCode.RightKey)) {
                X += s;
            }
            if (SplashKit.KeyDown(KeyCode.UpKey)) {
                Y -= s;
            }
            if (SplashKit.KeyDown(KeyCode.DownKey)) {
                Y += s;
            }

            // handle escape key press
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) { Quit = true; }
        }

        // keep player on window
        public void StayOnWindow(Window limit) {
            // horizontal check
            if (X < GAP) { X = GAP; }
            else if (X > limit.Width - _PlayerBitmap.Width - GAP) {
                X = limit.Width - _PlayerBitmap.Width - GAP;
            }
            // vertical check
            if (Y < GAP) { Y = GAP; }
            else if (Y > limit.Height - _PlayerBitmap.Height - GAP) {
                Y = limit.Height - _PlayerBitmap.Height - GAP;
            }
        }
    }
}