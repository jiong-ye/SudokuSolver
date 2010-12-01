﻿namespace Sudoku_Solver
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
            this.SetNumbers = new System.Windows.Forms.Button();
            this.SingleSolve = new System.Windows.Forms.Button();
            this.ShowPossibleValues = new System.Windows.Forms.Button();
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
            this.ShowPossibleValues.Location = new System.Drawing.Point(552, 72);
            this.ShowPossibleValues.Name = "ShowPossibleValues";
            this.ShowPossibleValues.Size = new System.Drawing.Size(150, 23);
            this.ShowPossibleValues.TabIndex = 1002;
            this.ShowPossibleValues.Text = "Show Possible Values";
            this.ShowPossibleValues.UseVisualStyleBackColor = true;
            // 
            // SudokuSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 547);
            this.Controls.Add(this.ShowPossibleValues);
            this.Controls.Add(this.SingleSolve);
            this.Controls.Add(this.SetNumbers);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 280);
            this.Name = "SudokuSolver";
            this.Text = "Sudoku Solver";
            this.Load += new System.EventHandler(this.SudokuSolver_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetNumbers;
        private System.Windows.Forms.Button SingleSolve;
        private System.Windows.Forms.Button ShowPossibleValues;
    }
}

