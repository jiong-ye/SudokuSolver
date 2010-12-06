using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //check if a column contains a number
        Boolean ColumnContainsNumber(int column, int number)
        {
            for (int i = 0; i < BLOCK_COLUMNS * CELL_COLUMNS; i++)
            {
                if (Numbers[i, column].Value == number)
                    return true;
            }
            return false;
        }
    }
}
