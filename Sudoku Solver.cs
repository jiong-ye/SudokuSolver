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
        public const int CELLS = 9;
        public const int CELL_WIDTH = 30;
        public const int CELL_HEIGHT = 30;

        public const int BLOCKS = 9;
        public const int BLOCK_ROWS = 3;
        public const int BLOCK_COLUMNS = 3;
        public const int BLOCK_WIDTH = CELL_WIDTH * BLOCK_COLUMNS;
        public const int BLOCK_HEIGHT = CELL_HEIGHT * BLOCK_ROWS;

        public SudokuSolver()
        {
            InitializeComponent();

            SetupBlocks();
        }

        public void SetupBlocks()
        {
            for (int i = 0; i < BLOCKS; i++)
            {
                Panel block = GetNewBlock(i);
                this.Controls.Add(block);
            }
        }

        public Panel GetNewBlock(int i)
        {
            Panel block = new Panel();

            //set block styles
            block.BorderStyle = BorderStyle.FixedSingle;
            block.Width = BLOCK_WIDTH;
            block.Height = BLOCK_HEIGHT;
            block.Top = BLOCK_HEIGHT * (i / 3) + 5;
            block.Left = BLOCK_WIDTH * (i % 3) + 4;

            return block;
        }
    }
}
