using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //set up 9 blocks to contain 9 blocks of numbers
        public void SetupBlocks()
        {
            for (int i = 0; i < BLOCK_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS; j++)
                {
                    Panel block = GetNewBlock(i, j);
                    Blocks[i * BLOCK_COLUMNS + i] = block;
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

        List<int> GetBlockHorizontalSiblings(int block)
        {
            List<int> siblings = new List<int>();

            if (block >= 0 && block <= 2)
            {
                for (int i = 0; i <= 2; i++)
                    if (i != block)
                        siblings.Add(i);
            }
            else if (block >= 3 && block <= 5)
            {
                for (int i = 3; i <= 5; i++)
                    if (i != block)
                        siblings.Add(i);
            }
            else if (block >= 6 && block <= 8)
            {
                for (int i = 6; i <= 8; i++)
                    if (i != block)
                        siblings.Add(i);
            }
            return siblings;
        }

        List<int> GetBlockVerticalSiblings(int block)
        {
            List<int> siblings = new List<int>();

            if (block >= 0 && block <= 2)
            {
                siblings.Add(block + CELL_ROWS);
                siblings.Add(block + CELL_ROWS * 2);
            }
            else if (block >= 3 && block <= 5)
            {
                siblings.Add(block - CELL_ROWS);
                siblings.Add(block + CELL_ROWS);
            }
            else if (block >= 6 && block <= 8)
            {
                siblings.Add(block - CELL_ROWS);
                siblings.Add(block - CELL_ROWS * 2);
            }
            return siblings;
        }

        Pointx FindCellInBlock(int block, int number)
        {
            Pointx target = new Pointx();
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].Block == block && Numbers[i,j].Value == number)
                    {
                        target.X = i;
                        target.Y = j;
                        target.Filled = true;
                    }
                }
            }
            return target;
        }
    }
}
