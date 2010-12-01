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
        protected enum CellState { Empty, Set, Solved, Guessed };
        protected enum CellStyleState { Normal, Conflicted, Solved };
        protected struct Number
        {
            public int Block;                   //which block this cell belongs to
            public TextBox Cell;                //textbox control for this cell
            public CellState State;             //state of this cell
            public CellStyleState StyleState;   //determines the background color mostly
            public int Value;                   //the integer value of this cell
            public List<int> PossibleValues;    //a list of possible values;
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

        protected Number[,] Numbers = new Number[BLOCK_ROWS * CELL_ROWS, BLOCK_COLUMNS * CELL_COLUMNS];
        protected Boolean NumberSet = false;

        public SudokuSolver()
        {
            InitializeComponent();
        }

        private void SudokuSolver_Load(object sender, EventArgs e)
        {
            SetupBlocks();
            SetTestNumber();
        }

        private void SetNumbers_Click(object sender, EventArgs e)
        {
            if (!NumberSet)
            {
                SetOriginalNumber(true);
                NumberSet = true;
                SetNumbers.Text = "Unset Numbers";
            }
            else
            {
                SetOriginalNumber(false);
                NumberSet = false;
                SetNumbers.Text = "Set Numbers";
            }

        }

        private void SingleSolve_Click(object sender, EventArgs e)
        {
            if (NumberSet)
            {
                for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
                {
                    for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                    {
                        if (Numbers[i, j].State == CellState.Empty && Numbers[i, j].Value == 0)
                        {
                            List<int> PossibleValues = SolveByRowColumnAndBlock(i, j);
                            if (PossibleValues.Count == 1)
                            {
                                SetCellValue(i, j, PossibleValues[0], CellState.Solved);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Set Numbers First, Dumbass.");
            }
        }

        
    }
}