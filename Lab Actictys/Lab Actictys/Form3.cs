using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab_Actictys
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            // Read input and split on whitespace
            var tokens = Regex.Split(richTextBox1.Text, @"\s+", RegexOptions.Multiline | RegexOptions.CultureInvariant);

            // Regex for numeric constants: integers, decimals, optional exponent part
            // Examples: 0, 12, 3.14, .5, 5., 1e10, 2.3e-5, 7E+2
            var numberRegex = new Regex(@"^(?:\d+(?:\.\d+)?|\.\d+)(?:[eE][+-]?\d+)?$", RegexOptions.CultureInvariant);

            richTextBox2.Clear();
            bool hasInvalid = false;

            foreach (var t in tokens)
            {
                if (string.IsNullOrWhiteSpace(t)) continue;
                if (numberRegex.IsMatch(t))
                {
                    richTextBox2.AppendText(t + " ");
                }
                else
                {
                    hasInvalid = true;
                }
            }

            if (hasInvalid)
            {
                MessageBox.Show("Some tokens are not valid numeric constants.");
            }
        }
    }
}
