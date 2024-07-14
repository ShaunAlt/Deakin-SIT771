using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _4_3_RobotDodge
{
    public class RobotDodge
    {
        // private variables
        private Player _Player;
        private Window _GameWindow;
        private Robot _TestRobot;

        // public variables
        public bool Quit { get { return _Player.Quit; } }

        // class constructor
        public RobotDodge(Window gameWindow)
        {
            _GameWindow = gameWindow;
            _Player = new Player(_GameWindow);
            _TestRobot = RandomRobot();
        }

        // draw the game
        public void Draw()
        {
            _GameWindow.Clear(Color.White);
            _TestRobot.Draw();
            _Player.Draw();
            _GameWindow.Refresh(60);
        }

        // handle keyboard inputs
        public void HandleInput()
        {
            _Player.HandleInput();
            _Player.StayOnWindow(_GameWindow);
        }

        // create a new random robot
        public Robot RandomRobot()
        {
            return new Robot(_GameWindow);
        }

        // update the game
        public void Update()
        {
            if (_Player.CollidesWith(_TestRobot))
            {
                _TestRobot = RandomRobot();
            }
        }
    }
}