using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        private void StartGuessing()
        {
            List<Pointx> BestGuesses = new List<Pointx>();
            int GuessNumberCount = 2;

            //get cells that i can guess reasonably accurate,
            //starting with cells with 2 possible values
            while (BestGuesses.Count == 0 && GuessNumberCount < 10)
            {
                BestGuesses = GetBestGuesses(GuessNumberCount);
                GuessNumberCount++;
            }

            //pick a random cell out of all the possible guesses
            if (BestGuesses.Count > 0)
            {
                Random index = new Random();
                Pointx guess = BestGuesses[index.Next(0,BestGuesses.Count)];

                //make the guess
                List<int> PossibleValues = Numbers[guess.X, guess.Y].PossibleValues;
                if (Guesses.Count > 0)
                {
                    foreach (Guess g in Guesses)
                    {
                        if (g.Coord.X == guess.X && g.Coord.Y == guess.Y)
                        {
                            foreach (int GuessedVaule in g.GuessValues)
                            {
                                PossibleValues.Remove(GuessedVaule);
                            }
                        }
                    }
                }

                if (PossibleValues.Count > 0)
                {
                    SetCellValue(guess.X, guess.Y, PossibleValues[0], CellState.Guessed);
                    SetCellStyle(Numbers[guess.X, guess.Y].Cell, CellStyleState.Guessed);
                    Guesses.Add(new Guess(guess.X,guess.Y,PossibleValues[0]));
                    AppendStatus("Guess Cell[" + guess.X.ToString() + "," + guess.Y.ToString() + "] to be " + PossibleValues[0].ToString());
                }
            }
        }

        private List<Pointx> GetBestGuesses(int GuessNumberCount)
        {
            List<Pointx> BestGuesses = new List<Pointx>();
            for(int i = 0; i < BLOCK_ROWS * CELL_ROWS;i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].State == CellState.ShowedPossibles)
                    {
                        if (Numbers[i, j].PossibleValues.Count == GuessNumberCount)
                        {
                            BestGuesses.Add(new Pointx(i, j));
                        }
                    }
                }
            }

            return BestGuesses;
        }
    }
}
