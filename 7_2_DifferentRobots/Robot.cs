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
}