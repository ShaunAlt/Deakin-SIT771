using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _6_2_RobotDodgeChanges
{
    public class Player
    {
        // private variables
        private SplashKitSDK.Bitmap _bitmap;
        private int _lives;
        private SplashKitSDK.Point2D _pos;
        private bool _quit;
        private SplashKitSDK.Vector2D _vel;

        // constants
        private const double ACC = 2.0;
        private const string BITMAP_FILENAME = "Resources/images/Player.png";
        private const string BITMAP_NAME = "Player";
        private const int BORDER = 10;
        private const double FRICTION = 0.1;
        private const double MAX_SPEED = 4.0;

        // public variables
        public int height { get { return _bitmap.Height; } }
        public int lives {
            get { return _lives; }
            set {
                _lives = value;
                if (_lives <= 0) { _quit = true; }
            }
        }
        public SplashKitSDK.Point2D pos { get {
            return new SplashKitSDK.Point2D()
            {
                X = _pos.X + (width / 2),
                Y = _pos.Y + (height / 2)
            };
        }}
        public bool quit { get { return _quit; } }
        public int width { get { return _bitmap.Width; } }
        public double x { get { return _pos.X; } }
        public double y { get { return _pos.Y; } }

        // class constructor
        public Player(SplashKitSDK.Window w, int numLives) {
            _bitmap = new SplashKitSDK.Bitmap(BITMAP_NAME, BITMAP_FILENAME);
            _lives = numLives;
            _pos = new SplashKitSDK.Point2D()
            {
                X = (w.Width - width) / 2,
                Y = (w.Height - height) / 2
            };
            _quit = false;
            _vel = new SplashKitSDK.Vector2D(){ X = 0, Y = 0 };
        }

        // check collisions
        public bool CollidesWith(Robot r)
        {
            return _bitmap.CircleCollision(x, y, r.collisionCircle);
        }

        // draw player
        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, x, y);
        }

        // handle keypad presses
        public void HandleInput() {
            // set if speed + acceleration is doubled
            bool extra_speed = SplashKit.KeyDown(KeyCode.SpaceKey);
            double acc_mag = ACC;
            double max_speed = MAX_SPEED;
            if (extra_speed) {
                acc_mag *= 2;
                max_speed *= 2;
            }

            // define acceleration
            Vector2D acc = new Vector2D() { X = 0, Y = 0 };
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.WKey)) { acc.Y -= 1; }
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.SKey)) { acc.Y += 1; }
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.AKey)) { acc.X -= 1; }
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.DKey)) { acc.X += 1; }

            // set acceleration vector to required magnitude
            acc = SplashKit.VectorMultiply(SplashKit.UnitVector(acc), acc_mag);

            // calculate new velocity
            _vel = SplashKit.VectorAdd(_vel, acc);

            // apply friction
            _vel = SplashKit.VectorMultiply(_vel, 1 - FRICTION);

            // limit speed
            if (SplashKit.VectorMagnitude(_vel) > max_speed)
            {
                _vel = SplashKit.VectorMultiply(
                    SplashKit.UnitVector(_vel),
                    max_speed
                );
            }

            // update position from speed
            _pos.X += _vel.X;
            _pos.Y += _vel.Y;

            // handle escape key press
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.EscapeKey))
            { _quit = true; }
        }

        // keep player on window
        public void StayOnWindow(SplashKitSDK.Window limit) {
            // horizontal check
            if (x < BORDER) { _pos.X = BORDER; }
            else if (x > limit.Width - _bitmap.Width - BORDER) {
                _pos.X = limit.Width - _bitmap.Width - BORDER;
            }
            // vertical check
            if (y < BORDER) { _pos.Y = BORDER; }
            else if (y > limit.Height - _bitmap.Height - BORDER) {
                _pos.Y = limit.Height - _bitmap.Height - BORDER;
            }
        }
    }
}