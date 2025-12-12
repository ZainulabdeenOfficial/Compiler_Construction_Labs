using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab_Actictys
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            // Take input text
            string input = richTextBox1.Text;

            // Split input on spaces, tabs, or newlines
            string[] words = Regex.Split(input, @"\s+", RegexOptions.Multiline | RegexOptions.CultureInvariant);

            // Regular Expression for valid variables
            // Start with a letter, total length <= 10, then letters or digits
            Regex regex1 = new Regex(@"^[A-Za-z][A-Za-z0-9]{0,9}$", RegexOptions.CultureInvariant);

            // Clear previous output
            richTextBox2.Clear();
            bool hasInvalid = false;

            foreach (string word in words)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;

                if (regex1.IsMatch(word))
                {
                    // Valid variable → append to output
                    richTextBox2.AppendText(word + " ");
                }
                else
                {
                    hasInvalid = true;
                }
            }

            if (hasInvalid)
            {
                MessageBox.Show("Some tokens are not valid variables (start with a letter, max length 10, letters/digits only).");
            }
        }
    }
}
