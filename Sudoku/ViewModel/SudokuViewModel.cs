using ELTE.Windows.Sudoku.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ELTE.Windows.Sudoku.ViewModel
{
    /// <summary>
    /// Sudoku nézetmodell típusa.
    /// </summary>
    public class SudokuViewModel : ViewModelBase
    {
        #region Fields

        private Dispatcher _dispatcher;
        private SudokuGameModel _model; // modell

        #endregion

        #region Properties

        /// <summary>
        /// Új játék kezdése parancs lekérdezése.
        /// </summary>
        public DelegateCommand NewGameCommand { get; private set; }

        /// <summary>
        /// Játék betöltése parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Játék mentése parancs lekérdezése.
        /// </summary>
        public DelegateCommand SaveGameCommand { get; private set; }

        /// <summary>
        /// Kilépés parancs lekérdezése.
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }

        /// <summary>
        /// Játékmező gyűjtemény lekérdezése.
        /// </summary>
        public ObservableCollection<SudokuField> Fields { get; set; }

        /// <summary>
        /// Lépések számának lekérdezése.
        /// </summary>
        public Int32 GameStepCount { get { return _model.GameStepCount; } }

        /// <summary>
        /// Fennmaradt játékidő lekérdezése.
        /// </summary>
        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }

        /// <summary>
        /// Alacsony nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameEasy
        {
            get { return _model.GameDifficulty == GameDifficulty.Easy; }
            set
            {
                if (_model.GameDifficulty == GameDifficulty.Easy)
                    return;

                _model.GameDifficulty = GameDifficulty.Easy;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        /// <summary>
        /// Közepes nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameMedium
        {
            get { return _model.GameDifficulty == GameDifficulty.Medium; }
            set
            {
                if (_model.GameDifficulty == GameDifficulty.Medium)
                    return;

                _model.GameDifficulty = GameDifficulty.Medium;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        /// <summary>
        /// Magas nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameHard
        {
            get { return _model.GameDifficulty == GameDifficulty.Hard; }
            set
            {
                if (_model.GameDifficulty == GameDifficulty.Hard)
                    return;

                _model.GameDifficulty = GameDifficulty.Hard;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Új játék eseménye.
        /// </summary>
        public event EventHandler NewGame;

        /// <summary>
        /// Játék betöltésének eseménye.
        /// </summary>
        public event EventHandler LoadGame;

        /// <summary>
        /// Játék mentésének eseménye.
        /// </summary>
        public event EventHandler SaveGame;

        /// <summary>
        /// Játékból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitGame;

        #endregion

        #region Constructors

        /// <summary>
        /// Sudoku nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell típusa.</param>
        public SudokuViewModel(SudokuGameModel model)
        {
            // játék csatlakoztatása
            _model = model;
            _model.GameAdvanced += new EventHandler<SudokuEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<SudokuEventArgs>(Model_GameOver);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => { OnNewGame(); RefreshTable(); });
            LoadGameCommand = new DelegateCommand(param => { OnLoadGame(); RefreshTable(); });
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());

            // játéktábla létrehozása
            Fields = new ObservableCollection<SudokuField>();
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new SudokuField
                    {
                        IsLocked = true,
                        Text = String.Empty,
                        X = i,
                        Y = j,
                        Number = i * _model.Table.Size + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }

            RefreshTable();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Tábla frissítése.
        /// </summary>
        private void RefreshTable()
        {
            foreach (SudokuField field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.IsLocked = _model.Table.IsLocked(field.X, field.Y);
            }

            OnPropertyChanged("GameTime");
            OnPropertyChanged("GameStepCount");
        }

        /// <summary>
        /// Játék léptetése eseménykiváltása.
        /// </summary>
        /// <param name="index">A lépett mező indexe.</param>
        private void StepGame(Int32 index)
        {
            SudokuField field = Fields[index];

            _model.Step(field.X, field.Y);

            field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást

            field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
        }

        #endregion

        #region Game event handlers

        /// <summary>
        /// Játék végének eseménykezelője.
        /// </summary>
        private void Model_GameOver(object sender, SudokuEventArgs e)
        {
            foreach (SudokuField field in Fields)
            {
                field.IsLocked = true; // minden mezőt lezárunk
            }
        }

        /// <summary>
        /// Játék előrehaladásának eseménykezelője.
        /// </summary>
        private void Model_GameAdvanced(object sender, SudokuEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        #endregion

        #region Event methods

        /// <summary>
        /// Új játék indításának eseménykiváltása.
        /// </summary>
        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }

        

        /// <summary>
        /// Játék betöltése eseménykiváltása.
        /// </summary>
        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játék mentése eseménykiváltása.
        /// </summary>
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játékból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitGame()
        {
            if (ExitGame != null)
                ExitGame(this, EventArgs.Empty);
        }

        #endregion
    }
}
