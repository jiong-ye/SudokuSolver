using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //backtracing
        private void StartGuessing()
        {
            List<Guess> BestGuesses = GetBestGuesses();

            int GuessLeft = BestGuesses.Count;
            int GuessIndex = 0;
            int LastIndex = -1;

            while (GuessLeft > 0)
            {
                //get next guess
                Guess g = BestGuesses[GuessIndex];
                List<int> PossibleValues = SolveByRowColumnAndBlock(g.Coord.X, g.Coord.Y);

                //set cell to pre guess state
                SetCellValue(g.Coord.X, g.Coord.Y, 0, CellState.Empty);
                SetCellStyle(g.Coord.X, g.Coord.Y, CellStyleState.Guessing);
                SetGuessBoxItemState(GuessIndex, true);

                //remove guessed value 
                foreach (int val in g.GuessValues)
                    PossibleValues.Remove(val);

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
                                g.GuessValues.Clear();
                            }

                            AppendStatus("No More Valid Guesses. Failed");
                            break;
                        }
                    }

                    //if this guess is valid, we assume it's solved
                    if (isValidGuess)
                    {
                        SetCellValue(g.Coord.X, g.Coord.Y, guess, CellState.Guessed);
                        SetCellStyle(g.Coord.X, g.Coord.Y, CellStyleState.Guessed);
                        SetGuessBoxItemValue(GuessIndex, guess);
                        LastIndex = GuessIndex;
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
                        g.GuessValues.Clear();
                    }
                    else
                    {
                        AppendStatus("No Valid Guesses. Failed");
                        return;
                    }
                }
            }
        }

        private List<Guess> GetBestGuesses()
        {
            int GuessNumberCount = 2;
            List<Guess> BestGuesses = new List<Guess>();

            //get cells that i can guess reasonably accurate,
            //starting with cells with 2 possible values
            while (GuessNumberCount < 10)
            {
                foreach (Guess g in GetBestGuess(GuessNumberCount))
                    BestGuesses.Add(g);
                GuessNumberCount++;
            }

            return BestGuesses;
        }
        private List<Guess> GetBestGuess(int GuessNumberCount)
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

        private void SetGuessBoxList(List<Guess> Guesses)
        {
            foreach (Guess g in Guesses)
            {
                string Item = "Cell[" + g.Coord.X.ToString() + "," + g.Coord.Y.ToString() + "]";
                Item += " - Guessed: ";
                GuessBox.Items.Add(Item);
            }

            GuessBox.Refresh();
        }

        private void SetGuessBoxItemValue(int index, int guess)
        {
            string item = string.Empty;

            if (!GuessBox.InvokeRequired)
            {
                GuessBox.Items[index] += guess.ToString() + ",";
            }
            else
            {
                SetGuessBoxItemValueDelegate del = new SetGuessBoxItemValueDelegate(SetGuessBoxItemValue);
                GuessBox.Invoke(del, new object[] { index, guess });
            }
        }
        delegate void SetGuessBoxItemValueDelegate(int index, int guess);

        private void SetGuessBoxItemState(int index, Boolean state)
        {
            if (!GuessBox.InvokeRequired)
            {
                GuessBox.SetSelected(index, state);
                GuessBox.Refresh();
            }
            else
            {
                SetGuessBoxItemStateDelegate del = new SetGuessBoxItemStateDelegate(SetGuessBoxItemState);
                GuessBox.Invoke(del, new object[] { index, state });
            }
        }
        delegate void SetGuessBoxItemStateDelegate(int index, Boolean state);
    }
}
