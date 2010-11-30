using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public partial class SudokuSolver : Form
    {
        protected struct Number
        {
            int Value;
            TextBox ValueBox;
        }
        private const int CELLS = 9;
        private const int CELL_ROWS = 3;
        private const int CELL_COLUMNS = 3;
        private const int CELL_WIDTH = 60;
        private const int CELL_HEIGHT = 60;

        private const int BLOCKS = 9;
        private const int BLOCK_ROWS = 3;
        private const int BLOCK_COLUMNS = 3;
        private const int BLOCK_WIDTH = CELL_WIDTH * BLOCK_COLUMNS;
        private const int BLOCK_HEIGHT = CELL_HEIGHT * BLOCK_ROWS;
        
        protected Number[,] Numbers = new Number[9,9];

        public SudokuSolver()
        {
            InitializeComponent();
        }

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

        public void SetupCells(Panel block, int row, int column)
        {
            string _FontFamily = "Calibri";
            float _FontSize = 22.5f;

            for (int i = 0; i < CELL_ROWS; i++)
            {
                for (int j = 0; j < CELL_COLUMNS; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Top = CELL_HEIGHT * i;
                    tb.Left = CELL_WIDTH * j;
                    tb.Text = (row+i).ToString() + "," + (column+j).ToString();
                    tb.Multiline = true;
                    tb.Width = CELL_WIDTH;
                    tb.Height = CELL_HEIGHT;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new Font(_FontFamily, _FontSize);
                    block.Controls.Add(tb);
                }
            }
        }

        private void SudokuSolver_Load(object sender, EventArgs e)
        {
            SetupBlocks();
        }
    }
}
