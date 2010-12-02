using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ExtensionMethods;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //try solving, run only once
        private void SolveSingleRun()
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
                        else
                        {
                            Numbers[i, j].PossibleValues = PossibleValues.ToList();
                            Numbers[i, j].State = CellState.Empty;
                        }
                    }
                }
            }
        }

        //solve a cell by looking at its row, column and block
        private List<int> SolveByRowColumnAndBlock(int row, int column)
        {
            List<int> PossibleValues = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            //check block
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (i != row || j != column)
                    {
                        if (Numbers[row, column].Block == Numbers[i, j].Block)
                            PossibleValues.Remove(Numbers[i, j].Value);
                    }
                }
            }

            //check row
            for (int i = 0; i < BLOCK_COLUMNS * CELL_COLUMNS; i++)
            {
                PossibleValues.Remove(Numbers[row, i].Value);
            }

            //check column
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                PossibleValues.Remove(Numbers[i, column].Value);
            }

            return PossibleValues;
        }

        private void DisplayAllPossibleValues()
        {
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].PossibleValues != null)
                    {
                        if (Numbers[i, j].State == CellState.Set && Numbers[i, j].PossibleValues.Count > 0)
                        {
                            DisplayCellPossibleValues(i, j, Numbers[i, j].PossibleValues);
                        }
                    }
                }
            }
        }

        private void DisplayCellPossibleValues(int row, int column, List<int> PossibleValues)
        {

        }

        //check if a value is legal for a cell
        Boolean IsLegalValue(string name, int number)
        {
            Point c = GetCellCoordinateByName(name);
            if (!c.IsEmpty)
                return IsLegalValue(c.X, c.Y, number);
            return false;
        }
        Boolean IsLegalValue(int row, int column, int number)
        {
            List<Point> Conflicts = GetConflictingValueCoordinates(row, column, number);

            if (Conflicts.Count > 0)
            {
                ClearCellStyle(CellStyleState.Conflicted);
                foreach (Point c in Conflicts)
                {
                    SetCellStyle(Numbers[c.X, c.Y].Cell, CellStyleState.Conflicted);
                    Numbers[c.X, c.Y].StyleState = CellStyleState.Conflicted;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        //get a list of coordinates of conflicting values
        private List<Point> GetConflictingValueCoordinates(int row, int column, int number)
        {
            List<Point> Conflicts = new List<Point>();

            //check block
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (i != row || j != column)
                    {
                        if (Numbers[row, column].Block == Numbers[i, j].Block)
                            if (number == Numbers[i, j].Value)
                                if(Numbers[i, j].Value > 0)
                                    Conflicts.Add(new Point(i, j)); 
                    }
                }
            }

            //check row
            for (int i = 0; i < BLOCK_COLUMNS * CELL_COLUMNS; i++)
            {
                if (number == Numbers[row, i].Value)
                    if (Numbers[row, i].Value > 0)
                        Conflicts.Add(new Point(row, i));
            }

            //check column
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                if (number == Numbers[i, column].Value)
                    if (Numbers[i, column].Value > 0)
                        Conflicts.Add(new Point(i, column));
            }

            return Conflicts;
        }
    }
}
