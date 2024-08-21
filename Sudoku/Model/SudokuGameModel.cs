using ELTE.Forms.Sudoku.Persistence;
using System;
using System.Threading.Tasks;

namespace ELTE.Forms.Sudoku.Model
{
    public class SudokuGameModel
    {
        private const int GameTimeEasy = 3600;
        private const int GameTimeMedium = 1200;
        private const int GameTimeHard = 600;
        private const int GeneratedFieldCountEasy = 6;
        private const int GeneratedFieldCountMedium = 12;
        private const int GeneratedFieldCountHard = 18;

        private ISudokuDataAccess _dataAccess;
        private SudokuTable _table;
        private GameDifficulty _gameDifficulty;
        private int _gameStepCount;
        private int _gameTime;

        public SudokuGameModel(ISudokuDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new SudokuTable();
            _gameDifficulty = GameDifficulty.Medium;
        }

        public event EventHandler<SudokuEventArgs> GameAdvanced;

        public event EventHandler<SudokuEventArgs> GameOver;

        public int GameStepCount
        { get { return _gameStepCount; } }

        public int GameTime
        { get { return _gameTime; } }

        public SudokuTable Table
        { get { return _table; } }

        public bool IsGameOver
        { get { return (_gameTime == 0 || _table.IsFilled); } }

        public GameDifficulty GameDifficulty
        { get { return _gameDifficulty; } set { _gameDifficulty = value; } }

        public void NewGame()
        {
            _table = new SudokuTable();
            _gameStepCount = 0;

            switch (_gameDifficulty) // nehézségfüggő beállítása az időnek, illetve a generált mezőknek
            {
                case GameDifficulty.Easy:
                    _gameTime = GameTimeEasy;
                    GenerateFields(GeneratedFieldCountEasy);
                    break;

                case GameDifficulty.Medium:
                    _gameTime = GameTimeMedium;
                    GenerateFields(GeneratedFieldCountMedium);
                    break;

                case GameDifficulty.Hard:
                    _gameTime = GameTimeHard;
                    GenerateFields(GeneratedFieldCountHard);
                    break;
            }
        }

        public void AdvanceTime()
        {
            if (IsGameOver)
                return;

            _gameTime--;
            OnGameAdvanced();

            if (_gameTime == 0)
                OnGameOver(false);
        }

        public void Step(int x, int y)
        {
            if (IsGameOver)
                return;
            if (_table.IsLocked(x, y))
                return;

            _table.StepValue(x, y);

            _gameStepCount++;

            OnGameAdvanced();

            if (_table.IsFilled)
            {
                OnGameOver(true);
            }
        }

        public async Task LoadGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.Load(path);
            _gameStepCount = 0;

            switch (_gameDifficulty)
            {
                case GameDifficulty.Easy:
                    _gameTime = GameTimeEasy;
                    break;

                case GameDifficulty.Medium:
                    _gameTime = GameTimeMedium;
                    break;

                case GameDifficulty.Hard:
                    _gameTime = GameTimeHard;
                    break;
            }
        }

        public async Task SaveGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.Save(path, _table);
        }

        private void GenerateFields(int count)
        {
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                int x, y;

                do
                {
                    x = random.Next(_table.Size);
                    y = random.Next(_table.Size);
                }
                while (!_table.IsEmpty(x, y));

                do
                {
                    for (var j = random.Next(10) + 1; j >= 0; j--)
                    {
                        _table.StepValue(x, y);
                    }
                }
                while (_table.IsEmpty(x, y));

                _table.SetLock(x, y);
            }
        }

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new SudokuEventArgs(false, _gameStepCount, _gameTime));
        }

        private void OnGameOver(bool isWon)
        {
            if (GameOver != null)
                GameOver(this, new SudokuEventArgs(isWon, _gameStepCount, _gameTime));
        }
    }
}