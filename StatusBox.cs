using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        void AppendStatus(string message)
        {
            if (message.Length > 0)
            {
                StatusBox.Text += message + Environment.NewLine;
                StatusBox.SelectionStart = StatusBox.Text.Length;
                StatusBox.ScrollToCaret();
            }
        }
    }
}
