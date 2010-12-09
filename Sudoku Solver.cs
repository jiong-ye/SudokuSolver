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
        public SudokuSolver()
        {
            InitializeComponent();

            //set button widths
            SetNumbers.Width = SingleSolve.Width = ShowPossibleValues.Width = StatusBox.Width = SolveMultiRun.Width = StartGuess.Width = 250;

            //set form width and height based on controls
            this.Width = FORM_WIDTH + SetNumbers.Width + 25;
            this.Height = FORM_HEIGHT + 40;

            //set button left
            SetNumbers.Left = this.Width - SetNumbers.Width - 13;
            SingleSolve.Left = this.Width - SingleSolve.Width - 13;
            SolveMultiRun.Left = this.Width - SolveMultiRun.Width - 13;
            ShowPossibleValues.Left = this.Width - ShowPossibleValues.Width - 13;
            StartGuess.Left = this.Width - StartGuess.Width - 13;
            StatusBox.Left = this.Width - StatusBox.Width - 13;            
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
                AppendStatus("Numbers Locked");
            }
            else
            {
                SetOriginalNumber(false);
                NumberSet = false;
                SetNumbers.Text = "Set Numbers";
                AppendStatus("Numbers Unocked");
            }

        }

        private void SingleSolve_Click(object sender, EventArgs e)
        {
            if (NumberSet)
            {
                //ClearCellStyle(CellStyleState.Checked);
                SolveSingleRun();
            }
            else
            {
                AppendStatus("Set Numbers First, Dumbass.");
            }
        }

        private void SolveMultiRun_Click(object sender, EventArgs e)
        {
            if (NumberSet)
            {
                SolveMultiRuns();
            }
            else
            {
                AppendStatus("Set Numbers First, Dumbass.");
            }
        }

        private void ShowPossibleValues_Click(object sender, EventArgs e)
        {
            if (NumberSet)
            {
                if(!SolveMultiRuns())
                    DisplayAllPossibleValues();
            }
            else
            {
                AppendStatus("Set Numbers First, Dumbass.");
            }
        }

        private void StartGuess_Click(object sender, EventArgs e)
        {
            if (NumberSet)
            {
                DisplayAllPossibleValues();
                if (!SolveMultiRuns())
                {
                    StartGuessing();
                }
            }
            else
            {
                AppendStatus("Set Numbers First, Dumbass.");
            }
        }
    }
}