/// <summary>
/// Program Class
/// </summary>

using System;
using System.Security.Principal;
using CustomProgramTetris;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        new Window("Tetris", 500, 600);

        GameManager gameManager = GameManager.GetInstance();// get instance of the game manager

        do
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();

            if (SplashKit.KeyTyped(KeyCode.RightKey)) // move shape right
            {
                gameManager.InputProcessor("right");
            }
            if (SplashKit.KeyTyped(KeyCode.LeftKey)) // move shape left
            {
                gameManager.InputProcessor("left");
            }
            if (SplashKit.KeyTyped(KeyCode.UpKey)) // rotate the shape right
            {
                gameManager.InputProcessor("rotate");
            }
            if (SplashKit.KeyDown(KeyCode.DownKey)) // speed the shape up
            {
                gameManager.InputProcessor("softDrop");
            }
            if (SplashKit.KeyDown(KeyCode.SpaceKey)) // drop the shape
            {
                gameManager.InputProcessor("hardDrop");
            }

            gameManager.UpdateGame();
            SplashKit.RefreshScreen();
        } while (!SplashKit.WindowCloseRequested("Tetris"));

    }
}
