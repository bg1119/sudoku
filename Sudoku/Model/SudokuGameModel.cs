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

        private readonly ISudokuDataAccess _dataAccess;

        public SudokuGameModel(ISudokuDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            Table = new SudokuTable();
            GameDifficulty = GameDifficulty.Medium;
        }

        public event EventHandler<SudokuEventArgs> GameAdvanced;

        public event EventHandler<SudokuEventArgs> GameOver;

        public int GameStepCount { get; private set; }

        public int GameTime { get; private set; }

        public SudokuTable Table { get; private set; }

        public bool IsGameOver => (GameTime == 0 || Table.IsFilled);

        public GameDifficulty GameDifficulty { get; set; }

        public bool IsInEditMode { get; set; }

        public void NewGame()
        {
            Table = new SudokuTable();
            GameStepCount = 0;

            switch (GameDifficulty)
            {
                case GameDifficulty.Easy:
                    GameTime = GameTimeEasy;
                    GenerateFields(GeneratedFieldCountEasy);
                    break;

                case GameDifficulty.Medium:
                    GameTime = GameTimeMedium;
                    GenerateFields(GeneratedFieldCountMedium);
                    break;

                case GameDifficulty.Hard:
                    GameTime = GameTimeHard;
                    GenerateFields(GeneratedFieldCountHard);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AdvanceTime()
        {
            if (IsGameOver)
                return;

            GameTime--;
            OnGameAdvanced();

            if (GameTime == 0)
                OnGameOver(false);
        }

        public void Step(int x, int y, bool direction = true)
        {
            if (IsGameOver)
                return;
            if (!IsInEditMode && Table.IsLocked(x, y))
                return;

            if (direction)
            {
                Table.StepValue(x, y);
            }
            else
            {
                Table.StepBackValue(x, y);
            }

            GameStepCount++;

            OnGameAdvanced();

            if (Table.IsFilled)
            {
                OnGameOver(true);
            }
        }

        public void SetLock(int x, int y)
        {
            Table.SetLock(x,y);
        }

        public async Task LoadGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            Table = await _dataAccess.Load(path);
            GameStepCount = 0;

            switch (GameDifficulty)
            {
                case GameDifficulty.Easy:
                    GameTime = GameTimeEasy;
                    break;

                case GameDifficulty.Medium:
                    GameTime = GameTimeMedium;
                    break;

                case GameDifficulty.Hard:
                    GameTime = GameTimeHard;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task SaveGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.Save(path, Table);
        }

        private void GenerateFields(int count)
        {
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                int x, y;

                do
                {
                    x = random.Next(Table.Size);
                    y = random.Next(Table.Size);
                }
                while (!Table.IsEmpty(x, y));

                do
                {
                    for (var j = random.Next(10) + 1; j >= 0; j--)
                    {
                        Table.StepValue(x, y);
                    }
                }
                while (Table.IsEmpty(x, y));

                Table.SetLock(x, y);
            }
        }

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new SudokuEventArgs(false, GameStepCount, GameTime));
        }

        private void OnGameOver(bool isWon)
        {
            GameOver?.Invoke(this, new SudokuEventArgs(isWon, GameStepCount, GameTime));
        }
    }
}