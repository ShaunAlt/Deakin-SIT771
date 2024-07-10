using System;
using SplashKitSDK;

namespace _1_4_MakeAScene
{
    public class Program
    {
        public static void Main()
        {
            // initialize window, background, and ball position
            float[,] ball_positions = new float[4,2] {
                {100, 300}, // player 1
                {300, 500}, // player 2
                {500, 100}, // player 3
                {920, 300}, // goal
            };
            float ball_x;
            float ball_y;
            Bitmap bg = new Bitmap("Background", "SoccerField.jpg");
            int NUM_STEPS = 100;
            SoundEffect sound = new SoundEffect("Sound", "applause4.wav");
            Window w = new Window("Make a Scene - Shaun Altmann", 960, 600);
            w.Refresh();

            // animation
            for (int i = 0; i < 3; i++) { // 3 players
                for (int j = 0; j < NUM_STEPS; j++) { // move ball
                    ball_x = (
                        ball_positions[i, 0]
                        + (
                            (ball_positions[i+1, 0] - ball_positions[i, 0])
                            * j / NUM_STEPS
                        )
                    );
                    ball_y = (
                        ball_positions[i, 1]
                        + (
                            (ball_positions[i+1, 1] - ball_positions[i, 1])
                            * j / NUM_STEPS
                        )
                    );
                    DrawScreen(w, bg, ball_x, ball_y);
                    w.Refresh();
                    SplashKit.Delay(100);
                }
            }

            // goal scored + play sound
            DrawScreen(w, bg, ball_positions[3, 0], ball_positions[3, 1]);
            w.Refresh();
            SplashKit.Delay(100);
            sound.Play();
            SplashKit.Delay(5000);
        }

        // draw screen
        public static void DrawScreen(
                Window w,
                Bitmap bg,
                float ball_x,
                float ball_y
        ) {
            // draw background
            w.DrawBitmap(bg, -480, -320, SplashKit.OptionScaleBmp(0.5, 0.5));

            // draw players
            w.FillCircle(Color.Black, 100, 300, 5);
            w.FillCircle(Color.Black, 300, 500, 5);
            w.FillCircle(Color.Black, 500, 100, 5);

            // draw ball
            w.FillCircle(Color.White, ball_x, ball_y, 3);
        }
    }
}
