using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        Boolean RowContainsNumber(int row, int number)
        {
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                if (Numbers[row, i].Value == number)
                    return true;
            }
            return false;
        }
    }
}
