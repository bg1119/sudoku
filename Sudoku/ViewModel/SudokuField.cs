using System;
using System.Windows.Media;

namespace ELTE.Windows.Sudoku.ViewModel
{
    /// <summary>
    /// Sudoku játékmező típusa.
    /// </summary>
    public class SudokuField : ViewModelBase
    {
        private Boolean _isLocked;
        private String _text;

        /// <summary>
        /// Zártolság lekérdezése, vagy beállítása.
        /// </summary>
        public Boolean IsLocked 
        {
            get { return _isLocked; }
            set 
            {
                if (_isLocked != value)
                {
                    _isLocked = value;
                    OnPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Felirat lekérdezése, vagy beállítása.
        /// </summary>
        public String Text 
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value; 
                    OnPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Vízszintes koordináta lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// Függőleges koordináta lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 Y { get; set; }

        /// <summary>
        /// Sorszám lekérdezése.
        /// </summary>
        public Int32 Number { get; set; }

        /// <summary>
        /// Lépés parancs lekérdezése, vagy beállítása.
        /// </summary>
        public DelegateCommand StepCommand { get; set; }
    }
}
