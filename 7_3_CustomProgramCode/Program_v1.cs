// /* ****************************************************************************
//  * 7.3 - Custom Program Code
//  * Platformer Game.
//  * The below code is used to run a platformer game, containing multiple levels
//  * in which the player jumps around avoiding enemies. The player has a limited
//  * amount of lives and can collect power-ups. The game ends when the player
//  * runs out of lives or reaches the end of the level.
//  * 
//  * The game features:
//  * - A player character with a jumping and moving animation.
//  * - Platforms that the player can jump on.
//  * - Enemies that the player has to avoid.
//  * - Power-ups that the player can collect.
//  * - Sound effects and music.
//  * - Score tracking and display.
//  * - Levels with varying difficulty levels and obstacles.
//  * - A pause menu with options to resume, restart, or quit the game.
//  * - A game over screen with a score and option to play again.
//  * - A high score system that saves and loads the top scores.
//  * - Customizable game settings such as difficulty, sound effects, and music volume.
//  * - A tutorial screen that explains the game's controls and rules.
//  * 
//  * Author: Shaun Altmann
//  * ***************************************************************************/

// using System;
// using SplashKitSDK;

// namespace _7_3_CustomProgramCode
// {
//     /* ************************************************************************
//      * Individual Level Class
//      * ***********************************************************************/
//     public class Level
//     {
//         /*
//             Level
//             -
//             Represents a level in the game.

//             Constants
//             -
//             None

//             Fields
//             -
//             - _lvl : `LevelNumber`
//                 - Level number indicating the level to be played in the game.
//             - _platforms : `List<Platform>`
//                 - List of platforms that exist in given level.
//             - _player : `Player`
//                 - Player for the current level.
//             - _timer : `Timer`
//                 - Timer used to indicate the amount of time that has elapsed in
//                     the level.
//             - LevelNumber : `enum`
//                 - Enumerator containing a list of all levels to be played.

//             Properties
//             -
//             None

//             Methods
//             -
//             - Level(lvl)
//                 - Constructor Method.
//                 - Creates a new level for the given level number.
//             - Update() : `void`
//                 - Instance Method.
//                 - Updates the positions of all sprites in the level, as well as
//                     handling all collisions and other object interactions.
//         */

//         // Constants

//         // Fields
//         private LevelNumber _lvl;
//         private List<Platform> _platforms;
//         private Player _player;
//         private SplashKitSDK.Timer _timer;
//         private SplashKitSDK.Window _w;
//         public enum LevelNumber { Level_1, Level_2, Level_3 }

//         // Properties

//         // Constructor
//         public Level(Window w, LevelNumber lvl)
//         {
//             // initialize window, level number, timer
//             _lvl = lvl;
//             _timer = SplashKit.CreateTimer($"level {lvl} timer");
//             _w = w;

//             // create level player + platforms
//             switch (lvl) {
//                 case LevelNumber.Level_1:
//                     _player = new Player(new Point2D() { X = 100, Y = 700 });
//                     _platforms = new List<Platform>
//                     {
//                         new Platform(
//                             new Point2D() { X = 300, Y = 700 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 600, Y = 600 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 900, Y = 600 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 1100, Y = 500 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 900, Y = 400 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 1100, Y = 300 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 900, Y = 200 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 650, Y = 200 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 400, Y = 200 },
//                             Platform.PlatformType.Med
//                         ),
//                         new Platform(
//                             new Point2D() { X = 150, Y = 400 },
//                             Platform.PlatformType.Sml
//                         )
//                     };
//                     break;
//                 case LevelNumber.Level_2:
//                     _player = new Player(new Point2D() { X = 100, Y = 700 });
//                     _platforms = new List<Platform>
//                     {
//                     };
//                     break;
//                 case LevelNumber.Level_3:
//                     _player = new Player(new Point2D() { X = 100, Y = 700 });
//                     _platforms = new List<Platform>
//                     {
//                     };
//                     break;
//                 default:
//                     _player = new Player(new Point2D() { X = 100, Y = 700 });
//                     _platforms = new List<Platform>
//                     {
//                     };
//                     break;
//             }
//         }

//         // Update Level
//         public void Update()
//         {
//         }
//     }
//     /* ************************************************************************
//      * Platform Class
//      * ***********************************************************************/
//     public class Platform : Sprite
//     {
//         /*
//             Platform
//             -
//             Represents a platform in the game.

//             Constants
//             -
//             - GRAVITY : `double`
//                 - `Sprite` Constant.
//                 - Acceleration due to gravity that affects all sprites.

//             Fields
//             -
//             - _bmp : `Bitmap`
//                 - `Sprite` Field.
//                 - Bitmap used to display the sprite.
//             - _pos : `Point2D`
//                 - `Sprite` Field.
//                 - Position of the sprite on the screen (centre).
//             - PlatformType : `enum`
//                 - Public Enumeration Field.
//                 - Type of platform to generate.

//             Properties
//             -
//             - bitmap : `Bitmap`
//                 - `Sprite` Property.
//                 - Bitmap used to display the sprite.
//             - height : `int`
//                 - `Sprite` Property.
//                 - Height of the sprite.
//             - width : `int`
//                 - `Sprite` Property.
//                 - Width of the sprite.
//             - x : `double`
//                 - `Sprite` Property.
//                 - X-Coordinate of the sprite.
//             - y : `double`
//                 - `Sprite` Property.
//                 - Y-Coordinate of the sprite.

//             Methods
//             -
//             - Draw() : `void`
//                 - `Sprite` Public Instance Method.
//                 - Draws the sprite to the screen at its current position.
//             - GetFilename(type) : `string`
//                 - Private Static Method.
//                 - Get the filename of the bitmap image.
//             - GetName(type) : `string`
//                 - Private Static Method.
//                 - Get name of the bitmap from the given type.
//             - Update() : `void`
//                 - `Sprite` Public Virtual Instance Method.
//                 - Updates the position of the sprite on the screen.
//         */

//         // Fields
//         public enum PlatformType { Tiny, Sml, Med, Lrg }

//         // Constructor
//         public Platform(Point2D pos, PlatformType type) : base(
//                 GetName(type),
//                 GetFilename(type),
//                 pos
//         )
//         {
//         }

//         // Get Image Filename from Type
//         private static string GetFilename(PlatformType type)
//         {
//             return type switch
//             {
//                 PlatformType.Tiny => "Resources/images/Platform - Tiny.png",
//                 PlatformType.Sml => "Resources/images/Platform - Sml.png",
//                 PlatformType.Med => "Resources/images/Platform - Med.png",
//                 PlatformType.Lrg => "Resources/images/Platform - Lrg.png",
//                 _ => "Resources/images/Platform - Tiny.png"
//             };
//         }

//         // Get Image Name from Type
//         private static string GetName(PlatformType type)
//         {
//             return type switch
//             {
//                 PlatformType.Tiny => "Platform - Tiny",
//                 PlatformType.Sml => "Platform - Small",
//                 PlatformType.Med => "Platform - Medium",
//                 PlatformType.Lrg => "Platform - Large",
//                 _ => "Platform - Tiny"
//             };
//         }

//         // Update Sprite Position
//         public override void Update()
//         {

//         }
//     }
//     /* ************************************************************************
//      * Player Class
//      * ***********************************************************************/
//     public class Player : Sprite
//     {
//         /*
//             Player
//             -
//             Represents the player in the game.

//             Constants
//             -
//             - ACC : `double`
//                 - Acceleration of the player when they move around.
//             - GRAVITY : `double`
//                 - `Sprite` Constant.
//                 - Acceleration due to gravity that affects all sprites.
//             - JUMP : `double`
//                 - The vertical acceleration of the player when they jump.
//             - MAX_SPEED : `double`
//                 - Maximum speed of the player.

//             Fields
//             -
//             - _bmp : `Bitmap`
//                 - `Sprite` Field.
//                 - Bitmap used to display the sprite.
//             - _lives : `int`
//                 - Number of lives the player has remaining.
//             - _pos : `Point2D`
//                 - `Sprite` Field.
//                 - Position of the sprite on the screen (centre).

//             Properties
//             -
//             - bitmap : `Bitmap`
//                 - `Sprite` Property.
//                 - Bitmap used to display the sprite.
//             - height : `int`
//                 - `Sprite` Property.
//                 - Height of the sprite.
//             - width : `int`
//                 - `Sprite` Property.
//                 - Width of the sprite.
//             - x : `double`
//                 - `Sprite` Property.
//                 - X-Coordinate of the sprite.
//             - y : `double`
//                 - `Sprite` Property.
//                 - Y-Coordinate of the sprite.

//             Methods
//             -
//             - Draw() : `void`
//                 - `Sprite` Public Instance Method.
//                 - Draws the sprite to the screen at its current position.
//             - Update() : `void`
//                 - `Sprite` Public Virtual Instance Method.
//                 - Updates the position of the sprite on the screen.
//         */

//         // Constants
//         private const double ACC = 0.5;
//         private const double JUMP = 5.0;
//         private const double MAX_SPEED = 2.0;

//         // Constructor
//         public Player(Point2D pos) : base(
//                 "Player",
//                 "Resources/images/Player.png",
//                 pos
//         )
//         {
//         }

//         // Update Sprite Position
//         public override void Update()
//         {

//         }
//     }

//     public class Program
//     {
//         public static void Main()
//         {

//         }
//     }

//     /* ************************************************************************
//      * Sprite Class
//      * ***********************************************************************/
//     public abstract class Sprite
//     {
//         /*
//             Sprite
//             -
//             Represents a single generic sprite object which can be displayed on
//             the screen.

//             Constants
//             -
//             - GRAVITY : `double`
//                 - Acceleration due to gravity that affects all sprites.

//             Fields
//             -
//             - _bmp : `Bitmap`
//                 - Bitmap used to display the sprite.
//             - _pos : `Point2D`
//                 - Position of the sprite on the screen (centre).

//             Properties
//             -
//             - bitmap : `Bitmap`
//                 - Bitmap used to display the sprite.
//             - height : `int`
//                 - Height of the sprite.
//             - width : `int`
//                 - Width of the sprite.
//             - x : `double`
//                 - X-Coordinate of the sprite.
//             - y : `double`
//                 - Y-Coordinate of the sprite.

//             Methods
//             -
//             - Draw() : `void`
//                 - Public Instance Method.
//                 - Draws the sprite to the screen at its current position.
//             - Update() : `void`
//                 - Public Abstract Instance Method.
//                 - Updates the position of the sprite on the screen.
//         */

//         // Constants
//         private const double GRAVITY = 0.5;

//         // Fields
//         private Bitmap _bmp;
//         private Point2D _pos;

//         // Properties
//         public Bitmap bitmap { get { return _bmp; } }
//         public int height { get { return _bmp.Height; } }
//         public int width { get { return _bmp.Width; } }
//         public double x { get { return _pos.X; } }
//         public double y { get { return _pos.Y; } }

//         // Constructor
//         public Sprite(string bp_name, string bp_filename, Point2D pos)
//         {
//             _bmp = new Bitmap(bp_name, bp_filename);
//             _pos = pos;
//         }

//         // Draw Sprite
//         public void Draw()
//         {
//             SplashKit.DrawBitmap(_bmp, x - (width / 2), y - (height / 2));
//         }

//         // Update Sprite Position
//         public abstract void Update();
//     }
// }
