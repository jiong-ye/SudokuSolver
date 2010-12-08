namespace Sudoku_Solver
{
    partial class SudokuSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuSolver));
            this.SetNumbers = new System.Windows.Forms.Button();
            this.SingleSolve = new System.Windows.Forms.Button();
            this.ShowPossibleValues = new System.Windows.Forms.Button();
            this.SolveMultiRun = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.StartGuess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SetNumbers
            // 
            this.SetNumbers.Location = new System.Drawing.Point(552, 12);
            this.SetNumbers.Name = "SetNumbers";
            this.SetNumbers.Size = new System.Drawing.Size(150, 23);
            this.SetNumbers.TabIndex = 1000;
            this.SetNumbers.TabStop = false;
            this.SetNumbers.Text = "Set Numbers";
            this.SetNumbers.UseVisualStyleBackColor = true;
            this.SetNumbers.Click += new System.EventHandler(this.SetNumbers_Click);
            // 
            // SingleSolve
            // 
            this.SingleSolve.Location = new System.Drawing.Point(552, 42);
            this.SingleSolve.Name = "SingleSolve";
            this.SingleSolve.Size = new System.Drawing.Size(150, 23);
            this.SingleSolve.TabIndex = 1001;
            this.SingleSolve.TabStop = false;
            this.SingleSolve.Text = "Solve - Single Run";
            this.SingleSolve.UseVisualStyleBackColor = true;
            this.SingleSolve.Click += new System.EventHandler(this.SingleSolve_Click);
            // 
            // ShowPossibleValues
            // 
            this.ShowPossibleValues.Location = new System.Drawing.Point(552, 101);
            this.ShowPossibleValues.Name = "ShowPossibleValues";
            this.ShowPossibleValues.Size = new System.Drawing.Size(150, 23);
            this.ShowPossibleValues.TabIndex = 1002;
            this.ShowPossibleValues.TabStop = false;
            this.ShowPossibleValues.Text = "Show Possible Values";
            this.ShowPossibleValues.UseVisualStyleBackColor = true;
            this.ShowPossibleValues.Click += new System.EventHandler(this.ShowPossibleValues_Click);
            // 
            // SolveMultiRun
            // 
            this.SolveMultiRun.Location = new System.Drawing.Point(552, 72);
            this.SolveMultiRun.Name = "SolveMultiRun";
            this.SolveMultiRun.Size = new System.Drawing.Size(150, 23);
            this.SolveMultiRun.TabIndex = 1003;
            this.SolveMultiRun.TabStop = false;
            this.SolveMultiRun.Text = "Solve - Till Finish or Stuck";
            this.SolveMultiRun.UseVisualStyleBackColor = true;
            this.SolveMultiRun.Click += new System.EventHandler(this.SolveMultiRun_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBox.Location = new System.Drawing.Point(552, 160);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusBox.Size = new System.Drawing.Size(150, 145);
            this.StatusBox.TabIndex = 1004;
            this.StatusBox.TabStop = false;
            // 
            // StartGuess
            // 
            this.StartGuess.Location = new System.Drawing.Point(552, 131);
            this.StartGuess.Name = "StartGuess";
            this.StartGuess.Size = new System.Drawing.Size(150, 23);
            this.StartGuess.TabIndex = 1005;
            this.StartGuess.TabStop = false;
            this.StartGuess.Text = "Start Guessing";
            this.StartGuess.UseVisualStyleBackColor = true;
            this.StartGuess.Click += new System.EventHandler(this.StartGuess_Click);
            // 
            // SudokuSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 547);
            this.Controls.Add(this.StartGuess);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.SolveMultiRun);
            this.Controls.Add(this.ShowPossibleValues);
            this.Controls.Add(this.SingleSolve);
            this.Controls.Add(this.SetNumbers);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 280);
            this.Name = "SudokuSolver";
            this.Text = "Sudoku Solver";
            this.Load += new System.EventHandler(this.SudokuSolver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetNumbers;
        private System.Windows.Forms.Button SingleSolve;
        private System.Windows.Forms.Button ShowPossibleValues;
        private System.Windows.Forms.Button SolveMultiRun;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.Button StartGuess;
    }
}

