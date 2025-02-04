/// <summary>
/// Tetromino Class
/// implements getting a shape, drawing it, and interacting with is
/// </summary>

using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace CustomProgramTetris
{
    public class Tetromino
    {
        private int _x, _y, _type, _length;
        private Counter _count;
        private Color _color;
        private int[,] _shape;

        public Tetromino(int type, Color color, int[,] shape)
        {
            _x = 4;
            _y = 0;
            _count = new Counter();
            _type = type;
            _color = color;
            _shape = shape;
            _length = _shape.GetLength(0);
        }

        public void DrawTetromino()
        {
            for (int col = 0; col < this.Length; col++)
            {
                for (int row = 0; row < this.Length; row++)
                {
                    if (this.Shape[col, row] != 0)
                    {
                        SplashKit.FillRectangle(_color, (col + _x) * 30, (row + _y) * 30, 30, 30); // 30 is the size of each cell on the grid
                        SplashKit.DrawRectangle(Color.White, (col + _x) * 30, (row + _y) * 30, 30, 30); // draws the grid on shape
                    }
                }
            }
        }

        // method to move the tetromino right
        public Tetromino MoveRight(Board board)
        {
            _x++;
            if (board.CheckCells(this) == CellStatus.FULL || board.CheckCells(this) == CellStatus.NONEXISTENT)// check if still within the board and not on another piece
            {
                _x--;
            }

            return this;
        }

        // method to move the tetromino left
        public Tetromino MoveLeft(Board board)
        {
            _x--;
            if (board.CheckCells(this) == CellStatus.FULL || board.CheckCells(this) == CellStatus.NONEXISTENT) // check if still within the board and not on another piece
            {
                _x++;
            }

            return this;
        }

        // method to rotate the tetromino clockwise
        public Tetromino Rotate(Board board)
        {
            int tetDim = _length;
            int[,] rotatedTet = new int[tetDim, tetDim];
            int[,] originalTet = _shape;

            for (int x = 0; x < tetDim; x++)
            {
                for (int y = 0; y < tetDim; y++)
                {
                    rotatedTet[x, y] = this.Shape[y, tetDim - 1 - x];
                }
            }

            this.Shape = rotatedTet;
            if (board.CheckCells(this) == CellStatus.FULL || board.CheckCells(this) == CellStatus.NONEXISTENT) // check if still within the board and not on another piece
            {
                this.Shape = originalTet;
            }

            return this;
        }

        public Tetromino SoftDrop(Board board)
        {
            if (_count.Count == 100)
            {
                _y++;
                _count.Reset();
                if (board.CheckCells(this) == CellStatus.FULL || board.CheckCells(this) == CellStatus.NONEXISTENT) // check if still within the board and not on another piece
                {
                    _y--;
                }
            }
            _count.Increment(1);
            return this;
        }

        public Tetromino HardDrop(Board board)
        {
            _y++;
            if (board.CheckCells(this) == CellStatus.FULL || board.CheckCells(this) == CellStatus.NONEXISTENT) // check if still within the board and not on another piece
            {
                _y--;
            }
            return this;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public int Type
        {
            get
            {
                return _type;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
        }

        public int[,] Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;
            }
        }

        public int Length
        {
            get
            {
                return _length;
            }
        }
    }
}
