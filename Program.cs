using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SudokuSolver ss = new SudokuSolver();

            Application.Run(ss);
        }
    }
}
