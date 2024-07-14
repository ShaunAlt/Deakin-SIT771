using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _4_3_RobotDodge
{
    public class Robot
    {
        // private variables
        private double X, Y;
        private Color MainColor;
        
        // public variables
        public int Width { get { return 50; } }
        public int Height { get { return 50; } }
        public Circle CollisionCircle { get {
            return SplashKit.CircleAt(
                X + (Width / 2),
                Y + (Height / 2),
                20
            );
        }}

        // class constructor
        public Robot(Window gameWindow)
        {
            X = SplashKit.Rnd(gameWindow.Width - Width);
            Y = SplashKit.Rnd(gameWindow.Height - Height);
            MainColor = Color.RandomRGB(200);
        }

        // draw robot
        public void Draw()
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
}