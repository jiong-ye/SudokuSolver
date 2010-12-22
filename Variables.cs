using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        protected enum CellState { Empty, Set, Solved, ShowedPossibles, Guessed };
        protected enum CellStyleState { Normal, Checked, Conflicted, Solved, ShowedPossibles, Guessed, Guessing };

        protected struct Number
        {
            public int Block;                                                   //which block this cell belongs to
            public TextBox Cell;                                                //textbox control for this cell
            public CellState State;                                             //state of this cell
            public CellStyleState StyleState;                                   //determines the background color mostly
            public int Value;                                                   //the integer value of this cell
            public List<int> PossibleValues;                                    //a list of possible values;
        }

        protected struct Guess
        {
            public Pointx Coord;
            public List<int> GuessValues;
            public Boolean Correct;

            public Guess(int _x, int _y, int _value)
            {
                Coord = new Pointx(_x, _y);
                GuessValues = new List<int>();
                GuessValues.Clear();
                if (_value > 0)
                    GuessValues.Add(_value);
                Correct = false;
            }
        }

        public struct Pointx
        {
            public int X;
            public int Y;
            public Boolean Filled;

            public Pointx(int _x, int _y)
            {
                this.X = _x;
                this.Y = _y;
                this.Filled = true;
            }
        }

        private const int CELLS = 9;                                            //number of cells in a block
        private const int CELL_ROWS = 3;                                        //number of rows of cells in a block
        private const int CELL_COLUMNS = 3;                                     //number of columns of cells in a block
        private const int CELL_WIDTH = 60;                                      //cell width, change this to change form width
        private const int CELL_HEIGHT = 60;                                     //cell height, change this to change form height
        private const float CELL_FONT_SIZE = 33.0F;                             //cell font size
        private const float CELL_FONT_SIZE_SMALL = 15.0F;
        private const string CELL_FONT_FAMILY = "Calibri";
        private const float POSSIBLE_VALUE_FONT_SIZE = 15.0f;

        private const int BLOCKS = 9;                                           //number of blocks
        private const int BLOCK_ROWS = 3;                                       //number of rows of blocks
        private const int BLOCK_COLUMNS = 3;                                    //number of columns of blocks
        private const int BLOCK_WIDTH = CELL_WIDTH * BLOCK_COLUMNS;             //width of block
        private const int BLOCK_HEIGHT = CELL_HEIGHT * BLOCK_ROWS;              //height of block

        private const int FORM_WIDTH = BLOCK_WIDTH * BLOCK_COLUMNS;
        private const int FORM_HEIGHT = BLOCK_HEIGHT * BLOCK_ROWS;

        protected int CellUnsolved = 81;
        protected int FailedAttemp = 0;
        protected Panel[] Blocks = new Panel[9];

        //this array holds number structures that is used to do operations
        protected Number[,] Numbers = new Number[BLOCK_ROWS * CELL_ROWS, BLOCK_COLUMNS * CELL_COLUMNS];
        protected Boolean NumberSet = false;

        //a list of guesses
        protected List<Guess> Guesses = new List<Guess>();

        protected int[, ,] TestNumbers = { 
                                            {
                                                {0,0,0,7,0,0,9,0,5}, 
                                                {0,0,0,0,0,0,3,4,0}, 
                                                {0,0,5,0,0,3,0,1,8}, 
                                                {2,0,0,0,8,0,0,0,0}, 
                                                {5,9,4,0,0,2,1,0,0}, 
                                                {0,0,1,0,0,0,4,0,0}, 
                                                {8,0,0,0,0,0,0,7,4}, 
                                                {0,5,0,0,0,4,0,0,0}, 
                                                {9,0,0,0,2,0,5,0,0}  
                                            },
                                            {
                                                {0,0,0,3,0,0,0,0,0},
                                                {2,0,0,5,0,0,0,1,7},
                                                {0,0,0,0,1,0,0,0,6},
                                                {0,4,7,0,0,0,0,0,0},
                                                {0,3,0,4,0,2,0,8,0},
                                                {8,0,6,7,3,0,0,0,0},
                                                {0,0,1,0,0,0,5,0,0},
                                                {0,8,0,0,4,1,0,0,0},
                                                {0,0,0,8,0,0,0,0,3}
                                            }
                                       };
    }
}
