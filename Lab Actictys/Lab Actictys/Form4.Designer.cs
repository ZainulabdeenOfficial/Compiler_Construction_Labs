using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sessional1
{
    public partial class Form1 : Form
    {
        private RichTextBox richTextBox1;
        private Button button1;
        private DataGridView dataGridView1;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.richTextBox1 = new RichTextBox();
            this.button1 = new Button();
            this.dataGridView1 = new DataGridView();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(300, 120);
            this.richTextBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new Point(330, 50);
            this.button1.Name = "button1";
            this.button1.Size = new Size(120, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Filter Keywords";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Location = new Point(12, 150);
            this.dataGridView1.Size = new Size(440, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ColumnCount = 1;
            this.dataGridView1.Columns[0].Name = "Valid Keywords";
            this.dataGridView1.TabIndex = 2;
            // 
            // Form1
            // 
            this.ClientSize = new Size(470, 320);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Keyword Filter";
            this.ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // take input from richTextBox
            string input = richTextBox1.Text;

            // split input into words by whitespace
            string[] words = Regex.Split(input, "\\s+");

            // regex for keywords
            Regex regex1 = new Regex(@"^(int|float|double|char)$");

            // clear DataGridView
            dataGridView1.Rows.Clear();

            foreach (string word in words)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;

                Match match1 = regex1.Match(word);

                if (match1.Success)
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
