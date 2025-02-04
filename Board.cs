/// <summary>
/// Board Class
/// creates and draws the grid on the screen
/// also draws the shapes when the have landed and hence been added to the board
/// </summary>

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using SplashKitSDK;

namespace CustomProgramTetris
{
    public class Board
    {
        private const int _rows = 20, _columns = 10, _cellSize = 30;
        private int[,] _gameBoard;

        public Board()
        {
            _gameBoard = new int[_columns, _rows];

            CreateBoard();
        }

        public void CreateBoard()
        {
            for (int col = 0; col < _columns; col++)
            {
                for (int row = 0; row < _rows; row++)
                {
                    _gameBoard[col, row] = 0; // set each cell as empty
                }
            }
        }

        public void DrawBoard()
        {
            for (int col = 0; col < _columns; col++)
            {
                for (int row = 0; row < _rows; row++)
                {
                    Color color = Color.RGBColor(47, 49, 54);

                    foreach (int i in GameManager.tetType.Keys)
                    {
                        if (i == _gameBoard[col, row])
                        {
                            Tetromino tet = GameManager.tetType[i]; // creates an instance to get its color
                            color = tet.Color;
                        }
                    }

                    SplashKit.FillRectangle(color, col * _cellSize, row * _cellSize, _cellSize, _cellSize); // fills in the cell with the relevant color
                    SplashKit.DrawRectangle(Color.White, col * _cellSize, row * _cellSize, _cellSize, _cellSize); // draws the grid for the board
                }
            }
        }

        public CellStatus CheckCells(Tetromino tet) // checks the state of the cells around the shape
        {
            for (int x = 0; x < tet.Length; x++)
            {
                for (int y = 0; y < tet.Length; y++)
                {
                    if (tet.Shape[x, y] != 0)
                    {
                        if ((tet.X + x) < 0 || (tet.X + x) >= _columns) // if shape is outside width of board
                        {
                            return CellStatus.NONEXISTENT;
                        }
                        else if (tet.Y + y >= _rows || _gameBoard[tet.X + x, tet.Y + y] != 0) // if shape is at bottom of board or on another piece
                        {
                            return CellStatus.FULL;
                        }
                    }
                }
            }
            return CellStatus.EMPTY;
        }

        public GameStatus AddToGameBoard(Tetromino tet) // adds a shape that has landed to the game board
        {
            for (int x = 0; x < tet.Length; x++)
            {
                for (int y = 0; y < tet.Length; y++)
                {
                    if (tet.Shape[x, y] != 0)
                    {
                        if (tet.Y + y == 0)
                        {
                            return GameStatus.GAME_OVER; // if it landed at the top then game over
                        }
                        else
                        {
                            foreach (int i in GameManager.tetType.Keys)
                            {
                                if (i == tet.Shape[x, y])
                                {
                                    _gameBoard[tet.X + x, tet.Y + y] = i; // set the cell number to the correct number for the shape
                                }
                            }
                        }
                    }
                }
            }
            return GameStatus.SHAPE_LANDED;
        }

        public int CheckLines() // checks if there is a full line
        {
            int lines = 0;

            for (int row = 0; row < _rows; row++)
            {
                bool isFull = true;
                for (int col = 0; col < _columns; col++)
                {
                    if (_gameBoard[col, row] == 0)
                    {
                        isFull = false;
                    }
                }

                if (isFull) // if the line is full
                {
                    isFull = false;
                    lines++; // increase lines count

                    for (int y = row; y > 0; y--)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            _gameBoard[x, y] = _gameBoard[x, y - 1]; // move all the above lines down a row
                        }
                    }
                }
            }
            return lines;
        }

        public int[,] GameBoard
        {
            get
            {
                return _gameBoard;
            }
        }
    }
}
