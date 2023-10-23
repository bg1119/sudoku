using System;

namespace ELTE.Forms.Sudoku.Model
{
    /// <summary>
    /// Sudoku eseményargumentum típusa.
    /// </summary>
    public class SudokuEventArgs : EventArgs
    {
        private int _gameTime;
        private int _steps;
        private bool _isWon;

        /// <summary>
        /// Sudoku eseményargumentum példányosítása.
        /// </summary>
        /// <param name="isWon">Győzelem lekérdezése.</param>
        /// <param name="gameStepCount">Lépésszám.</param>
        /// <param name="gameTime">Játékidő.</param>
        public SudokuEventArgs(bool isWon, int gameStepCount, int gameTime)
        {
            _isWon = isWon;
            _steps = gameStepCount;
            _gameTime = gameTime;
        }

        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public int GameTime
        { get { return _gameTime; } }

        /// <summary>
        /// Játéklépések számának lekérdezése.
        /// </summary>
        public int GameStepCount
        { get { return _steps; } }

        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public bool IsWon
        { get { return _isWon; } }
    }
}