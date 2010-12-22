using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        private void SetOriginalNumber(Boolean set)
        {
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    string value = Numbers[i, j].Cell.Text.Trim();
                    int number;

                    if (Int32.TryParse(value, out number))
                    {
                        if (number >= 1 && number <= 9)
                        {
                            if (set)
                                Numbers[i, j].Cell.Enabled = false;
                            else
                                Numbers[i, j].Cell.Enabled = true;
                        }
                    }
                }
            }
        }

        //set test numbers, so i dont have to enter them manually everytime.
        private void SetTestNumber()
        {
            int which = 0;

            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (TestNumbers[which , i, j] > 0)
                    {
                        Numbers[i, j].Cell.Text = TestNumbers[which, i, j].ToString();
                        Numbers[i, j].State = CellState.Set;
                        Numbers[i, j].Value = TestNumbers[which, i, j];
                    }
                }
            }
            AppendStatus("Test Numbers Loaded");
        }
        
        //validate textbox, only allow 1 - 9
        void ValidateTextbox(object sender, KeyEventArgs e)
        {
            Keys[] AllowedKey = { Keys.Delete, Keys.Back, Keys.Clear, Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            TextBox tb = (TextBox)sender;
            string value = tb.Text.Trim();

            if (Array.IndexOf(AllowedKey, e.KeyData) < 0)
            {
                int number = 0;

                if (e.KeyValue >= 49 && e.KeyValue <= 57)
                    number = e.KeyValue - 48;
                else if (e.KeyValue >= 97 && e.KeyValue <= 105)
                    number = e.KeyValue - 96;

                if (IsLegalValue(tb.Name, number))
                {
                    SetCellValue(tb.Name, number, CellState.Set);
                    CellUnsolved--;
                }

                e.SuppressKeyPress = true;
            }
        }

        
    }
}
