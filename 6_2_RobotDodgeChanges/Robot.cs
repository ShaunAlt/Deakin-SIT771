using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _6_2_RobotDodgeChanges
{
    public abstract class Robot
    {
        // private variables
        private SplashKitSDK.Point2D _pos;
        private SplashKitSDK.Vector2D _vel;

        // protected variables
        protected SplashKitSDK.Color _col;

        // constants
        private const int HEIGHT = 50;
        private const double MAX_SPEED = 4.0;
        private const double MIN_SPEED = 1.0;
        private const int WIDTH = 50;

        // public variables
        public SplashKitSDK.Circle collisionCircle { get {
            return SplashKit.CircleAt(
                _pos.X + (WIDTH / 2),
                _pos.Y + (HEIGHT / 2),
                20
            );
        }}
        public double x { get { return _pos.X; } }
        public double y { get { return _pos.Y; } }

        // class constructor
        public Robot(SplashKitSDK.Window w, Player p)
        {
            // set random colour
            _col = Color.RandomRGB(200);

            // set random off-screen robot position
            _pos = new SplashKitSDK.Point2D(){ X = 0, Y = 0 };
            int start_side = SplashKit.Rnd(1, 4); // side to start on
            if (start_side == 1) // left
            {
                _pos.X = -WIDTH;
                _pos.Y = SplashKit.Rnd(-HEIGHT, w.Height + HEIGHT);
            }
            else if (start_side == 2) // right
            {
                _pos.X = w.Width + WIDTH;
                _pos.Y = SplashKit.Rnd(-HEIGHT, w.Height + HEIGHT);
            }
            else if (start_side == 3) // top
            {
                _pos.Y = -HEIGHT;
                _pos.X = SplashKit.Rnd(-WIDTH, w.Width + WIDTH);
            }
            else // bottom
            {
                _pos.Y = w.Height + HEIGHT;
                _pos.X = SplashKit.Rnd(-WIDTH, w.Width + WIDTH);
            }

            // set random speed
            double spd = (SplashKit.Rnd()*(MAX_SPEED-MIN_SPEED)) + MIN_SPEED;

            // set robot velocity - targeted at player
            _vel = SplashKit.VectorMultiply(
                SplashKit.UnitVector(
                    SplashKit.VectorPointToPoint(
                        _pos,
                        p.pos
                    )
                ),
                spd
            );
        }

        // draw robot
        public abstract void Draw();

        // update the robot position
        public void Update()
        {
            _pos.X += _vel.X;
            _pos.Y += _vel.Y;
        }

        // check if the robot has gone off the screen
        public bool IsOffScreen(SplashKitSDK.Window limit)
        {
            return (
                (x < -WIDTH)
                || (x > limit.Width + WIDTH)
                || (y < -HEIGHT)
                || (y > limit.Height + HEIGHT)
            );
        }
    }

    public class Boxy : Robot
    {
        // class constructor
        public Boxy(
                SplashKitSDK.Window w,
                Player p
        ) : base(w, p)
        {

        }

        public override void Draw()
        {
            double leftX = x + 12;
            double rightX = x + 27;
            double eyeY = y + 10;
            double mouthY = y + 30;
            SplashKit.FillRectangle(Color.Gray, x, y, 50, 50);
            SplashKit.FillRectangle(_col, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(_col, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(_col, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(_col, leftX + 2, mouthY + 2, 21, 6);
        }
    }

    public class Roundy : Robot
    {
        // class constructor
        public Roundy(
                SplashKitSDK.Window w,
                Player p
        ) : base(w, p)
        {

        }

        public override void Draw()
        {
            double leftX = x + 17;
            double midX = x + 25;
            double rightX = x + 33;
            double midY = y + 25;
            double eyeY = y + 20;
            double mouthY = y + 35;

            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(_col, leftX, eyeY, 5);
            SplashKit.FillCircle(_col, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, x, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, x, mouthY, x + 50, mouthY);
        }
    }

    public class Robot_2 : Robot
    {
        // class constructor
        public Robot_2(
                SplashKitSDK.Window w,
                Player p
        ) : base(w, p)
        {

        }

        public override void Draw()
        {
            double leftX = x + 12;
            double rightX = x + 27;
            double eyeY = y + 10;
            double mouthY = y + 30;
            SplashKit.FillRectangle(Color.Black, x, y, 50, 50);
            SplashKit.FillRectangle(_col, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(_col, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(_col, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(_col, leftX + 2, mouthY + 2, 21, 6);
        }
    }
}