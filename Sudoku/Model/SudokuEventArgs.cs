using System;

namespace ELTE.Forms.Sudoku.Model
{
    public class SudokuEventArgs : EventArgs
    {
        private int _gameTime;
        private int _steps;
        private bool _isWon;

        public SudokuEventArgs(bool isWon, int gameStepCount, int gameTime)
        {
            _isWon = isWon;
            _steps = gameStepCount;
            _gameTime = gameTime;
        }

        public int GameTime
        { get { return _gameTime; } }

        public int GameStepCount
        { get { return _steps; } }

        public bool IsWon
        { get { return _isWon; } }
    }
}