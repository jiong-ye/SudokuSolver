using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        //backtracing
        private void StartGuessing()
        {
            List<Guess> BestGuesses = GetBestGuesses();
            UpdateStatusProgressMaximum(BestGuesses.Count);

            UInt64 PossibleGuesses = GetPossibleGuess();
            int GuessLeft = BestGuesses.Count;
            int GuessIndex = 0;
            int LastIndex = -1;
            int GuessMade = 0;

            while (GuessLeft > 0)
            {
                //update progress
                UpdateStatusProgress(GuessLeft, ++GuessMade, PossibleGuesses);
                
                //get next guess
                Guess g = BestGuesses[GuessIndex];
                List<int> PossibleValues = SolveByRowColumnAndBlock(g.Coord.X, g.Coord.Y);

                //set cell to pre guess state
                SetCellValue(g.Coord.X, g.Coord.Y, 0, CellState.Empty);
                SetCellStyle(g.Coord.X, g.Coord.Y, CellStyleState.Guessing);
                //SetGuessBoxItemState(GuessIndex, true);

                //remove guessed value 
                foreach (int val in g.GuessValues)
                    PossibleValues.Remove(val);

                if (PossibleValues.Count > 0)
                {
                    Random r = new Random();
                    //int guess = PossibleValues[r.Next(0, PossibleValues.Count)];
                    int guess = PossibleValues[0];
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
                        //SetGuessBoxItemValue(GuessIndex, guess);
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

        private UInt64 GetPossibleGuess()
        {
            UInt64 count = 1;
            
            foreach (Number n in Numbers)
            {
                if (n.State == CellState.ShowedPossibles && n.PossibleValues.Count > 0)
                    count =  count * Convert.ToUInt64(n.PossibleValues.Count);
            }

            return count;
        }
               
        private void UpdateStatusProgressMaximum(int max)
        {
            if (!StatusStrip.InvokeRequired)
            {
                StatusProgress.Maximum = max;
            }
            else
            {
                UpdateStatusProgressMaximumDelegate del = new UpdateStatusProgressMaximumDelegate(UpdateStatusProgressMaximum);
                StatusStrip.Invoke(del, new object[] { max });
            }
        }
        delegate void UpdateStatusProgressMaximumDelegate(int max);

        private void UpdateStatusProgress(int GuessLeft, int GuessMade, UInt64 PossibleGuesses)
        {
            if (!StatusStrip.InvokeRequired)
            {
                StatusProgress.Value = StatusProgress.Maximum - GuessLeft;
                StatusLabel.Text = GuessMade.ToString("###,###") + " of " + PossibleGuesses.ToString("###,###") + " Guesses Made";
            }
            else
            {
                UpdateStatusProgressDelegate del = new UpdateStatusProgressDelegate(UpdateStatusProgress);
                StatusStrip.Invoke(del, new object[] { GuessLeft, GuessMade, PossibleGuesses });
            }
        }
        delegate void UpdateStatusProgressDelegate(int GuessLeft, int GuessMade,  UInt64 PossibleGuesses);
    }
}
