using System;

namespace ELTE.Forms.Sudoku.Persistence
{
    public class SudokuTable
    {
        private int _regionSize;
        private int[,] _fieldValues;
        private bool[,] _fieldLocks;

        public bool IsFilled
        {
            get
            {
                foreach (var value in _fieldValues)
                    if (value == 0)
                        return false;
                return true;
            }
        }

        public int RegionSize
        { get { return _regionSize; } }

        public int Size
        { get { return _fieldValues.GetLength(0); } }

        public int this[int x, int y]
        { get { return GetValue(x, y); } }

        public SudokuTable() : this(9, 3) { }

        public SudokuTable(int tableSize, int regionSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
            if (regionSize < 0)
                throw new ArgumentOutOfRangeException("The region size is less than 0.", "regionSize");
            if (regionSize > tableSize)
                throw new ArgumentOutOfRangeException("The region size is grater than the table size.", "regionSize");
            if (tableSize % regionSize != 0)
                throw new ArgumentException("The table size is not a multiple of the region size.", "regionSize");

            _regionSize = regionSize;
            _fieldValues = new int[tableSize, tableSize];
            _fieldLocks = new bool[tableSize, tableSize];
        }

        public bool IsEmpty(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 0;
        }

        public bool IsLocked(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldLocks[x, y];
        }

        public int GetValue(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }

        public void SetValue(int x, int y, int value, bool lockField)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value < 0 || value > _fieldValues.GetLength(0) + 1)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");
            if (_fieldLocks[x, y])
                return;
            if (!CheckStep(x, y))
                return;

            _fieldValues[x, y] = value;
            _fieldLocks[x, y] = lockField;
        }

        public void StepValue(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            if (_fieldLocks[x, y])
                return;

            do
            {
                _fieldValues[x, y] = (_fieldValues[x, y] + 1) % (_fieldValues.GetLength(0) + 1); // ciklikus generálás
            }
            while (!CheckStep(x, y));
        }

        public void SetLock(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            _fieldLocks[x, y] = true;
        }

        private bool CheckStep(int x, int y)
        {
            if (_fieldValues[x, y] == 0)
                return true;
            else
            {
                for (var i = 0; i < _fieldValues.GetLength(0); i++)
                    if (_fieldValues[i, y] == _fieldValues[x, y] && x != i)
                        return false;

                for (var j = 0; j < _fieldValues.GetLength(1); j++)
                    if (_fieldValues[x, j] == _fieldValues[x, y] && y != j)
                        return false;

                for (var i = _regionSize * (x / _regionSize); i < _regionSize * ((x + 1) / _regionSize); i++)
                    for (var j = _regionSize * (y / _regionSize); j < _regionSize * ((y + 1) / _regionSize); j++)
                    {
                        if (_fieldValues[i, j] == _fieldValues[x, y] && x != i && y != j)
                            return false;
                    }

                return true;
            }
        }
    }
}