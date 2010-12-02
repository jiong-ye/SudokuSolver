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
        //set up number cells of a block
        public void SetupCells(Panel block, int row, int column)
        {
            string _FontFamily = "Calibri";
            float _FontSize = CELL_FONT_FIZE;
            int x, y;

            for (int i = 0; i < CELL_ROWS; i++)
            {
                for (int j = 0; j < CELL_COLUMNS; j++)
                {
                    x = row * CELL_ROWS + i;
                    y = column * CELL_COLUMNS + j;

                    //set up value box
                    TextBox tb = new TextBox();
                    tb.Name = "Cell_" + x.ToString() + "_" + y.ToString();
                    tb.Top = CELL_HEIGHT * i;
                    tb.Left = CELL_WIDTH * j;
                    tb.TabIndex = x * 10 + y;
                    tb.Multiline = true;
                    tb.AcceptsReturn = false;
                    tb.Width = CELL_WIDTH;
                    tb.Height = CELL_HEIGHT;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new Font(_FontFamily, _FontSize);
                    tb.KeyDown += new KeyEventHandler(ValidateTextbox);
                    block.Controls.Add(tb);

                    Numbers[x, y].Cell = tb;
                    Numbers[x, y].State = CellState.Empty;
                    Numbers[x, y].StyleState = CellStyleState.Normal;
                    Numbers[x, y].Value = 0;
                    Numbers[x, y].Block = row * CELL_ROWS + column;
                }
            }
        }

        //set value and state of a cell
        void SetCellValue(string name, int value, CellState state)
        {
            Pointx c = GetCellCoordinateByName(name);

            if (c.Filled)
                SetCellValue(c.X, c.Y, value, state);
        }

        //set value and state of a cell
        void SetCellValue(int row, int column, int value, CellState state)
        {
            Numbers[row, column].Value = value;
            Numbers[row, column].State = state;
            Numbers[row, column].Cell.Text = value.ToString();

            if (state == CellState.Solved)
            {
                SetCellStyle(Numbers[row, column].Cell, CellStyleState.Solved);
            }
        }

        //set cell style
        void SetCellStyle(TextBox tb, CellStyleState StyleState)
        {
            Color BackgroundColor = Color.White;
            switch (StyleState)
            {
                case CellStyleState.Solved:
                    BackgroundColor = Color.LightGreen;
                    break;
                case CellStyleState.Conflicted:
                    BackgroundColor = Color.Tan;
                    break;
                case CellStyleState.Checked:
                    BackgroundColor = Color.Gainsboro;
                    break;
                default:
                    break;
            }
            tb.BackColor = BackgroundColor;
        }

        //clear cell style
        void ClearCellStyle(CellStyleState StyleState)
        {
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].StyleState == StyleState)
                    {
                        Numbers[i, j].StyleState = CellStyleState.Normal;
                        Numbers[i, j].Cell.BackColor = Color.White;
                    }
                }
            }
        }

        Pointx GetCellCoordinateByName(string name)
        {
            Pointx coord = new Pointx();

            char[] NameSplit = { '_' };
            string[] NameSplitted = name.Split(NameSplit);

            if (NameSplitted.Length == 3)
            {
                int row, column;

                if (int.TryParse(NameSplitted[1], out row) && int.TryParse(NameSplitted[2], out column))
                {
                    coord.X = row;
                    coord.Y = column;
                    coord.Filled = true;
                }
            }
            return coord;
        }
    }
}
