using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplashKitSDK;

namespace _6_2_RobotDodgeChanges
{
    public class RobotDodge
    {
        // private variables
        private List<Bullet> _bullets;
        private int _level;
        private Player _player;
        private List<Robot> _robots;
        private int _score;
        private SplashKitSDK.Timer _timer;
        private SplashKitSDK.Window _window;

        // constants
        private const int MAX_LIVES = 5;
        private const double SPAWN_CHANCE = 0.01;

        // public variables
        public int level { get { return _level; } }
        public bool quit { get { return _player.quit; } }
        public int score { get { return _score; } }

        // class constructor
        public RobotDodge(Window w)
        {
            _window = w;
            _bullets = new List<Bullet>();
            _player = new Player(_window, MAX_LIVES);
            _robots = new List<Robot>();
            _level = 1;
            _score = 0;
            _timer = new SplashKitSDK.Timer("Robot Dodge Timer");
            _timer.Start();
        }

        // check robot collisions
        public void CheckCollisions()
        {
            // initialize list of robots to remove
            List<Robot> robotsRemove = new List<Robot>();
            List<Bullet> bulletsRemove = new List<Bullet>();

            // bullets off-screen
            foreach (Bullet b in _bullets)
            {
                if (b.IsOffScreen(_window)) { bulletsRemove.Add(b); }
            }

            // robots off-screen
            foreach (Robot r in _robots)
            {
                if (r.IsOffScreen(_window)) { robotsRemove.Add(r); }
            }

            // robots collide with player
            foreach (Robot r in _robots)
            {
                if (_player.CollidesWith(r))
                {
                    _player.lives--;
                    robotsRemove.Add(r);
                }
            }

            // robots collide with bullets
            foreach (Robot r in _robots)
            {
                foreach (Bullet b in _bullets)
                {
                    if (
                            SplashKit.CirclesIntersect(
                                b.collisionCircle,
                                r.collisionCircle
                            )
                    )
                    {
                        bulletsRemove.Add(b);
                        robotsRemove.Add(r);
                    }
                }
            }

            // remove bullets from list
            foreach (Bullet b in bulletsRemove) { _bullets.Remove(b); }

            // remove robots from list
            foreach (Robot r in robotsRemove) { _robots.Remove(r); }
        }

        // draw the game
        public void Draw()
        {
            _window.Clear(Color.White);
            foreach (Robot r in _robots) { r.Draw(); }
            _player.Draw();
            foreach (Bullet b in _bullets) { b.Draw(); }
            DrawStats();
            _window.Refresh(60);
        }

        public void DrawStats()
        {
            // write lives text
            SplashKit.DrawText(
                $"Lives Remaining: {_player.lives}",
                Color.Black,
                "TechnoRaceItalic-eZRWe.otf",
                15,
                _window.Width - 130,
                10
            );

            // write score text
            SplashKit.DrawText(
                $"Score: {_score}",
                Color.Black,
                "TechnoRaceItalic-eZRWe.otf",
                15,
                10,
                10
            );

            // write level text
            SplashKit.DrawText(
                $"Level: {_level}",
                Color.Black,
                "TechnoRaceItalic-eZRWe.otf",
                15,
                10,
                35
            );

            // draw life circles
            for (int i = 0; i < MAX_LIVES; i++)
            {
                if (i < _player.lives)
                {
                    SplashKit.FillCircle(
                        Color.Red,
                        _window.Width - ((i * 20) + 20),
                        40,
                        8
                    );
                }
                SplashKit.DrawCircle(
                    Color.Black,
                    _window.Width - ((i * 20) + 20),
                    40,
                    8
                );
            }
        }

        // handle event inputs
        public void HandleInput()
        {
            _player.HandleInput();
            _player.StayOnWindow(_window);

            // create new bullet if left mouse clicked
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _bullets.Add(new Bullet(
                    _player.pos,
                    SplashKit.MousePosition()
                ));
            }
        }

        // create a new random robot
        public Robot RandomRobot()
        {
            float val = SplashKit.Rnd();
            if (val < 0.33)
            {
                return new Boxy(_window, _player);
            }
            else if (val < 0.66)
            {
                return new Roundy(_window, _player);
            }
            else
            {
                return new Robot_2(_window, _player);
            }
        }

        // update the game
        public void Update()
        {
            // check collisions
            CheckCollisions();

            // update robots
            foreach (Robot r in _robots) { r.Update(); }

            // update bullets
            foreach (Bullet b in _bullets) { b.Update(); }

            // randomly create new robots
            if (SplashKit.Rnd() < SPAWN_CHANCE * _level)
            {
                _robots.Add(RandomRobot());
            }

            // update score and level
            if (_timer.Ticks > 1000)
            {
                _score++;
                SplashKit.ResetTimer("Robot Dodge Timer");
                if (_score % 5 == 0) { _level++; }
            }
        }
    }
}