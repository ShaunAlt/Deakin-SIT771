using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _7_2_DifferentRobots
{
    public class RobotDodge
    {
        // private variables
        private Player _Player;
        private Window _GameWindow;
        private List<Robot> _Robots;

        // public variables
        public bool Quit { get { return _Player.Quit; } }

        // class constructor
        public RobotDodge(Window gameWindow)
        {
            _GameWindow = gameWindow;
            _Player = new Player(_GameWindow);
            _Robots = new List<Robot>();
        }

        // check robot collisions
        public void CheckCollisions()
        {
            // initialize list of robots to remove
            List<Robot> robotsRemove = new List<Robot>();

            // set list of all robots to remove
            foreach (Robot r in _Robots)
            {
                if (
                        (_Player.CollidesWith(r))
                        || (r.IsOffScreen(_GameWindow))
                )
                {
                    robotsRemove.Add(r);
                }
            }

            // remove robots as required
            foreach (Robot r in robotsRemove) { _Robots.Remove(r); }
        }

        // draw the game
        public void Draw()
        {
            _GameWindow.Clear(Color.White);
            foreach (Robot r in _Robots) { r.Draw(); }
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
            // return new Robot(_GameWindow, _Player);
            // return new Boxy(_GameWindow, _Player);
            float val = SplashKit.Rnd();
            if (val < 0.33)
            {
                return new Boxy(_GameWindow, _Player);
            }
            else if (val < 0.66)
            {
                return new Roundy(_GameWindow, _Player);
            }
            else
            {
                return new Robot_2(_GameWindow, _Player);
            }
        }

        // update the game
        public void Update()
        {
            // check collisions
            CheckCollisions();

            // update robots
            foreach (Robot r in _Robots) { r.Update(); }

            // randomly create new robots
            const double SPAWN_CHANCE = 0.01;
            // const double SPAWN_CHANCE = 0.99;
            if (SplashKit.Rnd() < SPAWN_CHANCE) { _Robots.Add(RandomRobot()); }
        }
    }
}