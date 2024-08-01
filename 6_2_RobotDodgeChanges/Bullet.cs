using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _6_2_RobotDodgeChanges
{
    public class Bullet
    {
        // private variables
        private SplashKitSDK.Point2D _pos;
        private SplashKitSDK.Vector2D _vel;

        // constants
        private const float SPEED = 10;
        private const int WIDTH = 5;

        // public variables
        public SplashKitSDK.Circle collisionCircle { get {
            return SplashKit.CircleAt(_pos, WIDTH);
        }}
        public double x { get { return _pos.X; } }
        public double y { get { return _pos.Y; } }

        // class constructor
        public Bullet(SplashKitSDK.Point2D start, SplashKitSDK.Point2D target)
        {
            _pos = start;
            _vel = SplashKit.VectorMultiply(
                SplashKit.UnitVector(
                    SplashKit.VectorPointToPoint(_pos, target)
                ),
                SPEED
            );
        }

        // draw bullet
        public void Draw()
        {
            SplashKit.FillCircle(Color.Black, _pos.X, _pos.Y, WIDTH);
        }

        // check if bullet has gone off screen
        public bool IsOffScreen(SplashKitSDK.Window limit)
        {
            return (
                (_pos.X < -WIDTH)
                || (_pos.X > limit.Width + WIDTH)
                || (_pos.Y < -WIDTH)
                || (_pos.Y > limit.Height + WIDTH)
            );
        }

        // update bullet position
        public void Update()
        {
            _pos.X += _vel.X;
            _pos.Y += _vel.Y;
        }
    }
}