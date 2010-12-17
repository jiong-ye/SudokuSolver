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
                    tb.Font = new Font(CELL_FONT_FAMILY, CELL_FONT_SIZE);
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
            if (!Numbers[row, column].Cell.InvokeRequired)
            {
                Numbers[row, column].Value = value;
                Numbers[row, column].State = state;
                if (value > 0 && value < 10)
                    Numbers[row, column].Cell.Text = value.ToString();
                else
                    Numbers[row, column].Cell.Text = string.Empty;
            }
            else
            {
                SetCellValueDelegate del = new SetCellValueDelegate(SetCellValue);
                Numbers[row,column].Cell.Invoke(del, new object[] { row, column, value, state });
            }
        }

        delegate void SetCellValueDelegate(int row, int column, int value, CellState state);

        //set value and state of a cell, for possible values
        void SetCellValue(int row, int column, string value, CellState state)
        {
            Numbers[row, column].State = state;
            Numbers[row, column].Cell.Text = value.ToString();
        }

        //set cell style
        void SetCellStyle(int row, int column, CellStyleState StyleState)
        {
            Color BackgroundColor = Color.White;
            TextBox tb = Numbers[row, column].Cell;

            if (!tb.InvokeRequired)
            {
                tb.Font = new Font(CELL_FONT_FAMILY, CELL_FONT_SIZE);
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
                    case CellStyleState.ShowedPossibles:
                        BackgroundColor = Color.Coral;
                        tb.Font = new Font(CELL_FONT_FAMILY, CELL_FONT_SIZE_SMALL);
                        break;
                    case CellStyleState.Guessed:
                        BackgroundColor = Color.DarkSeaGreen;
                        break;
                    case CellStyleState.Guessing:
                        BackgroundColor = Color.LightCoral;
                        break;
                    default:
                        break;
                }
                tb.BackColor = BackgroundColor;
                tb.Refresh();
            }
            else
            {
                SetCellStyleDelegate del = new SetCellStyleDelegate(SetCellStyle);
                tb.Invoke(del, new object[] { row, column, StyleState });
            }
        }
        delegate void SetCellStyleDelegate(int row, int column, CellStyleState StyleState);

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

        //get the coordinate of cell by its name
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

        List<Pointx> GetPossibleCellsBySiblings(List<Pointx> CellSiblings, int TargetBlock)
        {
            List<Pointx> PossibleCells = new List<Pointx>();

            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].Block == TargetBlock && Numbers[i,j].State == CellState.Empty)
                    {
                        Boolean DoNotIntersect = true;

                        foreach (Pointx cell in CellSiblings)
                        {
                            if (cell.X == i || cell.Y == j)
                                DoNotIntersect = false;
                        }

                        if (DoNotIntersect)
                            PossibleCells.Add(new Pointx(i, j));
                    }
                }
            }

            return PossibleCells;
        }
    }
}
