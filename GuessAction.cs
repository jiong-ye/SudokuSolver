using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //backtracing
        private void StartGuessing()
        {
            List<Guess> BestGuesses = new List<Guess>();
            int GuessNumberCount = 2;

            //get cells that i can guess reasonably accurate,
            //starting with cells with 2 possible values
            while (GuessNumberCount < 10)
            {
                foreach (Guess g in GetBestGuesses(GuessNumberCount))
                    BestGuesses.Add(g);
                GuessNumberCount++;
            }

            int GuessLeft = BestGuesses.Count;
            int GuessIndex = 0;
            
            while (GuessLeft > 0)
            {
                //get next guess
                Guess g = BestGuesses[GuessIndex];
                List<int> PossibleValues = Numbers[g.Coord.X, g.Coord.Y].PossibleValues.ToList();
                
                //remove guessed value
                foreach (int val in g.GuessValues)
                    PossibleValues.Remove(val);

                AppendStatus("Guessing Cell[" + g.Coord.X.ToString() + "," + g.Coord.Y.ToString() + "] with " + PossibleValues.Count.ToString() + " Possible Values");

                if (PossibleValues.Count > 0)
                {
                    Random r = new Random();
                    int guess = PossibleValues[r.Next(0, PossibleValues.Count)];
                    Boolean isValidGuess = IsLegalValue(g.Coord.X, g.Coord.Y, guess);
                    g.GuessValues.Add(guess);

                    //guess until we have a non-conflicting guess or we run out of guesses
                    while (PossibleValues.Count > 0 && !isValidGuess)
                    {
                        PossibleValues.Remove(guess);

                        if (PossibleValues.Count > 0)
                        {
                            //get new guess
                            guess = PossibleValues[r.Next(0, PossibleValues.Count)];
                            isValidGuess = IsLegalValue(g.Coord.X, g.Coord.Y, guess);
                        }
                        else
                        {
                            if (GuessIndex > 0)
                            {
                                GuessIndex--;
                                GuessLeft++;
                            }
                            g.GuessValues.Clear();
                            AppendStatus("No More Valid Guesses. Failed");
                            break;
                        }
                    }

                    //if this guess is valid, we assume it's solved
                    if (isValidGuess)
                    {
                        SetCellValue(g.Coord.X, g.Coord.Y, guess, CellState.Guessed);
                        SetCellStyle(Numbers[g.Coord.X, g.Coord.Y].Cell, CellStyleState.Guessed);
                        AppendStatus("Guessed Cell[" + g.Coord.X.ToString() + "," + g.Coord.Y.ToString() + "] to be " + guess.ToString());
                        GuessIndex++;
                        GuessLeft--;
                    }
                }
                else
                {
                    if (GuessIndex > 0)
                    {
                        GuessIndex--;
                        GuessLeft++;
                    }
                    else
                    {
                        AppendStatus("No Valid Guesses. Failed");
                        return;
                    }
                }
            }
        }

        private List<Guess> GetBestGuesses(int GuessNumberCount)
        {
            List<Guess> BestGuesses = new List<Guess>();
            for (int i = 0; i < BLOCK_ROWS * CELL_ROWS; i++)
            {
                for (int j = 0; j < BLOCK_COLUMNS * CELL_COLUMNS; j++)
                {
                    if (Numbers[i, j].State == CellState.ShowedPossibles)
                    {
                        if (Numbers[i, j].PossibleValues.Count == GuessNumberCount)
                        {
                            BestGuesses.Add(new Guess(i, j, 0));
                        }
                    }
                }
            }

            return BestGuesses;
        }
    }
}
