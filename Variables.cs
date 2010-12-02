using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        protected enum CellState { Empty, Set, Solved, Guessed };
        protected enum CellStyleState { Normal, Conflicted, Solved };
        protected struct Number
        {
            public int Block;                                                   //which block this cell belongs to
            public TextBox Cell;                                                //textbox control for this cell
            public CellState State;                                             //state of this cell
            public CellStyleState StyleState;                                   //determines the background color mostly
            public int Value;                                                   //the integer value of this cell
            public List<int> PossibleValues;                                    //a list of possible values;
            public TextBox PossibleValuesBox;
        }

        private const int CELLS = 9;                                            //number of cells in a block
        private const int CELL_ROWS = 3;                                        //number of rows of cells in a block
        private const int CELL_COLUMNS = 3;                                     //number of columns of cells in a block
        private const int CELL_WIDTH = 60;                                      //cell width, change this to change form width
        private const int CELL_HEIGHT = 60;                                     //cell height, change this to change form height
        private const float CELL_FONT_FIZE = 27.0F;                             //cell font size
        private const float POSSIBLE_VALUE_FONT_SIZE = 15.0f;

        private const int BLOCKS = 9;                                           //number of blocks
        private const int BLOCK_ROWS = 3;                                       //number of rows of blocks
        private const int BLOCK_COLUMNS = 3;                                    //number of columns of blocks
        private const int BLOCK_WIDTH = CELL_WIDTH * BLOCK_COLUMNS;             //width of block
        private const int BLOCK_HEIGHT = CELL_HEIGHT * BLOCK_ROWS;              //height of block

        private const int FORM_WIDTH = BLOCK_WIDTH * BLOCK_COLUMNS;
        private const int FORM_HEIGHT = BLOCK_HEIGHT * BLOCK_ROWS;

        protected Panel[] Blocks = new Panel[9];

        //this array holds number structures that is used to do operations
        protected Number[,] Numbers = new Number[BLOCK_ROWS * CELL_ROWS, BLOCK_COLUMNS * CELL_COLUMNS];
        protected Boolean NumberSet = false;

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
    }
}
