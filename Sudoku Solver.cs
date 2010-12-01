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
            public int Value;
            public TextBox ValueBox;
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

        protected Number[,] Numbers = new Number[9, 9];

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
            float _FontSize = 25.5f;
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
                    //tb.Text = (x * 10 + y).ToString();
                    tb.Multiline = true;
                    tb.Width = CELL_WIDTH;
                    tb.Height = CELL_HEIGHT;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new Font(_FontFamily, _FontSize);
                    tb.KeyDown += new KeyEventHandler(ValidateTextbox);

                    Numbers[x, y].Value = 0;
                    Numbers[x, y].ValueBox = tb;

                    block.Controls.Add(tb);
                }
            }
        }

        void ValidateTextbox(object sender, KeyEventArgs e)
        {
            Keys[] AllowedKey = { Keys.Delete, Keys.Back, Keys.Clear };
            TextBox tb = (TextBox)sender;
            string value = tb.Text.Trim();

            if (value.Length > 0)
                e.SuppressKeyPress = true;

            if (Array.IndexOf(AllowedKey, e.KeyValue) >= 0 && (e.KeyValue < 47 || e.KeyValue > 55))
                e.SuppressKeyPress = true;

        }

        public SudokuSolver()
        {
            InitializeComponent();
        }

        private void SudokuSolver_Load(object sender, EventArgs e)
        {
            SetupBlocks();
        }
    }
}