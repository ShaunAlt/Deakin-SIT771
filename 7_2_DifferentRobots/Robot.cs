using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _7_2_DifferentRobots
{
    public abstract class Robot
    {
        // public variables
        public double X;
        public double Y;
        public Vector2D Velocity;
        public Color MainColor;
        public int Width { get { return 50; } }
        public int Height { get { return 50; } }
        public Circle CollisionCircle { get {
            return SplashKit.CircleAt(
                X + (Width / 2),
                Y + (Height / 2),
                20
            );
        }}
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
        public Robot(Window gameWindow, Player player)
        {
            const int SPEED = 4; // speed of the robot

            // set random off-screen robot position
            if (SplashKit.Rnd() < 0.5) // top / bottom
            {
                X = SplashKit.Rnd(gameWindow.Width - Width);
                if (SplashKit.Rnd() < 0.5) { Y = -Height; } // top
                else { Y = gameWindow.Height; } // bottom
            }
            else // left / right
            {
                Y = SplashKit.Rnd(gameWindow.Height - Height);
                if (SplashKit.Rnd() < 0.5) { X = -Width; } // left
                else { X = gameWindow.Width; } // right
            }

            MainColor = Color.RandomRGB(200); // set robot colour

            // set robot velocity
            Vector2D dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(
                pos,
                player.pos
            ));
            Velocity = SplashKit.VectorMultiply(dir, SPEED);
        }

        // draw robot
        public abstract void Draw();

        // update the robot position
        public void Update()
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        // check if the robot has gone off the screen
        public bool IsOffScreen(Window limit)
        {
            return (
                (X < -Width)
                || (X > limit.Width)
                || (Y < -Height)
                || (Y > limit.Height)
            );
        }
    }
    public class Boxy : Robot
    {
        // class constructor
        public Boxy(
                Window gameWindow,
                Player player
        ) : base(gameWindow, player)
        {

        }

        public override void Draw()
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;
            SplashKit.FillRectangle(Color.Gray, X, Y, 50, 50);
            SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }
    public class Roundy : Robot
    {
        // class constructor
        public Roundy(
                Window gameWindow,
                Player player
        ) : base(gameWindow, player)
        {

        }

        public override void Draw()
        {
            double leftX = X + 17;
            double midX = X + 25;
            double rightX = X + 33;
            double midY = Y + 25;
            double eyeY = Y + 20;
            double mouthY = Y + 35;

            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
            SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, mouthY);
        }
    }
    public class Robot_2 : Robot
    {
        // class constructor
        public Robot_2(
                Window gameWindow,
                Player player
        ) : base(gameWindow, player)
        {

        }

        public override void Draw()
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;
            SplashKit.FillRectangle(Color.Black, X, Y, 50, 50);
            SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }
}