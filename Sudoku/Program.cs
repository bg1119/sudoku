﻿using System;
using System.Windows.Forms;

namespace ELTE.Forms.Sudoku
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View.GameForm());
        }
    }
}
