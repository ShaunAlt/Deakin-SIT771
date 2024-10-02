// ----------------------------------------------------------------------------
// Purpose:
// SIT771 T2 2024
// 7.3 - Custom Program Code
// 7.4 - Something Awesome
//
// Description:
// This program runs a self-contained platformer game. The game saves and loads
// player accounts and scores from an xml file. The game has 10 different
// levels, each of which players are able to attempt, as well as instructions
// on how to play the game. The game also has a highscores chart which displays
// the highscore of the current player, as well as all-time highscores from all
// players.
//
// Author: Shaun Altmann
// ----------------------------------------------------------------------------

// ----------------------------------------------------------------------------
// Dependencies
// ----------------------------------------------------------------------------

// used for implementing fundamental C# functionality
using System;

// used for implementing SplashKit functionality
using SplashKitSDK;

// used for reading and writing XML save files
using System.Xml.Linq;

// ----------------------------------------------------------------------------
// Main Program - Auto-Generated
// ----------------------------------------------------------------------------
namespace _7_3_CustomProgramCode
{
    /// <summary>
    /// Represents a single player account in the game.
    /// </summary>
    /// <remarks>
    /// Fields + Properties
    /// <list type="bullet">
    ///     <item>
    ///         - _accounts : <c>List &lt; Account &gt; </c>
    ///         &lt;&lt; static &gt;&gt;
    ///     </item>
    ///     <item>+ Name : <c>string</c></item>
    ///     <item>+ Scores : <c>List &lt; Score &gt; </c></item>
    /// </list>
    /// 
    /// Methods
    /// <list type="bullet">
    ///     <item>+ Account(name) : <c>Account</c></item>
    ///     <item>+ AddScore(lvl, score) : <c>void</c></item>
    ///     <item>+ AddScore(lvl, score, datetime) : <c>void</c></item>
    ///     <item>+ GetAccount(name) : <c>Account</c></item>
    ///     <item>+ GetScore(lvl) : <c>Score | null</c></item>
    ///     <item>
    ///         + GetScores(lvl, num_max) : <c> List &lt; Score &gt;</c>
    ///     </item>
    ///     <item>+ ReadData(filename) : <c>void</c></item>
    ///     <item>+ SaveData(filename) : <c>void</c></item>
    /// </list>
    /// </remarks>
    /// <see cref="Score"/>
    public class Account
    {
        /// <summary>
        /// Static collection of all accounts (including the current account if
        /// the player is logged in) in the game.
        /// </summary>
        private static List<Account> _accounts = new List<Account>();

        /// <summary>
        /// Name of the current player account.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Collection of all scores the current player account has achieved in
        /// the game.
        /// </summary>
        public List<Score> Scores { get; private set; }

        /// <summary>
        /// Creates a new player account with the specified name.
        /// </summary>
        /// <param name="name">Name of the new player account.</param>
        public Account(string name)
        {
            // set the player name
            Name = name;

            // initialize the scores of the player
            Scores = new List<Score>();
        }

        /// <summary>
        /// Adds a new score for the given level to the current game account.
        /// </summary>
        /// <param name="lvl">Level the score was achieved on.</param>
        /// <param name="score">Score that was achieved.</param>
        public void AddScore(LevelNumber lvl, int score)
        {
            // only add the score if valid
            if (score > 0) {
                Scores.Add(new Score(score, lvl));
            }
        }

        /// <summary>
        /// Adds a new score for the given level to the current game account.
        /// </summary>
        /// <param name="lvl">Level the score was achieved on.</param>
        /// <param name="score">Score that was achieved.</param>
        /// <param name="datetime">
        ///     Date/Time that the score was achieved at.
        /// </param>
        public void AddScore(LevelNumber lvl, int score, DateTime datetime)
        {
            // only add the score if valid
            if (score > 0)
            {
                Scores.Add(new Score(score, lvl, datetime));
            }
        }

        /// <summary>
        /// Attempts to find the account from the collection of all accounts in
        /// the game. If unsuccessful, will create a new account and return it.
        /// </summary>
        /// <param name="name">
        ///     Name of the player account to find / create.
        /// </param>
        /// <returns>
        /// If able to find an account with that name, will return that
        /// account. Otherwise, will create a new account with the given name
        /// and return it.
        /// </returns>
        public static Account GetAccount(string name)
        {
            // attempt to find the account
            foreach(Account account in _accounts)
            {
                // return matching account
                if (account.Name == name)
                {
                    return account;
                }
            }

            // no account found, create a new account + return it
            Account newAccount = new Account(name);
            _accounts.Add(newAccount);
            return newAccount;
        }

        /// <summary>
        /// Gets the current player account's top score for the given level, or
        /// null if the player has not yet completed it.
        /// </summary>
        /// <param name="lvl">Level to get the score for.</param>
        /// <returns>
        /// If the player has completed the level before, it will return their
        /// best score. Otherwise, will return <c>null</c>.
        /// </returns>
        public Score? GetScore(LevelNumber lvl)
        {
            // defaults to no best score
            Score? bestscore = null;

            // search all scores in the current player account
            foreach(Score score in Scores)
            {
                if (
                        (score.level == lvl) // if the required level
                        && (
                            (bestscore == null) // first score
                            || (score.score > bestscore.score) // new best
                        )
                )
                {
                    bestscore = score;
                }
            }

            // return the best score or null if none found
            return bestscore;
        }

        /// <summary>
        /// Gets the top <c>num_max</c> scores for the specified level.
        /// </summary>
        /// <param name="lvl">Level number to get the scores for.</param>
        /// <param name="num_max">
        ///     Number of scores to retrieve. Defaults to 3.
        /// </param>
        /// <returns>
        /// List containing the best scores for the specified level. The length
        /// of this list will be equal to or less than <c>num_max</c>.
        /// </returns>
        public static List<Score> GetScores(LevelNumber lvl, int num_max = 3)
        {
            // initialize collections of scores
            List<Score> scores_all = new List<Score>(); // all relevant scores
            List<Score> scores_sort = new List<Score>(); // sorted scores
            List<Score> scores_slice = new List<Score>(); // top sorted scores

            // get all scores for that particular level
            foreach (Account account in _accounts)
            {
                foreach (Score score in account.Scores)
                {
                    if (score.level == lvl)
                    {
                        scores_all.Add(score);
                    }
                }
            }

            // sort the scores in descending order based on score
            scores_sort = scores_all.OrderBy(s1 => -1 * s1.score).ToList();

            // get only the top scores
            for (int i = 0; i < num_max; i++)
            {
                // if there are less scores than required, stop adding more
                // scores
                if (i < scores_sort.Count)
                {
                    scores_slice.Add(scores_sort[i]);
                }
                else {
                    break;
                }
            }

            // return the top scores for that level
            return scores_slice;
        }

        /// <summary>
        /// Read the file with the provided name, and re-create all of the
        /// accounts and scores associated with each.
        /// </summary>
        /// <param name="filename">Name of the file to read from.</param>
        public static void ReadData(string filename)
        {
            // initialize file document
            XDocument? doc;

            // attempt to read file data
            try
            {
                doc = XDocument.Load(filename);
            }
            catch
            {
                // error will occur if the file could not be found, or contains
                // invalid data -> don't attempt to parse the data
                return;
            }

            // if file data could not be read, return early
            if (doc == null)
            {
                return;
            }

            // read the list of accounts from the xml file
            var accounts = doc.Element("Accounts")?.Elements("Account");

            // if no accounts were found in the file, return early
            if (accounts == null)
            {
                return;
            }

            // re-create all accounts and scores from the xml file
            foreach (XElement account in accounts)
            {
                // get the name of the account
                string name = account.Element("Name")?.Value;

                // create a new account with the specified name
                Account newAccount = Account.GetAccount(name);

                // get the scores for the account from the xml file
                var scores = account.Element("Scores")?.Elements("Score");

                // if no scores were found, skip to the next account
                if (scores == null)
                {
                    continue;
                }

                // re-create all scores for the account
                foreach (var score in scores)
                {
                    // get the level, score, and date/time from the xml file
                    string level = score.Attribute("Level")?.Value;
                    string scoreVal = score.Attribute("Score")?.Value;
                    string dateTime = score.Attribute("DateTime")?.Value;

                    // create a new score with the specified level, score, and
                    // date/time data
                    newAccount.AddScore(
                        (LevelNumber) Convert.ToInt32(level),
                        Convert.ToInt32(scoreVal),
                        DateTime.Parse(dateTime)
                    );
                }
            }
        }

        /// <summary>
        /// Saves the data of all accounts to the specified save file.
        /// </summary>
        /// <param name="filename">Name of the file to save to.</param>
        public static void SaveData(string filename)
        {
            // save the player accounts to a file
            XDocument doc = new XDocument(
                // create the root element
                new XElement("Accounts",
                    // create a new account element for each player account
                    _accounts.Select(a =>
                        new XElement("Account",
                            // save the player account name
                            new XElement("Name", a.Name),

                            // save the scores for the player account, if any
                            new XElement("Scores",
                                a.Scores.Select(s =>
                                    // create a new score element for each
                                    // score in the player account
                                    new XElement("Score",
                                        // save the level the score is for
                                        new XAttribute("Level", (int)s.level),
                                        // save the score achieved
                                        new XAttribute("Score", s.score),
                                        // save the date/time the score was
                                        // achieved
                                        new XAttribute("DateTime", s.datetime)
                                    )
                                )
                            )
                        )
                    )
                )
            );

            // overwrite the current document (if it exists) with the new save
            // data
            doc.Save(filename);
        }
    }

    /// <summary>
    /// Represents a goal sprite within a game level.
    /// </summary>
    /// <remarks>
    /// Fields + Properties
    /// <list type="bullet">
    ///     <item>+ Bmp : <c>SplashKitSDK.Bmp | null</c></item>
    ///     <item>+ Pos : <c>SplashKitSDK.Point2D Pos</c></item>
    ///     <item>+ Txt : <c>string | null</c></item>
    /// </list>
    /// 
    /// Methods
    /// <list type="bullet">
    ///     <item>+ Draw() : <c>void</c></item>
    ///     <item>+ GetRectangle() : <c>SplashKitSDK.Rectangle</c></item>
    ///     <item>+ Goal(x, y, window) : <c>Goal</c></item>
    ///     <item>+ Update(vel_x, vel_y, reverse=false) : <c>void</c></item>
    /// </list>
    /// </remarks>
    /// <see cref="Level"/>
    /// <see cref="Sprite"/>
    public class Goal : Sprite
    {
        /// <summary>
        /// Creates a new goal sprite for a game level.
        /// </summary>
        /// <param name="x">X-Coordinate of the goal starting position.</param>
        /// <param name="y">Y-Coordinate of the goal starting position.</param>
        /// <param name="window">Game window the goal is drawn on.</param>
        /// <returns>
        /// New Goal sprite at the specified starting position.
        /// </returns>
        public Goal(
            float x,
            float y,
            Window window
        ) : base(x, y, "Resources/images/Target2.png", window) { }
    }

    /// <summary>
    /// Raised when the program encounters an invalid <c>LevelNumber</c> enum
    /// value.
    /// </summary>
    /// <see cref="LevelNumber"/>
    public class InvalidLevelNumberException : System.Exception
    {
        public InvalidLevelNumberException() { }
        public InvalidLevelNumberException(
            string message
        ) : base(message) { }
        public InvalidLevelNumberException(
            string message,
            System.Exception inner
        ) : base(message, inner) { }
    }

    /// <summary>
    /// Raised when the program encounters an invalid <c>PlatformType</c> enum
    /// value.
    /// </summary>
    /// <see cref="Platform"/> 
    /// <see cref="PlatformType"/>
    public class InvalidPlatformTypeException : System.Exception
    {
        public InvalidPlatformTypeException() { }
        public InvalidPlatformTypeException(
            string message
        ) : base(message) { }
        public InvalidPlatformTypeException(
            string message,
            System.Exception inner
        ) : base(message, inner) { }
    }

    /// <summary>
    /// Raised when a sprite is unable to be drawn due to invalid bitmap and
    /// text data.
    /// </summary>
    /// <see cref="Sprite"/>
    public class InvalidSpriteDrawException : System.Exception
    {
        public InvalidSpriteDrawException() { }
        public InvalidSpriteDrawException(
            string message
        ) : base(message) { }
        public InvalidSpriteDrawException(
            string message,
            System.Exception inner
        ) : base(message, inner) { }
    }

    /// <summary>
    /// Raised when a sprite is unable to create a bounding rectangle for
    /// itself.
    /// </summary>
    /// <see cref="Sprite"/>
    public class InvalidSpriteRectangleException : System.Exception
    {
        public InvalidSpriteRectangleException() { }
        public InvalidSpriteRectangleException(
            string message
        ) : base(message) { }
        public InvalidSpriteRectangleException(
            string message,
            System.Exception inner
        ) : base(message, inner) { }
    }

    /// <summary>
    /// Represents an individual level that can be played in the game.
    /// </summary>
    /// <remarks>
    /// Fields + Properties
    /// <list type="bullet">
    ///     <item>- _goal : <c>Goal</c></item>
    ///     <item>- _platforms : <c>List &lt; Platform &gt; </c></item>
    ///     <item>- _player : <c>Player</c></item>
    ///     <item>- _txts : <c>List &lt; SpriteText &gt; </c></item>
    ///     <item>- _window : <c>SplashKitSDK.Window</c></item>
    /// </list>
    /// 
    /// Methods
    /// <list type="bullet">
    ///     <item>- Draw(score, vel) : <c>void</c></item>
    ///     <item>- GetNonPlayerSprites() : <c>List &lt; Sprite &gt; </c></item>
    ///     <item>+ Level(lvl, w) : <c>Level</c></item>
    ///     <item>+ Play() : <c>int</c></item>
    ///     <item>- UpdateX(vel) : <c>double</c></item>
    ///     <item>- UpdateY(vel) : <c>double</c></item>
    /// </list>
    /// </remarks>
    /// <see cref="Goal"/>
    /// <see cref="LevelNumber"/>
    /// <see cref="Platform"/>
    /// <see cref="Player"/>
    /// <see cref="Sprite"/>
    /// <see cref="SpriteText"/>
    public class Level
    {
        /// <summary>
        /// Collection of platforms that are used to create the current level.
        /// </summary>
        private List<Platform> _platforms;

        /// <summary>
        /// Player sprite that is used to move around the current level.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Goal sprite that is the target for the current level.
        /// </summary>
        private Goal _goal;

        /// <summary>
        /// Collection of text elements that should be displayed in the current
        /// level.
        /// </summary>
        private List<SpriteText> _txts;
        
        /// <summary>
        /// Window used to display the current level.
        /// </summary>
        private SplashKitSDK.Window _window;

        /// <summary>
        /// Draws all sprites + score text for the current level on the game
        /// window.
        /// </summary>
        /// <param name="score">Score to display.</param>
        /// <param name="vel">Current player sprite velocity.</param>
        private void Draw(int score, SplashKitSDK.Vector2D vel)
        {
            // clear window
            _window.Clear(SplashKitSDK.Color.White);

            // draw the player
            _player.DrawPlayer(vel.X, vel.Y);

            // draw the non-player sprites
            foreach (Sprite sprite in GetNonPlayerSprites())
            {
                sprite.Draw();
            }

            // draw score text
            SplashKit.DrawTextOnWindow(
                _window,
                $"Current Score: {score}",
                SplashKitSDK.Color.Black,
                50,
                50
            );
        }

        /// <summary>
        /// Gets a collection of all platforms, text elements, and the goal in
        /// the current level (all sprites except for the player).
        /// </summary>
        /// <returns>
        /// Collection of all platforms, text elements, and the goal in the
        /// current level (all sprites except for the player).
        /// </returns>
        private List<Sprite> GetNonPlayerSprites()
        {
            // initialize sprites list
            List<Sprite> sprites = new List<Sprite>();

            // add all platforms
            foreach (Platform p in _platforms) { sprites.Add(p); }

            // add all text elements
            foreach (SpriteText t in _txts) { sprites.Add(t); }

            // add goal
            sprites.Add(_goal);

            // return sprites list
            return sprites;
        }

        /// <summary>
        /// Creates a new game level that the player is able to play.
        /// </summary>
        /// <param name="lvl">Number of the level to create.</param>
        /// <param name="w">Game window to display the level on.</param>
        public Level(LevelNumber lvl, SplashKitSDK.Window w)
        {
            // store window for the current level
            _window = w;

            // create the player
            _player = new Player(600, 400, w);

            // create the platform, goal, and text element sprites for the
            // level based on the level number
            switch (lvl)
            {
                case LevelNumber.L0: // basic platforms
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
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
                        // left wall
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 500, PlatformType.Large90, w),
                        // right wall
                        new Platform(3195, 500, PlatformType.Large90, w),
                        new Platform(3195, 300, PlatformType.Large90, w),
                        new Platform(3195, 100, PlatformType.Large90, w),
                        new Platform(3195, -100, PlatformType.Large90, w),
                        new Platform(3195, -300, PlatformType.Large90, w),
                        new Platform(3195, -500, PlatformType.Large90, w),
                        // platform 1
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        // platform 2
                        new Platform(1200, 300, PlatformType.Large, w),
                        new Platform(1400, 300, PlatformType.Large, w),
                        // platform 3
                        new Platform(1800, 100, PlatformType.Large, w),
                        new Platform(2000, 100, PlatformType.Large, w),
                        // platform 4
                        new Platform(2400, -100, PlatformType.Large, w),
                        new Platform(2600, -100, PlatformType.Large, w),
                    };
                    // create goal on platform 4
                    _goal = new Goal(2600, -200, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(
                            600,
                            300,
                            "Jump along the platforms to the goal",
                            w
                        ),
                    };
                    break;
                case LevelNumber.L1: // go down
                    // create platforms
                    _platforms = new List<Platform>(){
                        // left wall
                        new Platform(0, 0, PlatformType.Large90, w),
                        new Platform(0, 200, PlatformType.Large90, w),
                        new Platform(0, 400, PlatformType.Large90, w),
                        new Platform(0, 600, PlatformType.Large90, w),
                        new Platform(0, 800, PlatformType.Large90, w),
                        new Platform(0, 1000, PlatformType.Large90, w),
                        new Platform(0, 1200, PlatformType.Large90, w),
                        new Platform(0, 1400, PlatformType.Large90, w),
                        new Platform(0, 1600, PlatformType.Large90, w),
                        new Platform(0, 1800, PlatformType.Large90, w),
                        new Platform(0, 2000, PlatformType.Large90, w),
                        new Platform(0, 2200, PlatformType.Large90, w),
                        new Platform(0, 2400, PlatformType.Large90, w),
                        new Platform(0, 2600, PlatformType.Large90, w),
                        new Platform(0, 2800, PlatformType.Large90, w),
                        new Platform(0, 3000, PlatformType.Large90, w),
                        new Platform(0, 3200, PlatformType.Large90, w),
                        new Platform(0, 3400, PlatformType.Large90, w),
                        new Platform(0, 3600, PlatformType.Large90, w),
                        new Platform(0, 3800, PlatformType.Large90, w),
                        // right wall
                        new Platform(1195, 0, PlatformType.Large90, w),
                        new Platform(1195, 200, PlatformType.Large90, w),
                        new Platform(1195, 400, PlatformType.Large90, w),
                        new Platform(1195, 600, PlatformType.Large90, w),
                        new Platform(1195, 800, PlatformType.Large90, w),
                        new Platform(1195, 1000, PlatformType.Large90, w),
                        new Platform(1195, 1200, PlatformType.Large90, w),
                        new Platform(1195, 1400, PlatformType.Large90, w),
                        new Platform(1195, 1600, PlatformType.Large90, w),
                        new Platform(1195, 1800, PlatformType.Large90, w),
                        new Platform(1195, 2000, PlatformType.Large90, w),
                        new Platform(1195, 2200, PlatformType.Large90, w),
                        new Platform(1195, 2400, PlatformType.Large90, w),
                        new Platform(1195, 2600, PlatformType.Large90, w),
                        new Platform(1195, 2800, PlatformType.Large90, w),
                        new Platform(1195, 3000, PlatformType.Large90, w),
                        new Platform(1195, 3200, PlatformType.Large90, w),
                        new Platform(1195, 3400, PlatformType.Large90, w),
                        new Platform(1195, 3600, PlatformType.Large90, w),
                        new Platform(1195, 3800, PlatformType.Large90, w),
                        // platform - start
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        // lvl 1
                        new Platform(0, 700, PlatformType.Large, w),
                        new Platform(200, 700, PlatformType.Large, w),
                        new Platform(800, 700, PlatformType.Large, w),
                        new Platform(1000, 700, PlatformType.Large, w),
                        // lvl 2
                        new Platform(400, 900, PlatformType.Large, w),
                        new Platform(600, 900, PlatformType.Large, w),
                        // lvl 3
                        new Platform(0, 1100, PlatformType.Large, w),
                        new Platform(200, 1100, PlatformType.Large, w),
                        new Platform(800, 1100, PlatformType.Large, w),
                        new Platform(1000, 1100, PlatformType.Large, w),
                        // lvl 4
                        new Platform(400, 1300, PlatformType.Large, w),
                        new Platform(600, 1300, PlatformType.Large, w),
                        // lvl 5
                        new Platform(0, 1500, PlatformType.Large, w),
                        new Platform(200, 1500, PlatformType.Large, w),
                        new Platform(800, 1500, PlatformType.Large, w),
                        new Platform(1000, 1500, PlatformType.Large, w),
                        // lvl 6
                        new Platform(400, 1700, PlatformType.Large, w),
                        new Platform(600, 1700, PlatformType.Large, w),
                        // lvl 7
                        new Platform(0, 1900, PlatformType.Large, w),
                        new Platform(200, 1900, PlatformType.Large, w),
                        new Platform(800, 1900, PlatformType.Large, w),
                        new Platform(1000, 1900, PlatformType.Large, w),
                        // lvl 8
                        new Platform(400, 2100, PlatformType.Large, w),
                        new Platform(600, 2100, PlatformType.Large, w),
                        // lvl 9
                        new Platform(0, 2300, PlatformType.Large, w),
                        new Platform(200, 2300, PlatformType.Large, w),
                        new Platform(800, 2300, PlatformType.Large, w),
                        new Platform(1000, 2300, PlatformType.Large, w),
                        // lvl 10
                        new Platform(400, 2500, PlatformType.Large, w),
                        new Platform(600, 2500, PlatformType.Large, w),
                        // lvl 11
                        new Platform(0, 2700, PlatformType.Large, w),
                        new Platform(200, 2700, PlatformType.Large, w),
                        new Platform(800, 2700, PlatformType.Large, w),
                        new Platform(1000, 2700, PlatformType.Large, w),
                        // lvl 12
                        new Platform(400, 2900, PlatformType.Large, w),
                        new Platform(600, 2900, PlatformType.Large, w),
                        // lvl 13
                        new Platform(0, 3100, PlatformType.Large, w),
                        new Platform(200, 3100, PlatformType.Large, w),
                        new Platform(800, 3100, PlatformType.Large, w),
                        new Platform(1000, 3100, PlatformType.Large, w),
                        // lvl 14
                        new Platform(400, 3300, PlatformType.Large, w),
                        new Platform(600, 3300, PlatformType.Large, w),
                        // lvl 15
                        new Platform(0, 3500, PlatformType.Large, w),
                        new Platform(200, 3500, PlatformType.Large, w),
                        new Platform(800, 3500, PlatformType.Large, w),
                        new Platform(1000, 3500, PlatformType.Large, w),
                        // lvl 16
                        new Platform(400, 3700, PlatformType.Large, w),
                        new Platform(600, 3700, PlatformType.Large, w),
                        // floor
                        new Platform(0, 4000, PlatformType.Large, w),
                        new Platform(200, 4000, PlatformType.Large, w),
                        new Platform(400, 4000, PlatformType.Large, w),
                        new Platform(600, 4000, PlatformType.Large, w),
                        new Platform(800, 4000, PlatformType.Large, w),
                        new Platform(1000, 4000, PlatformType.Large, w),
                    };
                    // create goal on floor
                    _goal = new Goal(600, 3900, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Go Down", w),
                    };
                    break;
                case LevelNumber.L2: // spiral
                    // create platforms
                    _platforms = new List<Platform>(){
                        // start
                        new Platform(500, 500, PlatformType.Large, w),
                        new Platform(500, 300, PlatformType.Large90, w),
                        new Platform(500, 300, PlatformType.Large, w),
                        // rise 1
                        new Platform(700, 500, PlatformType.Large, w),
                        new Platform(895, 300, PlatformType.Large90, w),
                        new Platform(895, 100, PlatformType.Large90, w),
                        new Platform(700, 100, PlatformType.Large, w),
                        // flat 1
                        new Platform(500, 100, PlatformType.Large, w),
                        // drop 1
                        new Platform(300, 100, PlatformType.Large, w),
                        new Platform(300, 100, PlatformType.Large90, w),
                        new Platform(300, 300, PlatformType.Large90, w),
                        new Platform(300, 500, PlatformType.Large90, w),
                        new Platform(300, 700, PlatformType.Large, w),
                        // flat 2
                        new Platform(500, 700, PlatformType.Large, w),
                        new Platform(700, 700, PlatformType.Large, w),
                        // rise 2
                        new Platform(900, 700, PlatformType.Large, w),
                        new Platform(1095, 500, PlatformType.Large90, w),
                        new Platform(1095, 300, PlatformType.Large90, w),
                        new Platform(1095, 100, PlatformType.Large90, w),
                        new Platform(1095, -100, PlatformType.Large90, w),
                        new Platform(900, -100, PlatformType.Large, w),
                        new Platform(1050, 500, PlatformType.Small, w),
                        new Platform(1050, 300, PlatformType.Small, w),
                        new Platform(1050, 100, PlatformType.Small, w),
                        // flat 3
                        new Platform(700, -100, PlatformType.Large, w),
                        new Platform(500, -100, PlatformType.Large, w),
                        new Platform(300, -100, PlatformType.Large, w),
                        // drop 2
                        new Platform(100, -100, PlatformType.Large, w),
                        new Platform(100, -100, PlatformType.Large90, w),
                        new Platform(100, 100, PlatformType.Large90, w),
                        new Platform(100, 300, PlatformType.Large90, w),
                        new Platform(100, 500, PlatformType.Large90, w),
                        new Platform(100, 700, PlatformType.Large90, w),
                        new Platform(100, 900, PlatformType.Large, w),
                        // flat 4
                        new Platform(300, 900, PlatformType.Large, w),
                        new Platform(500, 900, PlatformType.Large, w),
                        new Platform(700, 900, PlatformType.Large, w),
                        new Platform(900, 900, PlatformType.Large, w),
                        // rise 3
                        new Platform(1100, 900, PlatformType.Large, w),
                        new Platform(1295, 700, PlatformType.Large90, w),
                        new Platform(1295, 500, PlatformType.Large90, w),
                        new Platform(1295, 300, PlatformType.Large90, w),
                        new Platform(1295, 100, PlatformType.Large90, w),
                        new Platform(1295, -100, PlatformType.Large90, w),
                        new Platform(1295, -300, PlatformType.Large90, w),
                        new Platform(1100, -300, PlatformType.Large, w),
                        new Platform(1250, 700, PlatformType.Small, w),
                        new Platform(1250, 500, PlatformType.Small, w),
                        new Platform(1250, 300, PlatformType.Small, w),
                        new Platform(1250, 100, PlatformType.Small, w),
                        new Platform(1250, -100, PlatformType.Small, w),
                        // end
                        new Platform(900, -300, PlatformType.Large, w),
                        new Platform(700, -300, PlatformType.Large, w),
                        new Platform(500, -300, PlatformType.Large, w),
                        new Platform(300, -300, PlatformType.Large, w),
                        new Platform(100, -300, PlatformType.Large, w),
                        new Platform(100, -300, PlatformType.Large90, w),
                    };
                    // create goal at end
                    _goal = new Goal(200, -200, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Get Out", w),
                    };
                    break;
                case LevelNumber.L3: // central wall
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        // right wall
                        new Platform(1995, 300, PlatformType.Large90, w),
                        new Platform(1995, 100, PlatformType.Large90, w),
                        new Platform(1995, -100, PlatformType.Large90, w),
                        new Platform(1995, -300, PlatformType.Large90, w),
                        new Platform(1995, -500, PlatformType.Large90, w),
                        // centre divider
                        new Platform(995, 300, PlatformType.Large90, w),
                        new Platform(995, 100, PlatformType.Large90, w),
                        new Platform(995, -100, PlatformType.Large90, w),
                        new Platform(995, -300, PlatformType.Large90, w),
                        // little platforms
                        new Platform(200, 300, PlatformType.Small, w),
                        new Platform(400, 100, PlatformType.Small, w),
                        new Platform(600, -100, PlatformType.Small, w),
                        new Platform(800, -300, PlatformType.Small, w),
                        // large run 1
                        new Platform(1000, -100, PlatformType.Large, w),
                        new Platform(1200, -100, PlatformType.Large, w),
                        new Platform(1400, -100, PlatformType.Large, w),
                        new Platform(1600, -100, PlatformType.Large, w),
                        // large run 2
                        new Platform(1200, 100, PlatformType.Large, w),
                        new Platform(1400, 100, PlatformType.Large, w),
                        new Platform(1600, 100, PlatformType.Large, w),
                        new Platform(1800, 100, PlatformType.Large, w),
                        // large run 3
                        new Platform(1000, 300, PlatformType.Large, w),
                        new Platform(1200, 300, PlatformType.Large, w),
                        new Platform(1400, 300, PlatformType.Large, w),
                        new Platform(1600, 300, PlatformType.Large, w),
                    };
                    // create goal at end of other side
                    _goal = new Goal(1100, 400, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Get Over", w),
                    };
                    break;
                case LevelNumber.L4: // spiral platforms
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        new Platform(0, -700, PlatformType.Large90, w),
                        new Platform(0, -900, PlatformType.Large90, w),
                        new Platform(0, -1100, PlatformType.Large90, w),
                        new Platform(0, -1300, PlatformType.Large90, w),
                        // right wall
                        new Platform(1795, 300, PlatformType.Large90, w),
                        new Platform(1795, 100, PlatformType.Large90, w),
                        new Platform(1795, -100, PlatformType.Large90, w),
                        new Platform(1795, -300, PlatformType.Large90, w),
                        new Platform(1795, -500, PlatformType.Large90, w),
                        new Platform(1795, -700, PlatformType.Large90, w),
                        new Platform(1795, -900, PlatformType.Large90, w),
                        new Platform(1795, -1100, PlatformType.Large90, w),
                        new Platform(1795, -1300, PlatformType.Large90, w),
                        // platforms (spiral)
                        new Platform(400, 300, PlatformType.Small, w),
                        new Platform(700, 200, PlatformType.Small, w),
                        new Platform(1000, 50, PlatformType.Small, w),
                        new Platform(1300, -100, PlatformType.Small, w),
                        new Platform(1500, -350, PlatformType.Small, w),
                        new Platform(1300, -600, PlatformType.Small, w),
                        new Platform(1000, -750, PlatformType.Small, w),
                        new Platform(700, -900, PlatformType.Small, w),
                        new Platform(400, -1000, PlatformType.Small, w),
                        new Platform(100, -1000, PlatformType.Small, w),
                    };
                    // create goal on last platform
                    _goal = new Goal(124, -1100, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Get Twisty", w),
                    };
                    break;
                case LevelNumber.L5: // tiny rise/fall
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        new Platform(0, -700, PlatformType.Large90, w),
                        new Platform(0, -900, PlatformType.Large90, w),
                        new Platform(0, -1100, PlatformType.Large90, w),
                        new Platform(0, -1300, PlatformType.Large90, w),
                        // right wall
                        new Platform(1995, 300, PlatformType.Large90, w),
                        new Platform(1995, 100, PlatformType.Large90, w),
                        new Platform(1995, -100, PlatformType.Large90, w),
                        new Platform(1995, -300, PlatformType.Large90, w),
                        new Platform(1995, -500, PlatformType.Large90, w),
                        new Platform(1995, -700, PlatformType.Large90, w),
                        new Platform(1995, -900, PlatformType.Large90, w),
                        new Platform(1995, -1100, PlatformType.Large90, w),
                        new Platform(1995, -1300, PlatformType.Large90, w),
                        // left platforms
                        new Platform(300, 250, PlatformType.Tiny, w),
                        new Platform(300, 0, PlatformType.Tiny, w),
                        new Platform(300, -250, PlatformType.Tiny, w),
                        new Platform(300, -500, PlatformType.Tiny, w),
                        new Platform(300, -750, PlatformType.Tiny, w),
                        new Platform(300, -1000, PlatformType.Tiny, w),
                        // central platform
                        new Platform(600, -1250, PlatformType.Large, w),
                        new Platform(800, -1250, PlatformType.Large, w),
                        new Platform(1000, -1250, PlatformType.Large, w),
                        new Platform(1200, -1250, PlatformType.Large, w),
                        // right platforms
                        new Platform(1700, -1000, PlatformType.Tiny, w),
                        new Platform(1700, -750, PlatformType.Tiny, w),
                        new Platform(1700, -500, PlatformType.Tiny, w),
                        new Platform(1675, -250, PlatformType.Medium, w),
                    };
                    // create goal on last platform
                    _goal = new Goal(1725, -350, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Get Vertical", w),
                    };
                    break;
                case LevelNumber.L6: // ultra tiny - 1
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        // right wall
                        new Platform(1995, 300, PlatformType.Large90, w),
                        new Platform(1995, 100, PlatformType.Large90, w),
                        new Platform(1995, -100, PlatformType.Large90, w),
                        new Platform(1995, -300, PlatformType.Large90, w),
                        new Platform(1995, -500, PlatformType.Large90, w),
                        // platforms
                        new Platform(300, 250, PlatformType.Tiny90, w),
                        new Platform(600, 0, PlatformType.Tiny90, w),
                        new Platform(900, 0, PlatformType.Tiny90, w),
                        new Platform(1200, 0, PlatformType.Tiny90, w),
                        new Platform(1500, 0, PlatformType.Tiny90, w),
                    };
                    // create goal on last platform
                    _goal = new Goal(1500, -100, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Get Tiny", w),
                    };
                    break;
                case LevelNumber.L7: // staircase
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        new Platform(2000, 500, PlatformType.Large, w),
                        new Platform(2200, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(400, 300, PlatformType.Large90, w),
                        new Platform(400, 100, PlatformType.Large90, w),
                        new Platform(400, -100, PlatformType.Large90, w),
                        new Platform(400, -300, PlatformType.Large90, w),
                        new Platform(400, -500, PlatformType.Large90, w),
                        new Platform(400, -700, PlatformType.Large90, w),
                        new Platform(400, -900, PlatformType.Large90, w),
                        new Platform(400, -1100, PlatformType.Large90, w),
                        new Platform(400, -1300, PlatformType.Large90, w),
                        // right wall
                        new Platform(2395, 300, PlatformType.Large90, w),
                        new Platform(2395, 100, PlatformType.Large90, w),
                        new Platform(2395, -100, PlatformType.Large90, w),
                        new Platform(2395, -300, PlatformType.Large90, w),
                        new Platform(2395, -500, PlatformType.Large90, w),
                        new Platform(2395, -700, PlatformType.Large90, w),
                        new Platform(2395, -900, PlatformType.Large90, w),
                        new Platform(2395, -1100, PlatformType.Large90, w),
                        new Platform(2395, -1300, PlatformType.Large90, w),
                        // staircase 1
                        new Platform(800, 300, PlatformType.Large, w),
                        new Platform(800, 300, PlatformType.Large90, w),
                        new Platform(1000, 100, PlatformType.Large, w),
                        new Platform(1000, 100, PlatformType.Large90, w),
                        new Platform(1200, -100, PlatformType.Large, w),
                        new Platform(1200, -100, PlatformType.Large90, w),
                        new Platform(1400, -300, PlatformType.Large, w),
                        new Platform(1400, -300, PlatformType.Large90, w),
                        new Platform(1600, -500, PlatformType.Large, w),
                        new Platform(1600, -500, PlatformType.Large90, w),
                        new Platform(1800, -700, PlatformType.Large, w),
                        new Platform(1800, -700, PlatformType.Large90, w),
                        new Platform(2000, -900, PlatformType.Large, w),
                        new Platform(2000, -900, PlatformType.Large90, w),
                        // staircase 2
                        new Platform(2200, -700, PlatformType.Large, w),
                        new Platform(2200, -700, PlatformType.Large90, w),
                        new Platform(2000, -500, PlatformType.Large, w),
                        new Platform(2000, -500, PlatformType.Large90, w),
                        new Platform(1800, -300, PlatformType.Large, w),
                        new Platform(1800, -300, PlatformType.Large90, w),
                        new Platform(1600, -100, PlatformType.Large, w),
                        new Platform(1600, -100, PlatformType.Large90, w),
                        new Platform(1400, 100, PlatformType.Large, w),
                        new Platform(1400, 100, PlatformType.Large90, w),
                        // staircase 3
                        new Platform(1600, 300, PlatformType.Large, w),
                        new Platform(1600, 300, PlatformType.Large90, w),
                        new Platform(1800, 100, PlatformType.Large, w),
                        new Platform(1800, 100, PlatformType.Large90, w),
                        new Platform(2000, -100, PlatformType.Large, w),
                        new Platform(2000, -100, PlatformType.Large90, w),
                        // staircase 4
                        new Platform(2200, 100, PlatformType.Large, w),
                        new Platform(2200, 100, PlatformType.Large90, w),
                        new Platform(2000, 100, PlatformType.Large, w),
                    };
                    // create goal under last staircase
                    _goal = new Goal(2300, 400, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Staircase", w),
                    };
                    break;
                case LevelNumber.L8: // sine wave
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        new Platform(2000, 500, PlatformType.Large, w),
                        new Platform(2200, 500, PlatformType.Large, w),
                        new Platform(2400, 500, PlatformType.Large, w),
                        new Platform(2600, 500, PlatformType.Large, w),
                        new Platform(2800, 500, PlatformType.Large, w),
                        new Platform(3000, 500, PlatformType.Large, w),
                        new Platform(3200, 500, PlatformType.Large, w),
                        new Platform(3400, 500, PlatformType.Large, w),
                        new Platform(3600, 500, PlatformType.Large, w),
                        new Platform(3800, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        new Platform(0, -700, PlatformType.Large90, w),
                        new Platform(0, -900, PlatformType.Large90, w),
                        new Platform(0, -1100, PlatformType.Large90, w),
                        new Platform(0, -1300, PlatformType.Large90, w),
                        // right wall
                        new Platform(3995, 300, PlatformType.Large90, w),
                        new Platform(3995, 100, PlatformType.Large90, w),
                        new Platform(3995, -100, PlatformType.Large90, w),
                        new Platform(3995, -300, PlatformType.Large90, w),
                        new Platform(3995, -500, PlatformType.Large90, w),
                        new Platform(3995, -700, PlatformType.Large90, w),
                        new Platform(3995, -900, PlatformType.Large90, w),
                        new Platform(3995, -1100, PlatformType.Large90, w),
                        new Platform(3995, -1300, PlatformType.Large90, w),
                        // platforms
                        new Platform(300, 250, PlatformType.Tiny, w),
                        new Platform(600, 0, PlatformType.Tiny, w),
                        new Platform(900, -250, PlatformType.Tiny, w),
                        new Platform(1200, -500, PlatformType.Tiny, w),
                        new Platform(1500, -250, PlatformType.Tiny, w),
                        new Platform(1800, 0, PlatformType.Tiny, w),
                        new Platform(2100, -250, PlatformType.Tiny, w),
                        new Platform(2400, -500, PlatformType.Tiny, w),
                        new Platform(2700, -250, PlatformType.Tiny, w),
                        new Platform(3000, 0, PlatformType.Tiny, w),
                        new Platform(3300, -250, PlatformType.Tiny, w),
                        new Platform(3600, -500, PlatformType.Tiny, w),
                        new Platform(3900, -250, PlatformType.Tiny, w),
                    };
                    // create goal on last platform
                    _goal = new Goal(3900, -350, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Tiny Zig-Zag", w),
                    };
                    break;
                case LevelNumber.L9: // ultra-tiny sine wave
                    // create platforms
                    _platforms = new List<Platform>(){
                        // floor
                        new Platform(0, 500, PlatformType.Large, w),
                        new Platform(200, 500, PlatformType.Large, w),
                        new Platform(400, 500, PlatformType.Large, w),
                        new Platform(600, 500, PlatformType.Large, w),
                        new Platform(800, 500, PlatformType.Large, w),
                        new Platform(1000, 500, PlatformType.Large, w),
                        new Platform(1200, 500, PlatformType.Large, w),
                        new Platform(1400, 500, PlatformType.Large, w),
                        new Platform(1600, 500, PlatformType.Large, w),
                        new Platform(1800, 500, PlatformType.Large, w),
                        new Platform(2000, 500, PlatformType.Large, w),
                        new Platform(2200, 500, PlatformType.Large, w),
                        new Platform(2400, 500, PlatformType.Large, w),
                        new Platform(2600, 500, PlatformType.Large, w),
                        new Platform(2800, 500, PlatformType.Large, w),
                        new Platform(3000, 500, PlatformType.Large, w),
                        new Platform(3200, 500, PlatformType.Large, w),
                        new Platform(3400, 500, PlatformType.Large, w),
                        new Platform(3600, 500, PlatformType.Large, w),
                        new Platform(3800, 500, PlatformType.Large, w),
                        new Platform(4000, 500, PlatformType.Large, w),
                        new Platform(4200, 500, PlatformType.Large, w),
                        new Platform(4400, 500, PlatformType.Large, w),
                        new Platform(4600, 500, PlatformType.Large, w),
                        new Platform(4800, 500, PlatformType.Large, w),
                        new Platform(5000, 500, PlatformType.Large, w),
                        new Platform(5200, 500, PlatformType.Large, w),
                        new Platform(5400, 500, PlatformType.Large, w),
                        new Platform(5600, 500, PlatformType.Large, w),
                        new Platform(5800, 500, PlatformType.Large, w),
                        new Platform(6000, 500, PlatformType.Large, w),
                        new Platform(6200, 500, PlatformType.Large, w),
                        new Platform(6400, 500, PlatformType.Large, w),
                        new Platform(6600, 500, PlatformType.Large, w),
                        new Platform(6800, 500, PlatformType.Large, w),
                        new Platform(7000, 500, PlatformType.Large, w),
                        new Platform(7200, 500, PlatformType.Large, w),
                        new Platform(7400, 500, PlatformType.Large, w),
                        new Platform(7600, 500, PlatformType.Large, w),
                        new Platform(7800, 500, PlatformType.Large, w),
                        // left wall
                        new Platform(0, 300, PlatformType.Large90, w),
                        new Platform(0, 100, PlatformType.Large90, w),
                        new Platform(0, -100, PlatformType.Large90, w),
                        new Platform(0, -300, PlatformType.Large90, w),
                        new Platform(0, -500, PlatformType.Large90, w),
                        new Platform(0, -700, PlatformType.Large90, w),
                        new Platform(0, -900, PlatformType.Large90, w),
                        new Platform(0, -1100, PlatformType.Large90, w),
                        new Platform(0, -1300, PlatformType.Large90, w),
                        // right wall
                        new Platform(7995, 300, PlatformType.Large90, w),
                        new Platform(7995, 100, PlatformType.Large90, w),
                        new Platform(7995, -100, PlatformType.Large90, w),
                        new Platform(7995, -300, PlatformType.Large90, w),
                        new Platform(7995, -500, PlatformType.Large90, w),
                        new Platform(7995, -700, PlatformType.Large90, w),
                        new Platform(7995, -900, PlatformType.Large90, w),
                        new Platform(7995, -1100, PlatformType.Large90, w),
                        new Platform(7995, -1300, PlatformType.Large90, w),
                        // platforms
                        new Platform(300, 250, PlatformType.Tiny90, w),
                        new Platform(600, 0, PlatformType.Tiny90, w),
                        new Platform(900, -250, PlatformType.Tiny90, w),
                        new Platform(1200, -500, PlatformType.Tiny90, w),
                        new Platform(1500, -250, PlatformType.Tiny90, w),
                        new Platform(1800, 0, PlatformType.Tiny90, w),
                        new Platform(2100, -250, PlatformType.Tiny90, w),
                        new Platform(2400, -500, PlatformType.Tiny90, w),
                        new Platform(2700, -250, PlatformType.Tiny90, w),
                        new Platform(3000, 0, PlatformType.Tiny90, w),
                        new Platform(3300, -250, PlatformType.Tiny90, w),
                        new Platform(3600, -500, PlatformType.Tiny90, w),
                        new Platform(3900, -250, PlatformType.Tiny90, w),
                        new Platform(4200, 0, PlatformType.Tiny90, w),
                        new Platform(4500, -250, PlatformType.Tiny90, w),
                        new Platform(4800, -500, PlatformType.Tiny90, w),
                        new Platform(5100, -250, PlatformType.Tiny90, w),
                        new Platform(5400, 0, PlatformType.Tiny90, w),
                        new Platform(5700, -250, PlatformType.Tiny90, w),
                        new Platform(6000, -500, PlatformType.Tiny90, w),
                        new Platform(6300, -250, PlatformType.Tiny90, w),
                        new Platform(6600, 0, PlatformType.Tiny90, w),
                        new Platform(6900, -250, PlatformType.Tiny90, w),
                        new Platform(7200, -500, PlatformType.Tiny90, w),
                        new Platform(7500, -250, PlatformType.Tiny90, w),
                        new Platform(7800, 0, PlatformType.Tiny90, w),
                    };
                    // create goal on last platform
                    _goal = new Goal(7800, -100, w);
                    // create text elements
                    _txts = new List<SpriteText>() {
                        new SpriteText(600, 300, "Ultra-Tiny Zig-Zag", w),
                    };
                    break;
                default: // unknown level number
                    throw new InvalidLevelNumberException(
                        $"Invalid level number {lvl}"
                    );
            }
        }

        /// <summary>
        /// Plays the current level, and returns the score that the player
        /// achieves.
        /// </summary>
        /// <returns>
        /// If the player finishes the level, returns the score (positive
        /// integer). If the player closed the screen, returns <c>-1</c>. If
        /// the player hit the ESCAPE key, returns <c>-2</c>. If the player ran
        /// out of time and the score became negative, returns <c>-3</c>.
        /// </returns>
        public int Play()
        {
            // initialize score
            int score = 10000;

            // initialize player velocity
            SplashKitSDK.Vector2D vel = new Vector2D(){ X = 0, Y = 0 };

            // loop while window is open
            while (!_window.CloseRequested)
            {
                // process SplashKit events
                SplashKit.ProcessEvents();

                // decrease the player's score
                score--;

                // close if score <= 0
                if (score <= 0)
                {
                    return -3;
                }

                // close if escape key pressed
                if (SplashKit.KeyDown(SplashKitSDK.KeyCode.EscapeKey))
                {
                    return -2;
                }
                
                // update horizontal velocity
                vel.X = UpdateX(vel.X);

                // update vertical velocity
                vel.Y = UpdateY(vel.Y);

                // if reached target - return score
                if (_player.CheckCollision(_goal))
                {
                    return score;
                }

                // draw level
                Draw(score, vel);

                // update screen at 60fps
                _window.Refresh(60);
            }

            // window was closed - return invalid score
            return -1;
        }

        /// <summary>
        /// Updates the horizontal positions of all sprites in the window
        /// based on the keys pressed and sprite collisions.
        /// </summary>
        /// <returns>
        /// Returns the horizontal velocity of all sprites in the window.
        /// Negative values indicate that the player is moving right (all other
        /// sprites a moving left).
        /// </returns>
        /// <param name="vel">
        ///     The current horizontal velocity of all sprites in the game.
        /// </param>
        private double UpdateX(double vel)
        {
            // get acceleration from key presses
            double acc = 0;
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.RightKey)) {
                acc -= 0.35;
            }
            if (SplashKit.KeyDown(SplashKitSDK.KeyCode.LeftKey)) {
                acc += 0.35;
            }

            // calculate velocity from acceleration
            vel += acc;

            // reduce velocity (automatically limits max speed)
            vel *= 0.95;
            if (Math.Abs(vel) < 0.25) // come to a stop
            {
                vel = 0;
            }

            // complete initial motion update
            foreach (Sprite sprite in GetNonPlayerSprites())
            {
                sprite.Update(vel, 0);
            }

            // check if collisions were caused by this movement
            bool collisionFound = false;
            foreach (Platform platform in _platforms)
            {
                if (_player.CheckCollision(platform))
                {
                    collisionFound = true;
                    break;
                }
            }

            // if no collision found - return the validated velocity
            if (!collisionFound)
            {
                return vel;
            }

            // if collision found - move back and retry
            foreach (Sprite sprite in GetNonPlayerSprites())
            {
                sprite.Update(vel, 0, true);
            }

            // no valid velocity found - prevent horizontal motion
            return 0;
        }

        /// <summary>
        /// Updates the vertical positions of all sprites in the window based
        /// on the keys pressed and sprite collisions.
        /// </summary>
        /// <returns>
        /// Returns the vertical velocity of all sprites in the window.
        /// Negative values indicate that the player is moving down (all other
        /// sprites a moving up).
        /// </returns>
        /// <param name="vel">
        ///     The current vertical velocity of all sprites in the game.
        /// </param>
        private double UpdateY(double vel)
        {
            // jump if up key pressed + vertical velocity is 0 (this only
            // happens if standing on a platform)
            double acc = 0;
            if (
                    (SplashKit.KeyDown(SplashKitSDK.KeyCode.UpKey))
                    && (vel == 0)
            ) {
                acc = 15;
            }

            // gravity
            acc -= 0.251;

            // calculate velocity from acceleration
            vel += acc;

            // reduce velocity (automatically limits max speed)
            vel *= 0.99;

            // complete initial motion update
            foreach (Sprite sprite in GetNonPlayerSprites())
            {
                sprite.Update(0, vel);
            }

            // check if collisions were caused by this movement
            bool collisionFound = false;
            foreach (Platform platform in _platforms)
            {
                if (_player.CheckCollision(platform))
                {
                    collisionFound = true;
                    break;
                }
            }

            // if no collision found - return the validated velocity
            if (!collisionFound)
            {
                return vel;
            }

            // if collision found - move back and retry
            foreach (Sprite sprite in GetNonPlayerSprites())
            {
                sprite.Update(0, vel, true);
            }

            // no valid velocity found - prevent vertical motion
            return 0;
        }
    }

    /// <summary>
    /// Represents an individual platform within a game level.
    /// </summary>
    /// <remarks>
    /// Fields + Properties
    /// <list type="bullet">
    ///     <item>+ Bmp : <c>SplashKitSDK.Bmp | null</c></item>
    ///     <item>+ Pos : <c>SplashKitSDK.Point2D Pos</c></item>
    ///     <item>+ Txt : <c>string | null</c></item>
    /// </list>
    /// 
    /// Methods
    /// <list type="bullet">
    ///     <item>+ Draw() : <c>void</c></item>
    ///     <item>- GetName(type_) : <c>string</c> &lt;&lt; static &gt;&gt;</item>
    ///     <item>+ GetRectangle() : <c>SplashKitSDK.Rectangle</c></item>
    ///     <item>+ Platform(x, y, type_, window) : <c>Platform</c></item>
    ///     <item>+ Update(vel_x, vel_y, reverse=false) : <c>void</c></item>
    /// </list>
    /// </remarks>
    /// <see cref="Level"/>
    /// <see cref="Platform"/>
    /// <see cref="Sprite"/>
    public class Platform : Sprite
    {
        /// <summary>
        /// Creates a new platform in the game window for a particular level.
        /// </summary>
        /// <param name="x">Starting X-Coordinate of the platform.</param>
        /// <param name="y">Starting Y-Coordinate of the platform.</param>
        /// <param name="type_">Type of platform being created.</param>
        /// <param name="window">
        ///     Game window to display the platform on.
        /// </param>
        /// <returns></returns>
        public Platform(
                float x,
                float y,
                PlatformType type_,
                Window window
        ) : base(x, y, GetName(type_), window) { }

        /// <summary>
        /// Gets the name of the platform bitmap file from the given platform
        /// type.
        /// </summary>
        /// <param name="type_">
        ///     Type of platform to get the filename for.
        /// </param>
        /// <returns>
        /// String containing the directory and filename of the bitmap for the
        /// given platform.
        /// </returns>
        private static string GetName(PlatformType type_)
        {
            switch (type_)
            {
                case PlatformType.Tiny:
                    return (
                        "Resources/images/Platform v2/Platform - Tiny v2." 
                        + "png"
                    );
                case PlatformType.Tiny90:
                    return (
                        "Resources/images/Platform v2/Platform - Tiny v2 90." 
                        + "png"
                    );
                case PlatformType.Small:
                    return (
                        "Resources/images/Platform v2/Platform - Sml v2." 
                        + "png"
                    );
                case PlatformType.Small90:
                    return (
                        "Resources/images/Platform v2/Platform - Sml v2 90." 
                        + "png"
                    );
                case PlatformType.Medium:
                    return (
                        "Resources/images/Platform v2/Platform - Med v2." 
                        + "png"
                    );
                case PlatformType.Medium90:
                    return (
                        "Resources/images/Platform v2/Platform - Med v2 90." 
                        + "png"
                    );
                case PlatformType.Large:
                    return (
                        "Resources/images/Platform v2/Platform - Lrg v2." 
                        + "png"
                    );
                case PlatformType.Large90:
                    return (
                        "Resources/images/Platform v2/Platform - Lrg v2 90." 
                        + "png"
                    );
                default:
                    throw new InvalidPlatformTypeException(
                        $"Invalid platform type = {type_}"
                    );
            }
        }
    }

    /// <summary>
    /// Enumeration for the different platforms that can be created in each
    /// game level.
    /// </summary>
    /// <see cref="PlatformType"/>
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

    /// <summary>
    /// Represents an individual sprite in the game when the player is in a
    /// particular level.
    /// </summary>
    /// <remarks>
    /// Fields + Properties
    /// <list type="bullet">
    ///     <item>- _window : <c>SplashKitSDK.Window</c></item>
    ///     <item>+ Bmp : <c>SplashKitSDK.Bmp | null</c></item>
    ///     <item>+ Pos : <c>SplashKitSDK.Point2D Pos</c></item>
    ///     <item>+ Txt : <c>string | null</c></item>
    /// </list>
    /// 
    /// Methods
    /// <list type="bullet">
    ///     <item>+ Draw() : <c>void</c></item>
    ///     <item>+ GetRectangle() : <c>SplashKitSDK.Rectangle</c></item>
    ///     <item>+ Sprite(x, y, bitmap_txt, window) : <c>Sprite</c></item>
    ///     <item>+ Update(vel_x, vel_y, reverse=false) : <c>void</c></item>
    /// </list>
    /// </remarks>
    /// <see cref="Level"/>
    /// <see cref="Platform"/>
    /// <see cref="Player"/>
    /// <see cref="SpriteText"/>
    public abstract class Sprite
    {
        /// <summary>
        /// Game window the sprite will be drawn on.
        /// </summary>
        private SplashKitSDK.Window _window;
        
        /// <summary>
        /// Bitmap of the sprite (if it has a bitmap). Either the <c>Txt</c> or
        /// this should be <c>null</c>, and the other should contain a value.
        /// </summary>
        public SplashKitSDK.Bitmap? Bmp { get; private set; }

        /// <summary>
        /// Position of the sprite on the game window.
        /// </summary>
        public SplashKitSDK.Point2D Pos { get; private set; }

        /// <summary>
        /// Text to display (instead of a bitmap). Either the <c>Bmp</c> or
        /// this should be <c>null</c>, and the other should contain a value.
        /// </summary>
        public string? Txt { get; private set; }

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        public void Draw()
        {
            // if the bitmap exists - draw it
            if (Bmp != null)
            {
                SplashKit.DrawBitmapOnWindow(
                    _window,
                    Bmp,
                    Pos.X,
                    Pos.Y
                );
            }

            // if the text exists - draw it
            else if (Txt != null)
            {
                SplashKit.DrawTextOnWindow(
                    _window,
                    Txt,
                    Color.Black,
                    "Arial",
                    20,
                    Pos.X - (Txt.Length * 4),
                    Pos.Y - 7
                );
            }

            // neither exists - raise error
            else
            {
                throw new InvalidSpriteDrawException(
                    "Sprite unable to be drawn"
                );
            }
        }

        /// <summary>
        /// Gets a rectangle of the bounds of the sprite which can be used for
        /// checking rectangle collisions and other constraints.
        /// </summary>
        /// <returns>
        /// Rectangle of the bounding box of the current sprite.
        /// </returns>
        public SplashKitSDK.Rectangle GetRectangle()
        {
            // use bitmap if possible
            if (Bmp != null)
            {
                return new SplashKitSDK.Rectangle(
                    Pos.X,
                    Pos.Y,
                    Bmp.Width,
                    Bmp.Height
                );
            }
            
            // unable to create a bounding box from text or other data, so
            // raise error
            throw new InvalidSpriteRectangleException(
                "Unable to create a rectangle for this sprite"
            );
        }

        /// <summary>
        /// Creates a new sprite in the game window for a particular level.
        /// </summary>
        /// <param name="x">Starting X-Coordinate of the sprite.</param>
        /// <param name="y">Starting Y-Coordinate of the sprite.</param>
        /// <param name="bitmap_txt">
        ///     Bitmap resource location (if starting with "Resources/images"),
        ///     or text to display on the sprite.
        /// </param>
        /// <param name="window">Game window to display the sprite on.</param>
        public Sprite(
            float x,
            float y,
            string bitmap_txt,
            Window window
        )
        {
            // store game window
            _window = window;

            // set bitmap and text
            if (bitmap_txt.StartsWith("Resources/images"))
            {
                Bmp = new Bitmap(
                    bitmap_txt.Split('/').Last().Split('.')[0], // image name
                    bitmap_txt // file directory + name
                );
                Txt = null;
            }
            else
            {
                Bmp = null;
                Txt = bitmap_txt;
            }

            // set initial position
            Pos = new Point2D()
            {
                X = x,
                Y = y
            };
        }
        
        /// <summary>
        /// Updates the position of the sprite in the game window.
        /// </summary>
        /// <param name="vel_x">Velocity of the sprite in the X-Axis.</param>
        /// <param name="vel_y">Velocity of the sprite in the Y-Axis.</param>
        /// <param name="reverse">
        ///     Whether or not to reverse the provided direction. Defaults to
        ///     <c>false</c>, meaning that the provided values will be used.
        /// </param>
        public void Update(double vel_x, double vel_y, bool reverse = false)
        {
            // update x-axis position
            Pos.X += (vel_x * (reverse ? -1 : 1));

            // update y-axis position
            Pos.Y += (vel_y * (reverse ? -1 : 1));
        }
    }
    
    public class Player : Sprite
    {
        public Rectangle collision_rect { get { 
            return new SplashKitSDK.Rectangle()
            {
                X = pos.X,
                Y = pos.Y,
                Width = _bitmap.Width,
                Height = _bitmap.Height
            };
        }}
        private List<Bitmap> _bmps;
        private int _anim_counter;
        private int _anim_counter_frames;
        private bool _anim_counter_inc;
        public Player(
            float x,
            float y,
            Window window
        ) : base(x, y, "Resources/images/Player.png", window)
        {
            _anim_counter = 0;
            _anim_counter_inc = true;
            _anim_counter_frames = 0;
            _bmps = new List<Bitmap>()
            {
                new Bitmap("Player_l0", "Resources/images/Player v2/l0.png"),
                new Bitmap("Player_l1", "Resources/images/Player v2/l1.png"),
                new Bitmap("Player_l2", "Resources/images/Player v2/l2.png"),
                new Bitmap("Player_r0", "Resources/images/Player v2/r0.png"),
                new Bitmap("Player_r1", "Resources/images/Player v2/r1.png"),
                new Bitmap("Player_r2", "Resources/images/Player v2/r2.png"),
            };
        }
        public bool CheckCollision(Platform other)
        {
            // return collision_rect.IntersectsWith(other.collision_rect);
            return SplashKit.BitmapRectangleCollision(
                _bmps[2],
                Pos,
                other.GetRectangle()
            ) || SplashKit.BitmapRectangleCollision(
                _bmps[0],
                Pos,
                other.GetRectangle()
            );
        }
        public bool CheckCollision(Goal other)
        {
            return SplashKit.BitmapCollision(
                _bmps[2],
                Pos,
                other.Bmp,
                other.Pos
            ) || SplashKit.BitmapCollision(
                _bmps[0],
                Pos,
                other.Bmp,
                other.Pos
            );
        }
        public void DrawPlayer(
                double vel_x,
                double vel_y
        )
        {
            // update number of frames the current player animation has played
            //  for
            _anim_counter_frames++;

            if (vel_y > 0) // jumping up - use l0 or r0
            {
                _anim_counter = 2;
                _anim_counter_inc = true;
                _anim_counter_frames = 0;
            }
            else if (vel_y < 0) // falling down - use l2 or r2
            {
                _anim_counter = 0;
                _anim_counter_inc = false;
                _anim_counter_frames = 0;
            }

            // draw based on direction
            if (vel_x >= 0) // stationary or right
            {
                SplashKit.DrawBitmapOnWindow(
                    _window,
                    _bmps[_anim_counter],
                    Pos.X,
                    Pos.Y
                );
            }
            else // left
            {
                SplashKit.DrawBitmapOnWindow(
                    _window,
                    _bmps[_anim_counter + 3],
                    Pos.X,
                    Pos.Y
                );
            }

            // update animation counter
            if (_anim_counter_frames > 5 && Math.Abs(vel_x) > 0.25)
            {
                _anim_counter_frames = 0;
                if (_anim_counter_inc)
                {
                    _anim_counter++;
                    if (_anim_counter >= 2)
                    {
                        _anim_counter_inc = false;
                        _anim_counter = 2;
                    }
                }
                else
                {
                    _anim_counter--;
                    if (_anim_counter <= 0)
                    {
                        _anim_counter_inc = true;
                        _anim_counter = 0;
                    }
                }
            }
        }
    }

    public class SpriteText : Sprite
    {
        public SpriteText(
            float x,
            float y,
            string text,
            Window window
        ) : base(x, y, text, window)
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
            // data file name
            string filename = "test2.xml";
            // create window
            Window w = new Window("Game Window", 1200, 800);

            // initialize screen
            // Screen s = Screen.Login;
            Screen s = Screen.Lvl1; // testing

            // read data
            Account.ReadData(filename);

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
                        // Console.WriteLine($"{Account.accounts}");
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
                        lvl_score = new Level(LevelNumber.L0, w).Play();
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
                            new Level(LevelNumber.L1, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl2:
                        // play level 2
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L2,
                            new Level(LevelNumber.L2, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl3:
                        // play level 3
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L3,
                            new Level(LevelNumber.L3, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl4:
                        // play level 4
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L4,
                            new Level(LevelNumber.L4, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl5:
                        // play level 5
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L5,
                            new Level(LevelNumber.L5, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl6:
                        // play level 6
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L6,
                            new Level(LevelNumber.L6, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl7:
                        // play level 7
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L7,
                            new Level(LevelNumber.L7, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl8:
                        // play level 8
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L8,
                            new Level(LevelNumber.L8, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                    case Screen.Lvl9:
                        // play level 9
                        if (a == null) { s = Screen.Login; break; }
                        a.AddScore(
                            LevelNumber.L9,
                            new Level(LevelNumber.L9, w).Play()
                        );
                        s = Screen.PlayMenu;
                        break;
                }
            }

            // Close game window
            w.Close();
            Account.SaveData(filename);
        }

        public static Account? ScreenLogin(Window w)
        {
            // create screen ui elements
            UITextBox t1 = new UITextBox("Enter Name", 600, 300, 200, 40, w);
            UIButton b_login = new UIButton(UIButtonType.Login, 600, 400, w);
            UIButton b_exit = new UIButton(UIButtonType.Exit, 600, 500, w);
            List<UIBase> ui = new List<UIBase>(){t1, b_login, b_exit};

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
                if (b_login.Clicked() || t1.submitted) {
                    if (string.IsNullOrEmpty(t1.data)) { t1.Reset(); }
                    else { return Account.GetAccount(t1.data); }
                }
                if (b_exit.Clicked()) { return null; }

                // refresh window
                w.Refresh();
            }

            return null;
        }

        public static Screen ScreenMain(Account a, Window w)
        {
            // create screen ui elements
            UIButton b_play = new UIButton(UIButtonType.Play, 450, 420, w);
            UIButton b_scores = new UIButton(UIButtonType.Highscores, 750, 420, w);
            UIButton b_settings = new UIButton(UIButtonType.Instructions, 450, 520, w);
            UIButton b_logout = new UIButton(UIButtonType.Logout, 750, 520, w);
            UIText t_welcome = new UIText($"Welcome {a.Name} to My Platformer Game", 600, 150, 400, 60, w);

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
            UIButton b_back = new UIButton(UIButtonType.Back, 600, 750, w);
            UIButton b_lvl0 = new UIButton(UIButtonType.Level1, 120, 175, w);
            UIButton b_lvl1 = new UIButton(UIButtonType.Level2, 360, 175, w);
            UIButton b_lvl2 = new UIButton(UIButtonType.Level3, 600, 175, w);
            UIButton b_lvl3 = new UIButton(UIButtonType.Level4, 840, 175, w);
            UIButton b_lvl4 = new UIButton(UIButtonType.Level5, 1080, 175, w);
            UIButton b_lvl5 = new UIButton(UIButtonType.Level6, 120, 525, w);
            UIButton b_lvl6 = new UIButton(UIButtonType.Level7, 360, 525, w);
            UIButton b_lvl7 = new UIButton(UIButtonType.Level8, 600, 525, w);
            UIButton b_lvl8 = new UIButton(UIButtonType.Level9, 840, 525, w);
            UIButton b_lvl9 = new UIButton(UIButtonType.Level10, 1080, 525, w);
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
                    ui.Add(new UIText($"Level {n+1}", x_vals[j], y_vals[i] - 125, 240, 40, w));
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
            UIButton b_back = new UIButton(UIButtonType.Back, 600, 750, w);
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
                    ui.Add(new UIText($"Level {n+1}", x_vals[j], y_vals[i] - 125, 240, 40, w));
                    ui.Add(new UIText("Your Best:", x_vals[j], y_vals[i] - 85, 240, 30, w));
                    ui.Add(new UIText(hs != null ? $"{hs.score}" : "Not Set", x_vals[j], y_vals[i] - 65, 240, 30, w));
                    ui.Add(new UIText("High Scores:", x_vals[j], y_vals[i] - 20, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 1 ? $"1st: {hs_list[0].score}" : "1st: Not Set", x_vals[j], y_vals[i] + 10, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 2 ? $"2nd: {hs_list[1].score}" : "2nd: Not Set", x_vals[j], y_vals[i] + 40, 240, 30, w));
                    ui.Add(new UIText(hs_list.Count >= 3 ? $"3rd: {hs_list[2].score}" : "3rd: Not Set", x_vals[j], y_vals[i] + 70, 240, 30, w));
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
            UIButton b_back = new UIButton(UIButtonType.Back, 600, 750, w);
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
        public Score(int score, LevelNumber level, DateTime datetime)
        {
            _datetime = datetime;
            _level = level;
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

    public enum UIButtonType {
        Back,
        Exit,
        Highscores,
        Instructions,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Login,
        Logout,
        Play,
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
        private SplashKitSDK.Bitmap _dark; // dark image
        private SplashKitSDK.Bitmap _light; // light image
        // ***********
        // Constructor
        public UIButton(
                UIButtonType type_,
                double x,
                double y,
                SplashKitSDK.Window window
        ) : base("Button Label", x, y, 200, 60, window)
        {
            string _dir = GetBitmapDirectory(type_);
            _dark = new SplashKitSDK.Bitmap($"{(int)type_}_dark", $"{_dir}/dark.png");
            _light = new SplashKitSDK.Bitmap($"{(int)type_}_light", $"{_dir}/light.png");
        }

        private string GetBitmapDirectory(UIButtonType type_)
        {
            switch (type_)
            {
                case UIButtonType.Back:
                    return "Resources/images/Buttons/Back";
                case UIButtonType.Exit:
                    return "Resources/images/Buttons/Exit";
                case UIButtonType.Highscores:
                    return "Resources/images/Buttons/Highscores";
                case UIButtonType.Instructions:
                    return "Resources/images/Buttons/Instructions";
                case UIButtonType.Level1:
                    return "Resources/images/Buttons/Level/1";
                case UIButtonType.Level2:
                    return "Resources/images/Buttons/Level/2";
                case UIButtonType.Level3:
                    return "Resources/images/Buttons/Level/3";
                case UIButtonType.Level4:
                    return "Resources/images/Buttons/Level/4";
                case UIButtonType.Level5:
                    return "Resources/images/Buttons/Level/5";
                case UIButtonType.Level6:
                    return "Resources/images/Buttons/Level/6";
                case UIButtonType.Level7:
                    return "Resources/images/Buttons/Level/7";
                case UIButtonType.Level8:
                    return "Resources/images/Buttons/Level/8";
                case UIButtonType.Level9:
                    return "Resources/images/Buttons/Level/9";
                case UIButtonType.Level10:
                    return "Resources/images/Buttons/Level/10";
                case UIButtonType.Login:
                    return "Resources/images/Buttons/Login";
                case UIButtonType.Logout:
                    return "Resources/images/Buttons/Logout";
                case UIButtonType.Play:
                    return "Resources/images/Buttons/Play";
                default:
                    return "";
            }
        }

        // ***************
        // Draw UI Element
        public override void Draw()
        {
            // draw button fill + border
            if (MouseOver())
            {
                SplashKit.DrawBitmapOnWindow(
                    _window,
                    _dark,
                    _rect.X,
                    _rect.Y
                );
            }
            else
            {
                SplashKit.DrawBitmapOnWindow(
                    _window,
                    _light,
                    _rect.X,
                    _rect.Y
                );
            }
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
