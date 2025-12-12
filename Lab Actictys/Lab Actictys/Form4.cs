using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab_Actictys
{
    public partial class Form4 : Form
    {
        private IContainer? components = null;
        private RichTextBox richTextBox1 = null!;
        private Button button1 = null!;
        private DataGridView dataGridView1 = null!;

        public Form4()
        {
            InitializeComponent();
        }

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
            button1 = new Button();
            dataGridView1 = new DataGridView();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(300, 120);
            richTextBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(330, 50);
            button1.Name = "button1";
            button1.Size = new Size(120, 30);
            button1.TabIndex = 1;
            button1.Text = "Filter Keywords";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Location = new Point(12, 150);
            dataGridView1.Size = new Size(440, 150);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "Valid Keywords";
            dataGridView1.TabIndex = 2;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 320);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Form4";
            Text = "Keyword Filter";
            ResumeLayout(false);
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            // take input from richTextBox
            string input = richTextBox1.Text;

            // split input into words by whitespace
            string[] words = Regex.Split(input, @"\s+", RegexOptions.Multiline | RegexOptions.CultureInvariant);

            // regex for keywords
            Regex regex1 = new Regex(@"^(int|float|double|char)$", RegexOptions.CultureInvariant);

            // clear DataGridView
            dataGridView1.Rows.Clear();

            foreach (string word in words)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;

                if (regex1.IsMatch(word))
                {
                    // valid keyword → add to DataGridView
                    dataGridView1.Rows.Add(word);
                }
                else
                {
                    // show message for invalid input
                    MessageBox.Show("Invalid keyword: " + word);
                }
            }
        }
    }
}
