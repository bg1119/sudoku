using ELTE.Forms.Sudoku.Persistence;
using System;
using System.Threading.Tasks;

namespace ELTE.Forms.Sudoku.Model
{
    /// <summary>
    /// Sudoku játék típusa.
    /// </summary>
    public class SudokuGameModel
    {
        private const int GameTimeEasy = 3600;
        private const int GameTimeMedium = 1200;
        private const int GameTimeHard = 600;
        private const int GeneratedFieldCountEasy = 6;
        private const int GeneratedFieldCountMedium = 12;
        private const int GeneratedFieldCountHard = 18;

        private ISudokuDataAccess _dataAccess; // adatelérés
        private SudokuTable _table; // játéktábla
        private GameDifficulty _gameDifficulty; // nehézség
        private int _gameStepCount; // lépések száma
        private int _gameTime; // játékidő

        /// <summary>
        /// Sudoku játék példányosítása.
        /// </summary>
        /// <param name="dataAccess">Az adatelérés.</param>
        public SudokuGameModel(ISudokuDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new SudokuTable();
            _gameDifficulty = GameDifficulty.Medium;
        }

        /// <summary>
        /// Játék előrehaladásának eseménye.
        /// </summary>
        public event EventHandler<SudokuEventArgs> GameAdvanced;

        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler<SudokuEventArgs> GameOver;

        /// <summary>
        /// Lépések számának lekérdezése.
        /// </summary>
        public int GameStepCount
        { get { return _gameStepCount; } }

        /// <summary>
        /// Hátramaradt játékidő lekérdezése.
        /// </summary>
        public int GameTime
        { get { return _gameTime; } }

        /// <summary>
        /// Játéktábla lekérdezése.
        /// </summary>
        public SudokuTable Table
        { get { return _table; } }

        /// <summary>
        /// Játék végének lekérdezése.
        /// </summary>
        public bool IsGameOver
        { get { return (_gameTime == 0 || _table.IsFilled); } }

        /// <summary>
        /// Játéknehézség lekérdezése, vagy beállítása.
        /// </summary>
        public GameDifficulty GameDifficulty
        { get { return _gameDifficulty; } set { _gameDifficulty = value; } }

        /// <summary>
        /// Új játék kezdése.
        /// </summary>
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

        /// <summary>
        /// Játékidő léptetése.
        /// </summary>
        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            _gameTime--;
            OnGameAdvanced();

            if (_gameTime == 0) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        /// <summary>
        /// Táblabeli lépés végrehajtása.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        public void Step(int x, int y)
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
            if (_table.IsLocked(x, y)) // ha a mező zárolva van, nem léthatünk
                return;

            _table.StepValue(x, y);

            _gameStepCount++; // lépésszám növelés

            OnGameAdvanced();

            if (_table.IsFilled) // ha vége a játéknak, jelezzük, hogy győztünk
            {
                OnGameOver(true);
            }
        }

        /// <summary>
        /// Játék betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public async Task LoadGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.Load(path);
            _gameStepCount = 0;

            switch (_gameDifficulty) // játékidő beállítása
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

        /// <summary>
        /// Játék mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public async Task SaveGame(string path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.Save(path, _table);
        }

        /// <summary>
        /// Mezők generálása.
        /// </summary>
        /// <param name="count">Mezők száma.</param>
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
                while (!_table.IsEmpty(x, y)); // üres mező véletlenszerű kezelése

                do
                {
                    for (var j = random.Next(10) + 1; j >= 0; j--) // véletlenszerű növelés
                    {
                        _table.StepValue(x, y);
                    }
                }
                while (_table.IsEmpty(x, y));

                _table.SetLock(x, y);
            }
        }

        /// <summary>
        /// Játékidő változás eseményének kiváltása.
        /// </summary>
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new SudokuEventArgs(false, _gameStepCount, _gameTime));
        }

        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Győztünk-e a játékban.</param>
        private void OnGameOver(bool isWon)
        {
            if (GameOver != null)
                GameOver(this, new SudokuEventArgs(isWon, _gameStepCount, _gameTime));
        }
    }
}