using ELTE.Windows.Sudoku.Model;
using ELTE.Windows.Sudoku.Persistence;
using ELTE.Windows.Sudoku.View;
using ELTE.Windows.Sudoku.ViewModel;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace ELTE.Windows.Sudoku
{
    /// <summary>
    /// Alkalmazás típusa.
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private SudokuGameModel _model;
        private SudokuViewModel _viewModel;
        private MainWindow _view;
        private DispatcherTimer _timer;

        #endregion

        #region Constructors

        /// <summary>
        /// Alkalmazás példányosítása.
        /// </summary>
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #endregion

        #region Application event handlers

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new SudokuGameModel(new SudokuFileDataAccess());
            _model.GameOver += new EventHandler<SudokuEventArgs>(Model_GameOver);
            _model.NewGame();

            // nézemodell létrehozása
            _viewModel = new SudokuViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();

            // időzítő létrehozása
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        #endregion

        #region View event handlers

        /// <summary>
        /// Nézet bezárásának eseménykezelője.
        /// </summary>
        private void View_Closing(object sender, CancelEventArgs e)
        {
            Boolean restartTimer = _timer.IsEnabled;

            _timer.Stop();

            if (MessageBox.Show("Biztos, hogy ki akar lépni?", "Sudoku", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true; // töröljük a bezárást

                if (restartTimer) // ha szükséges, elindítjuk az időzítőt
                    _timer.Start();
            }
        }

        #endregion

        #region ViewModel event handlers

        /// <summary>
        /// Új játék indításának eseménykezelője.
        /// </summary>
        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            _model.NewGame();
            _timer.Start();
        }

        /// <summary>
        /// Játék betöltésének eseménykezelője.
        /// </summary>
        private async void ViewModel_LoadGame(object sender, System.EventArgs e)
        {
            Boolean restartTimer = _timer.IsEnabled;

            _timer.Stop();

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // dialógusablak
                openFileDialog.Title = "Sudoku tábla betöltése";
                openFileDialog.Filter = "Sudoku tábla|*.stl";
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await _model.LoadGame(openFileDialog.FileName);

                    _timer.Start();
                }
            }
            catch (SudokuDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restartTimer) // ha szükséges, elindítjuk az időzítőt
                _timer.Start();
        }

        /// <summary>
        /// Játék mentésének eseménykezelője.
        /// </summary>
        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            Boolean restartTimer = _timer.IsEnabled;

            _timer.Stop();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                saveFileDialog.Title = "Sudoku tábla betöltése";
                saveFileDialog.Filter = "Sudoku tábla|*.stl";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // játéktábla mentése
                        await _model.SaveGame(saveFileDialog.FileName);
                    }
                    catch (SudokuDataException)
                    {
                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restartTimer) // ha szükséges, elindítjuk az időzítőt
                _timer.Start();
        }

        /// <summary>
        /// Játékból való kilépés eseménykezelője.
        /// </summary>
        private void ViewModel_ExitGame(object sender, System.EventArgs e)
        {
            _view.Close(); // ablak bezárása
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// Játék végének eseménykezelője.
        /// </summary>
        private void Model_GameOver(object sender, SudokuEventArgs e)
        {
            _timer.Stop();

            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                "Összesen " + e.GameStepCount + " lépést tettél meg és " +
                                TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                "Sudoku játék",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("Sajnálom, vesztettél, lejárt az idő!",
                                "Sudoku játék",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
            }
        }

        #endregion
    }
}
