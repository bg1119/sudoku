using ELTE.Forms.Sudoku.Model;
using ELTE.Forms.Sudoku.Persistence;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ELTE.Forms.Sudoku.View
{
    public partial class GameForm : Form
    {
        private ISudokuDataAccess _dataAccess;
        private SudokuGameModel _model;
        private Button[,] _buttonGrid;
        private Timer _timer;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _dataAccess = new SudokuFileDataAccess();

            _model = new SudokuGameModel(_dataAccess);
            _model.GameAdvanced += Game_GameAdvanced;
            _model.GameOver += Game_GameOver;

            _timer = new Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;

            GenerateTable();
            SetupMenus();

            _model.NewGame();
            SetupTable();

            _timer.Start();
        }

        private void Game_GameAdvanced(object sender, SudokuEventArgs e)
        {
            _toolLabelGameTime.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
            _toolLabelGameSteps.Text = e.GameStepCount.ToString();
        }

        private void Game_GameOver(object sender, SudokuEventArgs e)
        {
            _timer.Stop();

            foreach (var button in _buttonGrid)
                button.Enabled = false;

            _menuFileSaveGame.Enabled = false;

            if (e.IsWon)
            {
                MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                "Összesen " + e.GameStepCount + " lépést tettél meg és " +
                                TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                "Sudoku játék",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                return;
            }

            MessageBox.Show("Sajnálom, vesztettél, lejárt az idő!",
                "Sudoku játék",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk);
        }

        private void ButtonGrid_MouseClick(object sender, MouseEventArgs e)
        {
            var x = (((Button)sender).TabIndex - 100) / _model.Table.Size;
            var y = (((Button)sender).TabIndex - 100) % _model.Table.Size;

            _model.Step(x, y);

            _buttonGrid[x, y].Text = _model.Table.IsEmpty(x, y) ? string.Empty : _model.Table[x, y].ToString();
        }

        private void MenuFileNewGame_Click(object sender, EventArgs e)
        {
            _menuFileSaveGame.Enabled = true;

            _model.NewGame();
            SetupTable();

            _timer.Start();
        }

        private async void MenuFileLoadGame_Click(object sender, EventArgs e)
        {
            var restartTimer = _timer.Enabled;
            _timer.Stop();

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGame(_openFileDialog.FileName);
                    _menuFileSaveGame.Enabled = true;
                }
                catch (SudokuDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _model.NewGame();
                    _menuFileSaveGame.Enabled = true;
                }

                SetupTable();
            }

            if (restartTimer)
                _timer.Start();
        }

        private async void MenuFileSaveGame_Click(object sender, EventArgs e)
        {
            var restartTimer = _timer.Enabled;
            _timer.Stop();

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGame(_saveFileDialog.FileName);
                }
                catch (SudokuDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (restartTimer)
                _timer.Start();
        }

        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            var restartTimer = _timer.Enabled;
            _timer.Stop();

            if (MessageBox.Show("Biztosan ki szeretne lépni?", "Sudoku játék", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                if (restartTimer)
                    _timer.Start();
            }
        }

        private void MenuGameEasy_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Easy;
        }

        private void MenuGameMedium_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Medium;
        }

        private void MenuGameHard_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Hard;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void GenerateTable()
        {
            _buttonGrid = new Button[_model.Table.Size, _model.Table.Size];
            for (var i = 0; i < _model.Table.Size; i++)
                for (var j = 0; j < _model.Table.Size; j++)
                {
                    var iBlock = i / 3;
                    var jBlock = j / 3;
                    _buttonGrid[i, j] = new Button
                    {
                        Location = new Point(5 + 50 * j + jBlock * 10, 25 + 50 * i + iBlock * 10),
                        Size = new Size(50, 50),
                        Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold),
                        Enabled = false,
                        TabIndex = 100 + i * _model.Table.Size + j,
                        FlatStyle = FlatStyle.Flat
                    };
                    _buttonGrid[i, j].MouseClick += ButtonGrid_MouseClick;

                    Controls.Add(_buttonGrid[i, j]);
                }
        }

        private void SetupTable()
        {
            for (var i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (var j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    if (_model.Table.IsEmpty(i, j))
                    {
                        _buttonGrid[i, j].Text = string.Empty;
                        _buttonGrid[i, j].Enabled = true;
                        _buttonGrid[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        _buttonGrid[i, j].Text = _model.Table[i, j].ToString();
                        _buttonGrid[i, j].Enabled = false;
                        _buttonGrid[i, j].BackColor = Color.Yellow;
                    }
                }
            }

            _toolLabelGameSteps.Text = _model.GameStepCount.ToString();
            _toolLabelGameTime.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
        }

        private void SetupMenus()
        {
            _menuGameEasy.Checked = (_model.GameDifficulty == GameDifficulty.Easy);
            _menuGameMedium.Checked = (_model.GameDifficulty == GameDifficulty.Medium);
            _menuGameHard.Checked = (_model.GameDifficulty == GameDifficulty.Hard);
        }
    }
}