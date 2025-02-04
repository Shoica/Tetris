/// <summary>
/// GameScreen Class
/// draws things to the screen that are not part of the playable game
/// for example the score and next piece but not the piece that is falling or the game board
/// </summary>

using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace CustomProgramTetris
{
    public class GameScreen
    {
        public GameScreen()
        {
        }

        public void DrawBackground() // draws the background color
        {
            SplashKit.FillRectangle(Color.RGBColor(54, 57, 63), 0, 0, 500, 600);
        }

        public void DrawNextTetromino(Tetromino nextTet) // draws the next tetromino on the side
        {
            SplashKit.DrawText("Next piece: ", Color.White, "Normal Font", 30, 320, 60);
            SplashKit.FillRectangle(Color.RGBColor(47, 49, 54), 320, 70, 160, 160); // draw fill color for square that next shape sits in
            SplashKit.DrawRectangle(Color.White, 320, 70, 160, 160); // draw border for square

            // create an instance of the same shape to draw on the side so that it does not effect the shape when it drops
            Tetromino tet = new Tetromino(nextTet.Type, nextTet.Color, nextTet.Shape);
            tet.X = 12;
            tet.Y = 3;
            tet.DrawTetromino();
        }

        public void DrawScore(Counter lines, Counter score)
        {
            SplashKit.DrawText("Lines: " + lines.Count, Color.White, "Normal Font", 30, 330, 300); // draw the lines count
            SplashKit.DrawText("Score: " + score.Count, Color.White, "Normal Font", 30, 330, 320); // draw the score count
        }

        public void DrawGameOver(Counter lines, Counter score) // draw a new screen when the player loses
        {
            SplashKit.FillRectangle(Color.RGBColor(47, 49, 54), 20, 20, 460, 560);
            SplashKit.DrawRectangle(Color.White, 20, 20, 460, 560);
            SplashKit.DrawText("Game Over!", Color.White, "Normal Font", 30, 200, 100);
            SplashKit.DrawText("Lines: " + lines.Count, Color.White, "Normal Font", 30, 200, 120);
            SplashKit.DrawText("Score: " + score.Count, Color.White, "Normal Font", 30, 200, 140);
        }
    }
}
