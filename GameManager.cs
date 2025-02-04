/// <summary>
/// GameManager Class
/// updates the game based on its state
/// </summary>

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SplashKitSDK;

namespace CustomProgramTetris
{
    public class GameManager
    {
        private static GameManager instance; // holds only instance of the class

        // GameManager fields
        private Board _board; // the board that tetris is played on
        private Tetromino _currentTet; // the current shape that is falling
        private Tetromino _nextTet; // the next shape to fall
        private GameStatus _gameStatus; // holds an value from the enum to determine state of the game
        private GameScreen _gameScreen;
        private Counter _time;
        private Counter _lines;
        private Counter _score;

        // making instances of each tetromino
        static Tetromino tetI = new Tetromino(1, Color.RGBColor(244, 166, 166), new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } });
        static Tetromino tetJ = new Tetromino(2, Color.RGBColor(241, 184, 127), new int[3, 3] { { 0, 0, 2 }, { 2, 2, 2 }, { 0, 0, 0 } });
        static Tetromino tetL = new Tetromino(3, Color.RGBColor(193, 218, 130), new int[3, 3] { { 3, 3, 3 }, { 0, 0, 3 }, { 0, 0, 0 } });
        static Tetromino tetO = new Tetromino(4, Color.RGBColor(136, 212, 194), new int[2, 2] { { 4, 4 }, { 4, 4 } });
        static Tetromino tetS = new Tetromino(5, Color.RGBColor(154, 192, 230), new int[3, 3] { { 0, 5, 0 }, { 5, 5, 0 }, { 5, 0, 0 } });
        static Tetromino tetT = new Tetromino(6, Color.RGBColor(180, 175, 223), new int[3, 3] { { 6, 0, 0 }, { 6, 6, 0 }, { 6, 0, 0 } });
        static Tetromino tetZ = new Tetromino(7, Color.RGBColor(242, 168, 209), new int[3, 3] { { 7, 0, 0 }, { 7, 7, 0 }, { 0, 7, 0 } });

        public static Dictionary<int, Tetromino> tetType = new Dictionary<int, Tetromino> // dictionary of the tetrominoes
        {
            { 1, tetI },
            { 2, tetJ },
            { 3, tetL },
            { 4, tetO },
            { 5, tetS },
            { 6, tetT },
            { 7, tetZ }
        };

        private GameManager()
        {
            _board = new Board();
            _currentTet = GetRandomTetromino();
            _nextTet = GetRandomTetromino();
            _gameStatus = GameStatus.NEW_SHAPE;
            _gameScreen = new GameScreen();
            _time = new Counter();
            _lines = new Counter();
            _score = new Counter();
        }

        // used to create the only instance of the class (singleton design pattern)
        public static GameManager GetInstance()
        {
            if (instance == null) // if there is no instance then create one
            {
                instance = new GameManager();
            }
            return instance;
        }

        public void UpdateGame()
        {
            _gameScreen.DrawBackground();
            if (_gameStatus != GameStatus.GAME_OVER) // if its not game over
            {
                _board.DrawBoard();
                _gameScreen.DrawScore(_lines, _score);

                if (_gameStatus == GameStatus.NEW_SHAPE)
                {
                    DropNewShape();
                }
                else if (_gameStatus == GameStatus.SHAPE_LANDED)
                {
                    ShapeLanded();
                }
            }
            else
            {
                _gameScreen.DrawGameOver(_lines, _score);
            }
        }

        public void DropNewShape()
        {
            _currentTet.DrawTetromino(); // draw piece
            _gameScreen.DrawNextTetromino(_nextTet); // draw next piece on the side

            if (_time.Count == 500) // move the piece down at certain intervals
            {
                _currentTet.Y++; // move piece down
                _time.Reset(); // reset time counter

                if (_board.CheckCells(_currentTet) == CellStatus.FULL) // check cells below the shape are empty
                {
                    _currentTet.Y--;
                    _gameStatus = _board.AddToGameBoard(_currentTet); // piece has landed so add it to the board
                }
            }
            else
            {
                _time.Increment(1); // increase time counter
            }
        }

        public void ShapeLanded()
        {
            int numLines = 0; // holds the number of lines just completed
            numLines += _board.CheckLines();
            _lines.Increment(numLines); // increase total number of lines

            _time.Reset();

            // increase score based on number of lines completed
            switch(numLines) 
            {
                case 1:
                    _score.Increment(40);
                    break;
                case 2:
                    _score.Increment(100);
                    break;
                case 3:
                    _score.Increment(300);
                    break;
                case 4:
                    _score.Increment(1200);
                    break;
                default:
                    _score.Increment(0);
                    break;
            }

            _currentTet = _nextTet; // next piece becomes the current piece
            _nextTet = GetRandomTetromino(); // get a new next piece
            _gameStatus = GameStatus.NEW_SHAPE; // change game status to shape falling
        }

        public void InputProcessor(string input)
        {
            if (input == "right")
            {
                _currentTet.MoveRight(_board);
            }
            else if (input == "left")
            {
                _currentTet.MoveLeft(_board);
            }
            else if (input == "rotate")
            {
                _currentTet.Rotate(_board);
            }
            else if (input == "softDrop")
            {
                _currentTet.SoftDrop(_board);
            }
            else if (input == "hardDrop")
            {
                _currentTet.HardDrop(_board);
            }
        }

        public static Tetromino GetRandomTetromino() // gets the type from the dictionary using the name and creates an instance
        {
            int rndNumber = SplashKit.Rnd(1, 8);
            Tetromino rndTet = tetType[rndNumber];
            Tetromino newTet = new Tetromino(rndTet.Type, rndTet.Color, rndTet.Shape);
            return newTet;
        }
    }
}
