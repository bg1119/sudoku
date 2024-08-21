using System;

namespace ELTE.Forms.Sudoku.Model
{
    public class SudokuEventArgs : EventArgs
    {
        public SudokuEventArgs(bool isWon, int gameStepCount, int gameTime)
        {
            IsWon = isWon;
            GameStepCount = gameStepCount;
            GameTime = gameTime;
        }

        public int GameTime { get; }

        public int GameStepCount { get; }

        public bool IsWon { get; }
    }
}