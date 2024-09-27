/* ****************************************************************************
 * 7.3 - Custom Program Code
 * Platformer Game.
 * 
 * The game features:
 * - A player character with a jumping and moving animation.
 * - Platforms that the player can jump on.
 * - Enemies that the player has to avoid.
 * - Power-ups that the player can collect.
 * - Sound effects and music.
 * - Score tracking and display.
 * - Levels with varying difficulty levels and obstacles.
 * - A pause menu with options to resume, restart, or quit the game.
 * - A game over screen with a score and option to play again.
 * - A high score system that saves and loads the top scores.
 * - Customizable game settings such as difficulty, sound effects, and music volume.
 * - A tutorial screen that explains the game's controls and rules.
 * 
 * Author: Shaun Altmann

 All "Attributes / Fields" are private. All "Constants" are private. All properties are public read-only.
 All methods are commented on if they are public or private.
 * ***************************************************************************/

using System;
using SplashKitSDK;
using System.Collections;
using System.Collections.Generic;

namespace _7_3_CustomProgramCode
{
    /* ************************************************************************
     * Game Account
     * ***********************************************************************/
    public class Account
    {
        /*
        Game Account
        -
        Represents a game account that can save and load high scores.

        Attributes / Fields
        -
        - _name : `string`
            - Name of the game account.
        - _scores : `List<Score>`
            - Collection of scores the player has achieved in the game.

        Constants
        -
        None

        Methods
        -
        - Account(name) : `Account`
            - Public Constructor Method.
            - Creates a new player account with the specified name.

        Properties
        -
        - name : `string`
            - Name of the game account.
        - scores : `List<Score>`
            - Collection of scores the player has achieved in the game.
        */

        // **********
        // Attributes
        public static List<Account> accounts = new List<Account>();
        private string _name;
        private List<Score> _scores;

        // **********
        // Properties
        public string name { get { return _name; } }
        public List<Score> scores { get { return _scores; } }

        // ***********
        // Constructor
        public Account(string name)
        {
            // set the player name
            _name = name;

            // initialize the scores of the player
            _scores = new List<Score>();
        }

        public void AddScore(LevelNumber lvl, int score)
        {
            if (score > 0) { _scores.Add(new Score(score, lvl)); }
        }

        public static Account GetAccount(string name)
        {
            foreach(Account a in accounts)
            {
                if (a.name == name)
                {
                    return a;
                }
            }
            Account na = new Account(name);
            accounts.Add(na);
            return na;
        }

        public static List<Score> GetScores(LevelNumber lvl, int num_max = 3)
        {
            /// <summary>
            /// Get the top <num_max> scores for a particular level
            /// </summary>
            
            // get all scores for that particular level
            List<Score> scores = new List<Score>();
            foreach (Account a in accounts)
            {
                foreach (Score s in a.scores)
                {
                    if (s.level == lvl) { scores.Add(s); }
                }
            }

            // sort the scores in descending order based on score
            List<Score> scores_sorted = scores.OrderBy(s1 => s1.score).ToList();

            List<Score> scores_sorted_slice = new List<Score>();
            for (int i = 0; i < num_max; i++)
            {
                if (i < scores_sorted.Count) { scores_sorted_slice.Add(scores_sorted[i]); }
                else { break; }
            }
            return scores_sorted_slice;
        }

        public Score? GetScore(LevelNumber lvl)
        {
            /// <summary>
            /// Tries to get the high score for the given level for the current accoount
            /// </summary>

            Score? bestscore = null;
            foreach(Score s in scores)
            {
                if (
                        (s.level == lvl)
                        && (
                            (bestscore == null)
                            || (s.score > bestscore.score)
                        )
                ) { bestscore = s; }
            }
            return bestscore;
        }
    }

    /* ************************************************************************
     * Individual Level Class
     * ***********************************************************************/
    public class Level
    {
        public List<Platform> platforms;
        public Player player;
        public Goal goal;

        public Level(LevelNumber lvl, Window w)
        {
            // create the sprites for the level based on the level number
            player = new Player(600, 400, w);
            switch (lvl)
            {
                case LevelNumber.L0:
                    platforms = new List<Platform>(){
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 500, PlatformType.Large90, w),
                        new Platform(0, 700, PlatformType.Large, w),
                        new Platform(200, 700, PlatformType.Large, w),
                        new Platform(400, 700, PlatformType.Large, w),
                        new Platform(600, 700, PlatformType.Large, w),
                        new Platform(800, 700, PlatformType.Large, w),
                        new Platform(1000, 700, PlatformType.Large, w),
                        new Platform(1200, 700, PlatformType.Large, w),
                        new Platform(1400, 700, PlatformType.Large, w),
                        new Platform(1600, 700, PlatformType.Large, w),
                        new Platform(1800, 700, PlatformType.Large, w),
                        new Platform(2000, 700, PlatformType.Large, w),
                        new Platform(2200, 700, PlatformType.Large, w),
                        new Platform(2400, 700, PlatformType.Large, w),
                        new Platform(2600, 700, PlatformType.Large, w),
                        new Platform(2800, 700, PlatformType.Large, w),
                        new Platform(3000, 700, PlatformType.Large, w),
                        new Platform(3195, 500, PlatformType.Large90, w),
                        new Platform(3195, 300, PlatformType.Large90, w),
                        new Platform(3195, 100, PlatformType.Large90, w),
                        new Platform(3195, -100, PlatformType.Large90, w),
                        new Platform(3195, -300, PlatformType.Large90, w),
                        new Platform(3195, -500, PlatformType.Large90, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1200, 300, PlatformType.Large, w),
                        new Platform(1400, 300, PlatformType.Large, w),
                        new Platform(1800, 100, PlatformType.Large, w),
                        new Platform(2000, 100, PlatformType.Large, w),
                        new Platform(2400, -100, PlatformType.Large, w),
                        new Platform(2600, -100, PlatformType.Large, w),
                    };
                    goal = new Goal(2600, -200, w);
                    break;
                case LevelNumber.L1:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L2:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L3:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L4:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L5:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L6:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L7:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L8:
                    platforms = new List<Platform>(){

                    };
                    break;
                case LevelNumber.L9:
                    platforms = new List<Platform>(){

                    };
                    break;
            }
        }

        public List<Sprite> GetNPCS()
        {
            List<Sprite> sprites = new List<Sprite>();
            foreach (Platform p in platforms) { sprites.Add(p); }
            sprites.Add(goal);
            return sprites;
        }

        public static int PlayLevel(LevelNumber levelnum, Window w)
        {
            // play a particular level and return the score the player got
            Level l = new Level(levelnum, w);

            // initialize score
            int score = 10000;
            Vector2D vel = new Vector2D(){ X = 0, Y = 0 };

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // escape key
                if (SplashKit.KeyDown(SplashKitSDK.KeyCode.EscapeKey))
                {
                    return -2;
                }

                // update left / right
                double acc_x = 0;
                if (SplashKit.KeyDown(SplashKitSDK.KeyCode.RightKey)) {
                    // right
                    acc_x -= 0.35;
                }
                if (SplashKit.KeyDown(SplashKitSDK.KeyCode.LeftKey)) {
                    // right
                    acc_x += 0.35;
                }
                // vel.X = Math.Min(3, Math.Max(-3, vel.X + acc_x));
                vel.X = vel.X + acc_x;
                // friction
                vel.X = vel.X * 0.95;
                foreach (Sprite s in l.GetNPCS()) { s.Update(vel.X, 0); }
                bool reverseX = false;
                foreach (Platform p in l.platforms) {
                    if (l.player.CheckCollision(p)) {
                        reverseX = true;
                        break;
                    }
                }
                if (reverseX) {
                    foreach (Sprite s in l.GetNPCS()) { s.Update(vel.X, 0, true); }
                    vel.X = 0;
                }

                // update up/down
                double acc_y = 0;
                if (SplashKit.KeyDown(SplashKitSDK.KeyCode.UpKey)) {
                    // jump
                    if (vel.Y == 0) {
                        acc_y = 15;
                    }
                }
                // gravity
                acc_y -= 0.251;
                // update velocity from acceleration
                // vel.Y = Math.Min(10, Math.Max(-10, vel.Y + acc_y));
                vel.Y = (vel.Y + acc_y) * 0.99;
                // update positions
                foreach (Sprite s in l.GetNPCS()) { s.Update(0, vel.Y); }
                // check collisions and teleport if required
                bool reverseY = false;
                foreach (Platform p in l.platforms) {
                    if (l.player.CheckCollision(p)) {
                        reverseY = true;
                        break;
                    }
                }
                if (reverseY) {
                    foreach (Sprite s in l.GetNPCS()) { s.Update(0, vel.Y, true); }
                    vel.Y = 0;
                }

                // draw sprites
                l.player.Draw();
                foreach (Sprite s in l.GetNPCS()) { s.Draw(); }

                // if reached target - return score
                if (l.player.CheckCollision(l.goal)) { return score; }

                score--; // decrease score based on time
                w.Refresh(60);
            }

            // window was closed - return invalid score
            return -1;
        }
    }

    public abstract class Sprite
    {
        protected Bitmap _bitmap;
        protected Point2D _pos;
        protected Window _window;
        public Bitmap bitmap { get { return _bitmap; } }
        public Point2D pos { get { return _pos; } }
        public double height { get { return _bitmap.Height; } }
        public double width { get { return _bitmap.Width; } }
        public Sprite(
            float x,
            float y,
            string bitmap_name,
            Window window
        )
        {
            _window = window;
            _bitmap = new Bitmap(
                bitmap_name.Split('/').Last().Split('.')[0], // image name
                bitmap_name // file directory + name
            );
            _pos = new Point2D(){ X = x, Y = y };
        }
        public bool CheckCollision(Sprite other)
        {
            return SplashKit.BitmapCollision(
                _bitmap,
                _pos,
                other.bitmap,
                other.pos
            );
        }
        public void Draw()
        {
            SplashKit.DrawBitmapOnWindow(
                _window,
                _bitmap,
                _pos.X,
                _pos.Y
            );
        }
        public virtual void Update(double delta_x, double delta_y, bool reverse = false)
        {
            _pos.X += (delta_x * (reverse ? -1 : 1));
            _pos.Y += (delta_y * (reverse ? -1 : 1));
        }
        public virtual void Update(Vector2D delta, bool reverse = false)
        {
            _pos.X += (delta.X * (reverse ? -1 : 1));
            _pos.Y += (delta.Y * (reverse ? -1 : 1));
        }
    }
    public enum PlatformType {
        Tiny,
        Tiny90,
        Small,
        Small90,
        Medium,
        Medium90,
        Large,
        Large90
    }

    public class Platform : Sprite
    {
        public Platform(
                float x,
                float y,
                PlatformType type_,
                Window window
        ) : base(x, y, GetName(type_), window)
        { }
        private static string GetName(PlatformType type_)
        {
            switch (type_)
            {
                case PlatformType.Tiny:
                    return "Resources/images/Platform v2/Platform - Tiny v2.png";
                case PlatformType.Tiny90:
                    return "Resources/images/Platform v2/Platform - Tiny v2 90.png";
                case PlatformType.Small:
                    return "Resources/images/Platform v2/Platform - Sml v2.png";
                case PlatformType.Small90:
                    return "Resources/images/Platform v2/Platform - Sml v2 90.png";
                case PlatformType.Medium:
                    return "Resources/images/Platform v2/Platform - Med v2.png";
                case PlatformType.Medium90:
                    return "Resources/images/Platform v2/Platform - Med v2 90.png";
                case PlatformType.Large:
                    return "Resources/images/Platform v2/Platform - Lrg v2.png";
                case PlatformType.Large90:
                    return "Resources/images/Platform v2/Platform - Lrg v2 90.png";
                default:
                    throw new ArgumentException("Invalid platform type");
            }
        }
    }

    public class Player : Sprite
    {
        public Player(
            float x,
            float y,
            Window window
        ) : base(x, y, "Resources/images/Player.png", window)
        {
        }
    }

    public class Goal : Sprite
    {
        public Goal(
            float x,
            float y,
            Window window
        ) : base(x, y, "Resources/images/Target2.png", window)
        {

        }
    }

    /* ************************************************************************
     * Level Number Enumerator
     * ***********************************************************************/
    public enum LevelNumber
    {
        L0,
        L1,
        L2,
        L3,
        L4,
        L5,
        L6,
        L7,
        L8,
        L9,
    }
    public enum Screen {
        Quit, // quit and save data
        Login, // used to logging in a player
        Main, // used for showing the main menu
        PlayMenu, // used for selecting which level to play
        Scores, // used to display user and overall high scores
        Settings, // used to display the settings
        Lvl0,
        Lvl1,
        Lvl2,
        Lvl3,
        Lvl4,
        Lvl5,
        Lvl6,
        Lvl7,
        Lvl8,
        Lvl9,
    }

    public class Program
    {
        public static void Main()
        {
            // create window
            Window w = new Window("Game Window", 1200, 800);

            // initialize screen
            // Screen s = Screen.Login;
            Screen s = Screen.Lvl0; // testing

            // initialize account logged in
            // Account? a = null;
            Account? a = Account.GetAccount("test"); // testing

            int lvl_score;

            // Main loop to update the windows
            while ((!w.CloseRequested) && (s != Screen.Quit))
            {
                switch (s)
                {
                    case Screen.Login:
                        a = ScreenLogin(w);
                        Console.WriteLine($"{Account.accounts}");
                        if (a != null) { s = Screen.Main; }
                        else { s = Screen.Quit; }
                        break;
                    case Screen.Main:
                        // draw main menu
                        if (a == null) { s = Screen.Login; break; }
                        s = ScreenMain(a, w);
                        break;
                    case Screen.PlayMenu:
                        // draw level selection menu
                        if (a == null) { s = Screen.Login; break; }
                        s = ScreenPlayMenu(a, w);
                        break;
                    case Screen.Scores:
                        // draw user and overall high scores
                        if (a == null) { s = Screen.Login; break; }
                        s = ScreenScores(a, w);
                        break;
                    case Screen.Settings:
                        // draw controller settings
                        s = ScreenSettings(w);
                        break;
                    case Screen.Lvl0:
                        // play level 0
                        if (a == null) { s = Screen.Login; break; }
                        lvl_score = Level.PlayLevel(LevelNumber.L0, w);
                        if (lvl_score == -1) { s = Screen.Quit; }
                        else if (lvl_score == -2) {
                            // escaped
                            s = Screen.PlayMenu;
                        }
                        else {
                            a.AddScore(LevelNumber.L0, lvl_score);
                            s = Screen.PlayMenu;
                        }

                        // a.AddScore(
                        //     LevelNumber.L0,
                        //     Level.PlayLevel(LevelNumber.L0, w)
                        // );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl1:
                        // play level 1
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L1,
                            Level.PlayLevel(LevelNumber.L1, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl2:
                        // play level 2
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L2,
                            Level.PlayLevel(LevelNumber.L2, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl3:
                        // play level 3
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L3,
                            Level.PlayLevel(LevelNumber.L3, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl4:
                        // play level 4
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L4,
                            Level.PlayLevel(LevelNumber.L4, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl5:
                        // play level 5
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L5,
                            Level.PlayLevel(LevelNumber.L5, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl6:
                        // play level 6
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L6,
                            Level.PlayLevel(LevelNumber.L6, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl7:
                        // play level 7
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L7,
                            Level.PlayLevel(LevelNumber.L7, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl8:
                        // play level 8
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L8,
                            Level.PlayLevel(LevelNumber.L8, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl9:
                        // play level 9
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L9,
                            Level.PlayLevel(LevelNumber.L9, w)
                        );
                        s = Screen.PlayMenu;
                        break;
                }
            }

            // Close game window
            w.Close();
        }

        public static Account? ScreenLogin(Window w)
        {
            // create screen ui elements
            UITextBox t1 = new UITextBox("Enter Name", 600, 300, 200, 40, w);
            UIButton b1 = new UIButton("Login", 600, 400, 200, 60, w);
            UIButton b2 = new UIButton("Exit", 600, 500, 200, 60, w);
            List<UIBase> ui = new List<UIBase>(){t1, b1, b2};

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // update and draw UI elements
                foreach (UIBase uiElement in ui)
                {
                    uiElement.Update();
                    uiElement.Draw();
                }

                // process button click
                if (b1.Clicked() || t1.submitted) {
                    if (string.IsNullOrEmpty(t1.data)) { t1.Reset(); }
                    else { return Account.GetAccount(t1.data); }
                }
                if (b2.Clicked()) { return null; }

                // refresh window
                w.Refresh();
            }

            return null;
        }

        public static Screen ScreenMain(Account a, Window w)
        {
            // create screen ui elements
            UIButton b_play = new UIButton("Play", 450, 420, 200, 60, w);
            UIButton b_scores = new UIButton("Highscores", 750, 420, 200, 60, w);
            UIButton b_settings = new UIButton("Settings", 450, 520, 200, 60, w);
            UIButton b_logout = new UIButton("Logout", 750, 520, 200, 60, w);
            UIText t_welcome = new UIText($"Welcome {a.name} to My Platformer Game", 600, 150, 400, 60, w);

            List<UIBase> ui = new List<UIBase>(){b_play, b_scores, b_settings, b_logout, t_welcome};

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // update and draw UI elements
                foreach (UIBase uiElement in ui)
                {
                    uiElement.Update();
                    uiElement.Draw();
                }

                // process button click
                if (b_play.Clicked()) { return Screen.PlayMenu; }
                if (b_scores.Clicked()) { return Screen.Scores; }
                if (b_settings.Clicked()) { return Screen.Settings; }
                if (b_logout.Clicked()) { return Screen.Login; }

                // refresh window
                w.Refresh();
            }
            return Screen.Quit;
        }

        public static Screen ScreenPlayMenu(Account a, Window w)
        {
            // create back button
            UIButton b_back = new UIButton("Back", 600, 750, 200, 60, w);
            UIButton b_lvl0 = new UIButton("Play Level 0", 120, 175, 200, 60, w);
            UIButton b_lvl1 = new UIButton("Play Level 1", 360, 175, 200, 60, w);
            UIButton b_lvl2 = new UIButton("Play Level 2", 600, 175, 200, 60, w);
            UIButton b_lvl3 = new UIButton("Play Level 3", 840, 175, 200, 60, w);
            UIButton b_lvl4 = new UIButton("Play Level 4", 1080, 175, 200, 60, w);
            UIButton b_lvl5 = new UIButton("Play Level 5", 120, 525, 200, 60, w);
            UIButton b_lvl6 = new UIButton("Play Level 6", 360, 525, 200, 60, w);
            UIButton b_lvl7 = new UIButton("Play Level 7", 600, 525, 200, 60, w);
            UIButton b_lvl8 = new UIButton("Play Level 8", 840, 525, 200, 60, w);
            UIButton b_lvl9 = new UIButton("Play Level 9", 1080, 525, 200, 60, w);
            List<UIBase> ui = new List<UIBase>(){
                b_back,
                b_lvl0,
                b_lvl1,
                b_lvl2,
                b_lvl3,
                b_lvl4,
                b_lvl5,
                b_lvl6,
                b_lvl7,
                b_lvl8,
                b_lvl9
            };

            // create screen ui text elements
            List<LevelNumber> lvls = new List<LevelNumber>()
            {
                LevelNumber.L0, LevelNumber.L1, LevelNumber.L2, LevelNumber.L3, LevelNumber.L4,
                LevelNumber.L5, LevelNumber.L6, LevelNumber.L7, LevelNumber.L8, LevelNumber.L9
            };
            int[] x_vals = new int[] { 120, 360, 600, 840, 1080 };
            int[] y_vals = new int[] { 175, 525 };
            for (int i = 0; i < y_vals.Length; i++)
            {
                for (int j = 0; j < x_vals.Length; j++)
                {
                    int n = (5*i)+j;
                    Score? hs = a.GetScore(lvls[n]);
                    List<Score> hs_list = Account.GetScores(lvls[n]);
                    ui.Add(new UIText($"Level {n}", x_vals[j], y_vals[i] - 125, 240, 40, w));
                    ui.Add(new UIText("Your Best:", x_vals[j], y_vals[i] - 85, 240, 30, w));
                    ui.Add(new UIText(hs != null ? $"{hs.score}" : "Not Set", x_vals[j], y_vals[i] - 65, 240, 30, w));
                }
            }

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // update and draw UI elements
                foreach (UIBase uiElement in ui)
                {
                    uiElement.Update();
                    uiElement.Draw();
                }

                // process button click
                if (b_back.Clicked()) { return Screen.Main; }
                if (b_lvl0.Clicked()) { return Screen.Lvl0; }
                if (b_lvl1.Clicked()) { return Screen.Lvl1; }
                if (b_lvl2.Clicked()) { return Screen.Lvl2; }
                if (b_lvl3.Clicked()) { return Screen.Lvl3; }
                if (b_lvl4.Clicked()) { return Screen.Lvl4; }
                if (b_lvl5.Clicked()) { return Screen.Lvl5; }
                if (b_lvl6.Clicked()) { return Screen.Lvl6; }
                if (b_lvl7.Clicked()) { return Screen.Lvl7; }
                if (b_lvl8.Clicked()) { return Screen.Lvl8; }
                if (b_lvl9.Clicked()) { return Screen.Lvl9; }

                // refresh window
                w.Refresh();
            }
            return Screen.Quit;
        }

        public static Screen ScreenScores(Account a, Window w)
        {
            // create back button
            UIButton b_back = new UIButton("Back", 600, 750, 200, 60, w);
            List<UIBase> ui = new List<UIBase>(){b_back};

            // create screen ui text elements
            List<LevelNumber> lvls = new List<LevelNumber>()
            {
                LevelNumber.L0, LevelNumber.L1, LevelNumber.L2, LevelNumber.L3, LevelNumber.L4,
                LevelNumber.L5, LevelNumber.L6, LevelNumber.L7, LevelNumber.L8, LevelNumber.L9
            };
            int[] x_vals = new int[] { 120, 360, 600, 840, 1080 };
            int[] y_vals = new int[] { 175, 525 };
            for (int i = 0; i < y_vals.Length; i++)
            {
                for (int j = 0; j < x_vals.Length; j++)
                {
                    int n = (5*i)+j;
                    Score? hs = a.GetScore(lvls[n]);
                    List<Score> hs_list = Account.GetScores(lvls[n]);
                    ui.Add(new UIText($"Level {n}", x_vals[j], y_vals[i] - 125, 240, 40, w));
                    ui.Add(new UIText("Your Best:", x_vals[j], y_vals[i] - 85, 240, 30, w));
                    ui.Add(new UIText(hs != null ? $"{hs.score}" : "Not Set", x_vals[j], y_vals[i] - 65, 240, 30, w));
                    ui.Add(new UIText("High Scores:", x_vals[j], y_vals[i] - 20, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 1 ? $"1st: {hs_list[0].score}" : "1st: Not Set", x_vals[j], y_vals[i] + 10, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 2 ? $"2nd: {hs_list[0].score}" : "2nd: Not Set", x_vals[j], y_vals[i] + 40, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 3 ? $"3rd: {hs_list[0].score}" : "3rd: Not Set", x_vals[j], y_vals[i] + 70, 240, 30, w));
                }
            }

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // update and draw UI elements
                foreach (UIBase uiElement in ui)
                {
                    uiElement.Update();
                    uiElement.Draw();
                }

                // process button click
                if (b_back.Clicked()) { return Screen.Main; }

                // refresh window
                w.Refresh();
            }
            return Screen.Quit;
        }

        public static Screen ScreenSettings(Window w)
        {
            // create back button
            UIButton b_back = new UIButton("Back", 600, 750, 200, 60, w);
            List<UIBase> ui = new List<UIBase>(){b_back};

            // create screen ui text elements
            ui.Add(new UIText("Press the UP Arrow Key to Jump", 600, 150, 200, 60, w));
            ui.Add(new UIText("Press the LEFT or RIGHT Arrow Keys to Move Left or Right", 600, 350, 200, 60, w));
            ui.Add(new UIText("Your score is based on how quickly you each the Target", 600, 550, 200, 60, w));

            // loop while window is open
            while (!w.CloseRequested)
            {
                // Process SplashKit Events
                SplashKit.ProcessEvents();
                w.Clear(Color.White);

                // update and draw UI elements
                foreach (UIBase uiElement in ui)
                {
                    uiElement.Update();
                    uiElement.Draw();
                }

                // process button click
                if (b_back.Clicked()) { return Screen.Main; }

                // refresh window
                w.Refresh();
            }
            return Screen.Quit;
        }
    }
    
    /* ************************************************************************
     * Individual Player Level Score
     * ***********************************************************************/
    public class Score
    {
        /*
        Individual Player Level Score
        -
        Represents an individual score that a player got for a particular
        level.

        Attributes / Fields
        -
        - _datetime : `DateTime`
            - Date/Time that the score was generated.
        - _level : `LevelNumber`
            - Number of the level that the score was generated in.
        - _score : `int`
            - Score value that the player achieved.

        Constants
        -
        None

        Methods
        -
        - Score(score, level) : `Score`
            - Public Constructor Method.
            - Creates a new score object with the given score and level number.

        Properties
        -
        - datetime : `DateTime`
            - Date/Time that the score was generated.
        - level : `LevelNumber`
            - Number of the level that the score was generated in.
        - score : `int`
            - Score value that the player achieved.
        */

        // **********
        // Attributes
        private DateTime _datetime;
        private LevelNumber _level;
        private int _score;

        // **********
        // Properties
        public DateTime datetime { get { return _datetime; } }
        public LevelNumber level { get { return _level; } }
        public int score { get { return _score; } }

        // ***********
        // Constructor
        public Score(int score, LevelNumber level)
        {
            // set date/time that score was achieved
            _datetime = DateTime.Now;

            // set the level number that the player achieved the score for
            _level = level;

            // set score value that the player achieved
            _score = score;
        }
    }

    /* ************************************************************************
     * UI Parent
     * ***********************************************************************/
    public abstract class UIBase
    {
        /*
        UI Parent
        -
        Represents a base class for all user interface elements.

        Attributes / Fields
        -
        - _label : `string`
            - String of the text label to be displayed for the given user
                interface element.
        - _rect : `SplashKitSDK.Rectangle`
            - Rectangle box of the user interface element.
        - _window : `SplashKitSDK.Window`
            - Game window to display the user interface element on.

        Constants
        -
        None

        Methods
        -
        - Clicked() : `void`
            - Public Method.
            - Checks if the interface element has been clicked.
        - Draw() : `void`
            - Public Abstract Method.
            - Draws the user interface element to the screen.
        - MouseOver() : `bool`
            - Public Method.
            - Checks if the mouse is currently over the interface element.
        - UIBase(label, x, y, width, height, window) : `UIBase`
            - Public Constructor Method.
            - Creates a new user interface base element.
        - Update() : `void`
            - Public Virtual Method.
            - Updates the user interface element's state.

        Properties
        -
        - height : `double`
            - Height of the user interface element.
        - pos : `SplashKitSDK.Point2D`
            - 2D coordinate of the centre of the user interface element.
        - width : `double`
            - Width of the user interface element.
        */

        // **********
        // Attributes
        protected string _label;
        protected SplashKitSDK.Rectangle _rect;
        protected SplashKitSDK.Window _window;

        // **********
        // Properties
        public double height { get { return _rect.Height; } }
        public SplashKitSDK.Point2D pos { get {
            return new SplashKitSDK.Point2D()
            {
                X = _rect.X + (_rect.Width / 2),
                Y = _rect.Y + (_rect.Height / 2)
            };
        }}
        public double width { get { return _rect.Width; } }

        // ***********
        // Constructor
        public UIBase(
                string label,
                double x,
                double y,
                double width,
                double height,
                SplashKitSDK.Window window
        )
        {
            // set label to be displayed
            _label = label;

            // create element rectangle
            _rect = new SplashKitSDK.Rectangle()
            {
                X = x - (width / 2),
                Y = y - (height / 2),
                Width = width,
                Height = height
            };

            // set element window
            _window = window;
        }

        // **************
        // Get if Clicked
        public bool Clicked()
        {
            // check if mouse is over this element + clicked
            return (
                MouseOver() 
                && SplashKit.MouseClicked(SplashKitSDK.MouseButton.LeftButton)
            );
        }

        // ***************
        // Draw UI Element
        public abstract void Draw();

        // *******************
        // Check if Mouse Over
        public bool MouseOver()
        {
            // check if mouse is currently over the element rectangle
            return SplashKit.PointInRectangle(SplashKit.MousePosition(), _rect);
        }

        // *****************
        // Update UI Element
        public virtual void Update()
        {
            // do nothing by default
        }
    }

    /* ************************************************************************
     * UI Button
     * ***********************************************************************/
    public class UIButton : UIBase
    {
        /*
        UI Button
        -
        Represents an individual button in the user interface.

        Constants
        -
        - COL_HOVER_BORDER : `SplashKitSDK.Color`
            - Border colour used when the button is being hovered over.
        - COL_HOVER_FILL : `SplashKitSDK.Color`
            - Fill colour used when the button is being hovered over.
        - COL_NORMAL_BORDER : `SplashKitSDK.Color`
            - Border colour used when the button is not being hovered over.
        - COL_NORMAL_FILL : `SplashKitSDK.Color`
            - Fill colour used when the button is not being hovered over.
        - COL_TEXT : `SplashKitSDK.Color`
            - Color used for writing the text for the button.

        Methods
        -
        - Draw() : `void`
            - Public Override Method.
            - Draws the user interface button to the screen.
        - UIButton(label, x, y, width, height, window) : `UIButton`
            - Public Constructor Method.
            - Creates a new user interface button.
        */

        // *********
        // Constants
        private SplashKitSDK.Color COL_HOVER_BORDER = SplashKit.RGBColor(55, 72, 99);
        private SplashKitSDK.Color COL_HOVER_FILL = SplashKit.RGBColor(153, 153, 153);
        private SplashKitSDK.Color COL_NORMAL_BORDER = SplashKit.RGBColor(102, 102, 102);
        private SplashKitSDK.Color COL_NORMAL_FILL = SplashKit.RGBColor(204, 204, 204);
        private SplashKitSDK.Color COL_TEXT = SplashKit.RGBColor(255, 255, 255);

        // ***********
        // Constructor
        public UIButton(
                string label,
                double x,
                double y,
                double width,
                double height,
                SplashKitSDK.Window window
        ) : base(label, x, y, width, height, window)
        { }

        // ***************
        // Draw UI Element
        public override void Draw()
        {
            // local function used for draw fill + border of the button
            void DrawRectangle(
                    SplashKitSDK.Color fill,
                    SplashKitSDK.Color border
            )
            {
                SplashKit.FillRectangleOnWindow(_window, fill, _rect);
                SplashKit.DrawRectangleOnWindow(_window, border, _rect);
            }

            // draw button fill + border
            if (MouseOver())
            {
                DrawRectangle(COL_HOVER_FILL, COL_HOVER_BORDER);
            }
            else
            {
                DrawRectangle(COL_NORMAL_FILL, COL_NORMAL_BORDER);
            }

            // draw the button text
            SplashKit.DrawTextOnWindow(
                _window,
                _label,
                COL_TEXT,
                "Arial",
                20,
                pos.X - (_label.Length * 4),
                pos.Y - 7
            );
        }
    }

    public class UIText : UIBase
    {
        private SplashKitSDK.Color COL_TEXT = SplashKit.RGBColor(0, 0, 0);

        public UIText(
            string label,
            double x,
            double y,
            double width,
            double height,
            SplashKitSDK.Window window
        ) : base(label, x, y, width, height, window)
        { }

        public override void Draw()
        {
            SplashKit.DrawTextOnWindow(
                _window,
                _label,
                COL_TEXT,
                "Arial",
                20,
                pos.X - (_label.Length * 4),
                pos.Y - 7
            );
        }
    }

    /* ************************************************************************
     * UI Textbox
     * ***********************************************************************/
    public class UITextBox : UIBase
    {
        /*
        UI Textbox
        -
        Represents an individual textbox in the user interface.

        Attributes / Fields
        -
        - _data : `string`
            - Data entered into the textbox.
        - _placeholder : `string`
            - Placeholder text shown when nothing has been entered into the
                textbox.
        - _selected : `bool`
            - Whether or not the textbox has been selected.

        Constants
        -

        Methods
        -
        - Draw() : `void`
            - Public Override Method.
            - Draws the user interface textbox to the screen.
        - UITextBox(...) : `UITextBox`
            - Public Constructor Method.
            - Creates a new user interface textbox.
        - Update() : `void`
            - Public Override Method.
            - Updates the state of the textbox.

        Properties
        -
        - data : `string`
            - Data entered into the textbox.
        */

        // **********
        // Attributes
        protected string _data;
        protected string _placeholder;
        protected bool _selected;
        protected bool _submitted;
        private SplashKitSDK.Color COL_TEXT = SplashKit.RGBColor(0, 0, 0);
        private SplashKitSDK.Color COL_NORMAL_FILL = SplashKit.RGBColor(204, 204, 204);
        private SplashKitSDK.Color COL_SELECTED_FILL = SplashKit.RGBColor(255, 255, 255);
        private SplashKitSDK.Color COL_BORDER = SplashKit.RGBColor(102, 102, 102);

        // Properties
        public string data { get { return _data; } }
        public bool submitted { get { return _submitted; } }

        public UITextBox(
                string label,
                double x,
                double y,
                double width,
                double height,
                SplashKitSDK.Window window,
                string data = "",
                string placeholder = ""
        ) : base(label, x, y, width, height, window)
        {
            _data = data;
            _placeholder = placeholder;
            _selected = false;
            _submitted = false;
        }

        // Draw UI Element
        public override void Draw()
        {
            // local function used for draw fill + border of the button
            void DrawRectangle(
                    SplashKitSDK.Color fill,
                    SplashKitSDK.Color border
            )
            {
                SplashKit.FillRectangleOnWindow(_window, fill, _rect);
                SplashKit.DrawRectangleOnWindow(_window, border, _rect);
            }

            // draw button fill + border
            if (_selected)
            {
                DrawRectangle(COL_SELECTED_FILL, COL_BORDER);
            }
            else
            {
                DrawRectangle(COL_NORMAL_FILL, COL_BORDER);
            }

            // draw label text
            SplashKit.DrawTextOnWindow(
                _window,
                _label,
                COL_TEXT,
                "Arial",
                20,
                _rect.X,
                _rect.Y - 10
            );

            // draw data text
            if (_selected)
            {
                SplashKit.DrawTextOnWindow(
                    _window,
                    _data + SplashKit.TextInput(),
                    COL_TEXT,
                    "Arial",
                    20,
                    pos.X - ((_data.Length + SplashKit.TextInput().Length) * 4),
                    pos.Y - 7
                );
            }
            else
            {
                SplashKit.DrawTextOnWindow(
                    _window,
                    _data,
                    COL_TEXT,
                    "Arial",
                    20,
                    pos.X - (_data.Length * 4),
                    pos.Y - 7
                );
            }
        }

        public void Reset()
        {
            _data = "";
        }

        // Update
        public override void Update()
        {
            // update selected status
            if (SplashKit.MouseClicked(SplashKitSDK.MouseButton.LeftButton))
            {
                if (
                        (MouseOver())
                        && (!_selected)
                ) {
                    SplashKit.StartReadingText(_rect);
                    _selected = true;
                }
                else if (
                        (!MouseOver())
                        && (_selected)
                ) {
                    _data += SplashKit.TextInput();
                    SplashKit.EndReadingText();
                    _selected = false;
                }
            }

            // handle keypress inputs
            if (_selected)
            {
                // backspace
                if (SplashKit.KeyTyped(SplashKitSDK.KeyCode.BackspaceKey))
                {
                    if (
                            (SplashKit.TextInput().Length == 0)
                            && (_data.Length > 0)
                    ) {
                        _data = _data.Substring(0, _data.Length - 1);
                    }
                }

                // enter
                else if (SplashKit.KeyTyped(SplashKitSDK.KeyCode.ReturnKey))
                {
                    _data += SplashKit.TextInput();
                    _submitted = true;
                }

                // key pressed
                else
                {
                    // if (_selected) { SplashKit.StartReadingText(_rect); }
                    // else { SplashKit.EndReadingText(); }
                    // SplashKit.StartReadingText(_rect);
                    // string key = SplashKit.TextInput();
                    // SplashKit.EndReadingText();
                    // Console.WriteLine($"Key Pressed {key}");
                    // if (!string.IsNullOrEmpty(key)) { _data += key; }
                }
            }
        }
    }
}
