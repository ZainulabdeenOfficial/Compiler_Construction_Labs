using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab9cc
{
    public partial class Form1 : Form
    {
        // Designer fields
        private TextBox Input;
        private TextBox Output;
        private Button Compile;

        // Initialize UI controls
        private void InitializeComponent()
        {
            this.Input = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TextBox();
            this.Compile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Form scaling and minimum size
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(320, 160);
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(12, 12);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(360, 23);
            this.Input.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Input.TabIndex = 0;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(12, 41);
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.Size = new System.Drawing.Size(360, 23);
            this.Output.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Output.TabIndex = 1;
            // 
            // Compile
            // 
            this.Compile.AutoSize = true;
            this.Compile.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Compile.Padding = new Padding(8, 4, 8, 4);
            this.Compile.Location = new System.Drawing.Point(297, 70);
            this.Compile.Name = "Compile";
            this.Compile.Size = new System.Drawing.Size(75, 27);
            this.Compile.TabIndex = 2;
            this.Compile.Text = "Compile";
            this.Compile.UseVisualStyleBackColor = true;
            this.Compile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // Hookup of event kept in code-behind (Form1.cs)
            this.Compile.Click += new System.EventHandler(this.Compile_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.Compile);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}