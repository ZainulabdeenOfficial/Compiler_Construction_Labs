using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_Actictys
{
    public partial class Form3 : Form
    {
        private IContainer? components = null;
        private RichTextBox richTextBox1 = null!; // Input
        private RichTextBox richTextBox2 = null!; // Output
        private Button button1 = null!;
        private Label labelInput = null!;
        private Label labelOutput = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            button1 = new Button();
            labelInput = new Label();
            labelOutput = new Label();
            SuspendLayout();
            // 
            // labelInput
            // 
            labelInput.AutoSize = true;
            labelInput.Location = new Point(12, 9);
            labelInput.Name = "labelInput";
            labelInput.Size = new Size(34, 15);
            labelInput.TabIndex = 0;
            labelInput.Text = "Input";
            // 
            // richTextBox1 (Input)
            // 
            richTextBox1.Location = new Point(12, 27);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(260, 150);
            richTextBox1.TabIndex = 1;
            // 
            // labelOutput
            // 
            labelOutput.AutoSize = true;
            labelOutput.Location = new Point(290, 9);
            labelOutput.Name = "labelOutput";
            labelOutput.Size = new Size(45, 15);
            labelOutput.TabIndex = 2;
            labelOutput.Text = "Output";
            // 
            // richTextBox2 (Output)
            // 
            richTextBox2.Location = new Point(290, 27);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(260, 150);
            richTextBox2.TabIndex = 3;
            richTextBox2.ReadOnly = true;
            // 
            // button1
            // 
            button1.Location = new Point(220, 190);
            button1.Name = "button1";
            button1.Size = new Size(160, 30);
            button1.TabIndex = 4;
            button1.Text = "Filter Numeric Constants";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(564, 236);
            Controls.Add(labelInput);
            Controls.Add(richTextBox1);
            Controls.Add(labelOutput);
            Controls.Add(richTextBox2);
            Controls.Add(button1);
            Name = "Form3";
            Text = "Numeric Constant Filter";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
