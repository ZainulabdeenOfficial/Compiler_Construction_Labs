using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            // Populate task types
            cmbTaskType.Items.AddRange(new string[] { "Rescue", "Supply Drop", "Medical", "Recon" });
            cmbTaskType.SelectedIndex = 0;

            // Example placeholder command
            txtCommand.Text = "DEPLOY DRONE1 TO ZONE A1 ALT200 SPEED50 ACTION DROP_SUPPLIES";

            // Make ListView columns autosize when form resizes
            this.lvTokens.Resize += (s, e) =>
            {
                if (lvTokens.Columns.Count >= 2)
                {
                    lvTokens.Columns[0].Width = lvTokens.Width / 2 - 10;
                    lvTokens.Columns[1].Width = lvTokens.Width / 2 - 10;
                }
            };

            // Initial priority label
            lblPriorityValue.Text = $"Priority:{trackPriority.Value}";
        }

        private void trackPriority_ValueChanged(object? sender, EventArgs e)
        {
            lblPriorityValue.Text = $"Priority:{trackPriority.Value}";
        }

        private enum TokenType
        {
            Identifier,
            Keyword,
            Number,
            Zone,
            Action,
            Comma,
            Unknown
        }

        private record Token(string Text, TokenType Type);

        private void btnAnalyze_Click(object? sender, EventArgs e)
        {
            lstErrors.Items.Clear();
            lvTokens.Items.Clear();
            txtReport.Clear();

            var input = txtCommand.Text ?? string.Empty;
            var tokens = Tokenize(input);

            foreach (var t in tokens)
            {
                var lvi = new ListViewItem(new string[] { t.Text, t.Type.ToString() });
                lvTokens.Items.Add(lvi);
            }

            var report = Classify(tokens);
            txtReport.Text = report;
        }

        private List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();

            // Simple patterns
            var patterns = new Dictionary<TokenType, string>
            {
                { TokenType.Keyword, "\\b(DEPLOY|RETURN|HOLD|MOVE|TAKEOFF|LAND)\\b" },
                { TokenType.Action, "\\b(DROP_SUPPLIES|DROP_MEDKIT|SCAN|RESCUE)\\b" },
                { TokenType.Number, "\\b\\d+(?:\\.\\d+)?\\b" },
                { TokenType.Zone, "\\bZONA?\\b|\\bZONE\\b|\\b[A-Z]\\d+\\b" },
                { TokenType.Identifier, "\\b[A-Z0-9_]+\\b" },
            };

            // Tokenize by whitespace and punctuation
            var rawTokens = Regex.Split(input, "(\\s+|[,;])").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            foreach (var raw in rawTokens)
            {
                var r = raw.Trim();
                TokenType type = TokenType.Unknown;

                foreach (var kv in patterns)
                {
                    if (Regex.IsMatch(r, "^" + kv.Value + "$", RegexOptions.IgnoreCase))
                    {
                        type = kv.Key;
                        break;
                    }
                }

                // punctuation
                if (r == ",") type = TokenType.Comma;

                tokens.Add(new Token(r, type));
            }

            return tokens;
        }

        private string Classify(List<Token> tokens)
        {
            var errors = new List<string>();
            var sb = new System.Text.StringBuilder();

            // basic checks
            if (tokens.Count == 0)
            {
                errors.Add("No command provided.");
            }

            // check for at least one keyword and one action/number
            if (!tokens.Any(t => t.Type == TokenType.Keyword)) errors.Add("Missing operation keyword (DEPLOY/RETURN/MOVE/etc).");
            if (!tokens.Any(t => t.Type == TokenType.Action)) errors.Add("Missing action (DROP_SUPPLIES/DROP_MEDKIT/SCAN/RESCUE/etc).");
            if (!tokens.Any(t => t.Type == TokenType.Zone)) errors.Add("Operational zone not specified.");

            // Example specific rule: if ACTION is DROP_SUPPLIES require NUMBER for amount
            var hasDrop = tokens.Any(t => t.Type == TokenType.Action && Regex.IsMatch(t.Text, "DROP_", RegexOptions.IgnoreCase));
            if (hasDrop && !tokens.Any(t => t.Type == TokenType.Number))
            {
                errors.Add("Drop action requires a numeric amount or altitude.");
            }

            // Fill report
            sb.AppendLine($"Task Type (UI): {cmbTaskType.SelectedItem}");
            sb.AppendLine($"Priority (UI): {trackPriority.Value}");
            sb.AppendLine($"Operational Zone (UI): {txtZone.Text}");
            sb.AppendLine();

            sb.AppendLine("Tokens:");
            foreach (var t in tokens)
            {
                sb.AppendLine($" {t.Text} -> {t.Type}");
            }

            sb.AppendLine();
            sb.AppendLine("Analysis:");
            if (errors.Count == 0)
            {
                sb.AppendLine(" No errors. Command appears syntactically valid.");
            }
            else
            {
                foreach (var err in errors)
                {
                    sb.AppendLine(" ERROR: " + err);
                    lstErrors.Items.Add(err);
                }
            }

            return sb.ToString();
        }
    }
}
