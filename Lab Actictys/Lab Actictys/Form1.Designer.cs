using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Lab_Actictys
{
    public partial class Form1 : Form
    {
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Button button1;
        private IContainer components = null;

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
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(360, 120);
            richTextBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(12, 140);
            button1.Name = "button1";
            button1.Size = new Size(120, 30);
            button1.TabIndex = 1;
            button1.Text = "Filter Operators";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(12, 180);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(360, 120);
            richTextBox2.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 311);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(richTextBox2);
            Name = "Form1";
            Text = "Operators Filter";
            ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get input from richTextBox1
            string input = richTextBox1.Text;

            // Split the input on any whitespace (spaces, tabs, newlines)
            string[] words = Regex.Split(input, "\\s+", RegexOptions.Multiline | RegexOptions.CultureInvariant);

            // Regular Expression for arithmetic operators (+, -, *, /)
            Regex regex1 = new Regex(@"^[+\-*/]$", RegexOptions.CultureInvariant);

            // Clear previous results
            richTextBox2.Text = string.Empty;

            bool hasInvalid = false;

            foreach (var token in words)
            {
                if (string.IsNullOrWhiteSpace(token)) continue;

                Match match1 = regex1.Match(token);

                if (match1.Success)
                {
                    richTextBox2.AppendText(token + " ");
                }
                else
                {
                    hasInvalid = true;
                }
            }

            if (hasInvalid)
            {
                MessageBox.Show("Some inputs are not valid operators.");
            }
        }
    }
}
