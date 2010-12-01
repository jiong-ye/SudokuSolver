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
        protected int[,] TestNumbers = { 
                                           {0,0,0,0,0,5,0,3,7},
                                           {0,0,6,0,0,0,0,0,0},
                                           {0,5,0,3,7,0,1,2,0},
                                           {0,0,0,0,4,0,7,0,2},
                                           {0,0,0,0,1,8,6,0,4},
                                           {0,0,0,5,0,0,0,8,0},
                                           {0,4,1,0,0,0,0,0,0},
                                           {0,0,0,0,0,0,8,7,3},
                                           {0,0,0,0,8,0,0,0,1}
                                       };

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
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (TestNumbers[i, j] > 0)
                    {
                        Numbers[i, j].Cell.Text = TestNumbers[i, j].ToString();
                        Numbers[i, j].State = CellState.Set;
                        Numbers[i, j].Value = TestNumbers[i, j];
                    }
                }
            }
        }


        void SetCellState(string CellName, CellState state)
        {

        }

        //set up 9 blocks to contain 9 blocks of numbers
        public void SetupBlocks()
        {
            for (int i = 0; i < BLOCK_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS; j++)
                {
                    Panel block = GetNewBlock(i, j);
                    this.Controls.Add(block);
                }
            }
        }

        //set up a single block of numbers
        public Panel GetNewBlock(int row, int column)
        {
            Panel block = new Panel();

            //set block styles
            block.BorderStyle = BorderStyle.FixedSingle;
            block.Width = BLOCK_WIDTH;
            block.Height = BLOCK_HEIGHT;
            block.Top = BLOCK_HEIGHT * row + (row + 1) * 1;
            block.Left = BLOCK_WIDTH * column + (column + 1) * 1;

            SetupCells(block, row, column);

            return block;
        }

        //set up number cells of a block
        public void SetupCells(Panel block, int row, int column)
        {
            string _FontFamily = "Calibri";
            float _FontSize = 28.5f;
            int x, y;

            for (int i = 0; i < CELL_ROWS; i++)
            {
                for (int j = 0; j < CELL_COLUMNS; j++)
                {
                    x = row * CELL_ROWS + i;
                    y = column * CELL_COLUMNS + j;

                    //set up text box
                    TextBox tb = new TextBox();
                    tb.Name = "Cell_" + x.ToString() + "_" + y.ToString();
                    tb.Top = CELL_HEIGHT * i;
                    tb.Left = CELL_WIDTH * j;
                    tb.TabIndex = x * 10 + y;
                    //tb.Text = x.ToString() + "," + y.ToString();
                    //tb.Text = (row*CELL_ROWS+column).ToString();
                    tb.Multiline = true;
                    tb.AcceptsReturn = false;
                    tb.Width = CELL_WIDTH;
                    tb.Height = CELL_HEIGHT;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new Font(_FontFamily, _FontSize);
                    tb.KeyDown += new KeyEventHandler(ValidateTextbox);

                    Numbers[x, y].Cell = tb;
                    Numbers[x, y].State = CellState.Empty;
                    Numbers[x, y].StyleState = CellStyleState.Normal;
                    Numbers[x, y].Value = 0;
                    Numbers[x, y].Block = row * CELL_ROWS + column;

                    block.Controls.Add(tb);
                }
            }
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
                
                if(IsLegalValue(tb.Name, number))
                    SetCellValue(tb.Name, number, CellState.Set);

                e.SuppressKeyPress = true;
            }
        }

        //set value and state of a cell
        void SetCellValue(string name, int value, CellState state)
        {
            Point c = GetCellCoordinateByName(name);
           
            if(!c.IsEmpty)
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

        Point GetCellCoordinateByName(string name)
        {
            Point coord = new Point();
            char[] NameSplit = { '_' };
            string[] NameSplitted = name.Split(NameSplit);

            if (NameSplitted.Length == 3)
            {
                int row, column;

                if (int.TryParse(NameSplitted[1], out row) && int.TryParse(NameSplitted[2], out column))
                {
                    coord.X = row;
                    coord.Y = column;
                }
            }
            return coord;
        }
    }
}
