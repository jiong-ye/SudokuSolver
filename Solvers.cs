using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ExtensionMethods;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //try solving, run only once
        private Boolean SolveSingleRun()
        {
            Boolean ProduceAnswer = false;

            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    List<int> PossibleValues = new List<int>();
                    List<Pointx> PossibleCells = new List<Pointx>();
                    if (Numbers[i, j].State == CellState.Empty)
                    {
                        PossibleValues = SolveByRowColumnAndBlock(i, j);
                        if (PossibleValues.Count == 1)
                        {
                            SetCellValue(i, j, PossibleValues[0], CellState.Solved);
                            ProduceAnswer = true;
                            AppendStatus("Cell[" + i.ToString() + "," + j.ToString() + "] Solved to be " + PossibleValues[0].ToString());
                        }
                    }
                    else if (Numbers[i, j].State != CellState.Empty)
                    {
                        PossibleCells = SolveBySiblingsElimination(Numbers[i, j].Value, i, j);
                        if (PossibleCells.Count == 1)
                        {
                            SetCellValue(PossibleCells[0].X, PossibleCells[0].Y, Numbers[i, j].Value, CellState.Solved);
                            ProduceAnswer = true;
                            AppendStatus("Cell[" + PossibleCells[0].X.ToString() + "," + PossibleCells[0].Y.ToString() + "] Solved to be " + Numbers[i, j].Value.ToString());
                        }
                    }

                    if (!ProduceAnswer)
                    {
                        Numbers[i, j].PossibleValues = PossibleValues.ToList();
                        //Numbers[i, j].State = CellState.Empty;
                        //AppendStatus("Cell[" + i.ToString() + "," + j.ToString() + "] Checked. Not Solved.");
                    }
                    else
                    {
                        FailedAttemp = 0;
                    }
                }
            }
            return ProduceAnswer;
        }

        //try solving, run till completely solved or stuck
        private Boolean SolveMultiRuns()
        {
            FailedAttemp = 0;

            while (FailedAttemp < 5)
            {
                if (!SolveSingleRun())
                    FailedAttemp++;
            }

            if (FailedAttemp == 5)
                return false;
            return true;
        }

        //solve an empty cell by looking at its row, column and block
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

        //solve a filled cell by looking for the same value in siblings
        private List<Pointx> SolveBySiblingsElimination(int number, int row, int column)
        {
            List<Pointx> PossibleCell = new List<Pointx>();

            //get horizontal sibling blocks
            List<int> HorizontalSiblingBlocks = GetBlockHorizontalSiblings(Numbers[row, column].Block);
            List<int> VerticalSiblingBlocks = GetBlockVerticalSiblings(Numbers[row, column].Block);
            if (HorizontalSiblingBlocks.Count == 2 && VerticalSiblingBlocks.Count == 2)
            {
                //do horizontal first
                //get horizontal sibling cells of the same value
                int ContainingBlock = -1;
                List<Pointx> HorizontalCellSibling = new List<Pointx>();
                foreach(int block in HorizontalSiblingBlocks)
                {
                    Pointx SiblingCell = FindCellInBlock(block, number);
                    if (SiblingCell.Filled)
                    {
                        HorizontalCellSibling.Add(SiblingCell);
                        ContainingBlock = block;
                    }
                }
                if (HorizontalCellSibling.Count == 1)
                {
                    //get the block that dont have that number
                    HorizontalSiblingBlocks.Remove(ContainingBlock);

                    //find the two same value cells in a row of blocks
                    HorizontalCellSibling.Add(new Pointx(row, column));

                    //get the potential cells of the remaining block of the row
                    List<Pointx> PossibleCells = GetPossibleCellsBySiblings(HorizontalCellSibling, HorizontalSiblingBlocks[0]);

                    foreach (Pointx cell in PossibleCells)
                    {
                        if (!ColumnContainsNumber(cell.Y, number))
                        {
                            PossibleCell.Add(new Pointx(cell.X, cell.Y));
                        }
                    }
                }

                //if horizontal find nothing we do vertical
                if (PossibleCell.Count != 1)
                {
                    //do vertical
                    //get vertical sibling cell of the same value
                    ContainingBlock = -1;
                    PossibleCell.Clear();
                    List<Pointx> VerticalCellSibling = new List<Pointx>();
                    foreach (int block in VerticalSiblingBlocks)
                    {
                        Pointx SiblingCell = FindCellInBlock(block, number);
                        if (SiblingCell.Filled)
                        {
                            VerticalCellSibling.Add(SiblingCell);
                            ContainingBlock = block;
                        }
                    }
                    if (VerticalCellSibling.Count == 1)
                    {
                        //get the block that dont have the number
                        VerticalSiblingBlocks.Remove(ContainingBlock);

                        //find the two same value cells in a column of blocks
                        VerticalCellSibling.Add(new Pointx(row, column));

                        //get the potential cells of the remaining block of the row
                        List<Pointx> PossibleCells = GetPossibleCellsBySiblings(VerticalCellSibling, VerticalSiblingBlocks[0]);

                        foreach (Pointx cell in PossibleCells)
                        {
                            if (!RowContainsNumber(cell.Y, number))
                            {
                                PossibleCell.Add(new Pointx(cell.X, cell.Y));
                            }
                        }
                    }
                }
            }

            return PossibleCell;
        }

        private void DisplayAllPossibleValues()
        {
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].PossibleValues != null)
                    {
                        if (Numbers[i, j].State == CellState.Empty && Numbers[i, j].PossibleValues.Count > 0)
                        {
                            DisplayCellPossibleValues(i, j, Numbers[i, j].PossibleValues);
                        }
                    }
                }
            }
        }

        private void DisplayCellPossibleValues(int row, int column, List<int> PossibleValues)
        {
            string values = string.Empty;

            foreach (int number in PossibleValues)
            {
                values += number.ToString() + " ";
            }
            SetCellValue(row, column, values, CellState.ShowedPossibles);
        }

        //check if a value is legal for a cell
        Boolean IsLegalValue(string name, int number)
        {
            Pointx c = GetCellCoordinateByName(name);
            if (c.Filled)
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
                                if (Numbers[i, j].Value > 0)
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
