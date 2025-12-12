using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab3Actity1
{
    public partial class Form1 : Form
    {
        // Symbol Table structure using 2D array
        private string[,] symbolTable;
        private int currentIndex;
        private const int MAX_ENTRIES = 100;
        private const int COLUMNS = 5; // Index, Name, Type, Value, LineNumber

        // Lists for keywords and other components
        private List<string> keywords;
        private TextBox tfInput;
        private RichTextBox tfTokens;
        private RichTextBox symbolTableDisplay;
        private Button btnProcess;

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Symbol Table Implementation";
            this.Width = 800;
            this.Height = 600;

            // Input TextBox
            tfInput = new TextBox();
            tfInput.Multiline = true;
            tfInput.ScrollBars = ScrollBars.Vertical;
            tfInput.SetBounds(20, 20, 350, 150);

            // Tokens RichTextBox
            tfTokens = new RichTextBox();
            tfTokens.SetBounds(400, 20, 350, 200);

            // Symbol Table RichTextBox
            symbolTableDisplay = new RichTextBox();
            symbolTableDisplay.SetBounds(20, 200, 730, 300);

            // Process Button
            btnProcess = new Button();
            btnProcess.Text = "Process Code";
            btnProcess.SetBounds(20, 170, 120, 30);
            btnProcess.Click += btnProcess_Click;

            // Add controls to form
            this.Controls.Add(tfInput);
            this.Controls.Add(tfTokens);
            this.Controls.Add(symbolTableDisplay);
            this.Controls.Add(btnProcess);
        }

        private void InitializeSymbolTable()
        {
            symbolTable = new string[MAX_ENTRIES, COLUMNS];
            currentIndex = 0;

            // Initialize header row
            symbolTable[0, 0] = "Index";
            symbolTable[0, 1] = "Name";
            symbolTable[0, 2] = "Type";
            symbolTable[0, 3] = "Value";
            symbolTable[0, 4] = "Line";
        }

        private void InitializeKeywords()
        {
            keywords = new List<string>
            {
                "int", "float", "double", "char", "string",
                "if", "else", "while", "for", "return",
                "void", "main", "extern", "public", "private"
            };
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            ProcessInputCode();
        }

        private void ProcessInputCode()
        {
            string userInput = tfInput.Text;
            string[] lines = userInput.Split('\n');

            tfTokens.Clear();
            symbolTableDisplay.Clear();
            ResetSymbolTable();

            int lineNumber = 0;

            foreach (string line in lines)
            {
                lineNumber++;
                ProcessLine(line.Trim(), lineNumber);
            }

            DisplaySymbolTable();
        }

        private void ProcessLine(string line, int lineNumber)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            string[] tokens = TokenizeLine(line);

            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i].Trim();
                if (string.IsNullOrEmpty(token)) continue;

                // Check if token is a keyword
                if (keywords.Contains(token))
                {
                    tfTokens.AppendText($"<keyword, {token}>\n");

                    // Check for variable declaration pattern
                    if (IsDataType(token) && i + 1 < tokens.Length)
                    {
                        ProcessVariableDeclaration(tokens, i, lineNumber, token);
                    }
                }
                // Check if token is an identifier (variable/function name)
                else if (IsIdentifier(token))
                {
                    ProcessIdentifier(token, lineNumber);
                }
                // Check if token is a constant
                else if (IsConstant(token))
                {
                    tfTokens.AppendText($"<constant, {token}>\n");
                }
                // Check if token is an operator
                else if (IsOperator(token))
                {
                    tfTokens.AppendText($"<operator, {token}>\n");
                }
            }
        }

        private string[] TokenizeLine(string line)
        {
            char[] delimiters = { ' ', ';', ',', '(', ')', '{', '}', '[', ']', '=', '+', '-', '*', '/', '<', '>' };
            return line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ProcessVariableDeclaration(string[] tokens, int currentIndex, int lineNumber, string dataType)
        {
            if (currentIndex + 1 >= tokens.Length) return;

            string variableName = tokens[currentIndex + 1];

            if (IsIdentifier(variableName) && !keywords.Contains(variableName))
            {
                string value = "undefined";

                for (int i = currentIndex + 2; i < tokens.Length - 1; i++)
                {
                    if (tokens[i] == "=" && i + 1 < tokens.Length)
                    {
                        value = tokens[i + 1];
                        break;
                    }
                }

                AddToSymbolTable(variableName, dataType, value, lineNumber.ToString());
                tfTokens.AppendText($"<variable, {variableName}>\n");
            }
        }

        private void ProcessIdentifier(string identifier, int lineNumber)
        {
            if (keywords.Contains(identifier)) return;

            if (!IdentifierExists(identifier))
            {
                AddToSymbolTable(identifier, "unknown", "undefined", lineNumber.ToString());
            }

            tfTokens.AppendText($"<identifier, {identifier}>\n");
        }

        private void AddToSymbolTable(string name, string type, string value, string lineNumber)
        {
            if (currentIndex >= MAX_ENTRIES - 1) return;

            currentIndex++;
            symbolTable[currentIndex, 0] = currentIndex.ToString();
            symbolTable[currentIndex, 1] = name;
            symbolTable[currentIndex, 2] = type;
            symbolTable[currentIndex, 3] = value;
            symbolTable[currentIndex, 4] = lineNumber;
        }

        private bool IdentifierExists(string identifier)
        {
            for (int i = 1; i <= currentIndex; i++)
            {
                if (symbolTable[i, 1] == identifier)
                    return true;
            }
            return false;
        }

        private void DisplaySymbolTable()
        {
            symbolTableDisplay.AppendText("SYMBOL TABLE\n");
            symbolTableDisplay.AppendText("============\n");

            for (int i = 0; i <= currentIndex; i++)
            {
                string row = $"{symbolTable[i, 0]}\t{symbolTable[i, 1]}\t{symbolTable[i, 2]}\t{symbolTable[i, 3]}\t{symbolTable[i, 4]}\n";
                symbolTableDisplay.AppendText(row);
            }
        }

        private void ResetSymbolTable()
        {
            for (int i = 1; i < MAX_ENTRIES; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    symbolTable[i, j] = "";
                }
            }
            currentIndex = 0;

            symbolTable[0, 0] = "Index";
            symbolTable[0, 1] = "Name";
            symbolTable[0, 2] = "Type";
            symbolTable[0, 3] = "Value";
            symbolTable[0, 4] = "Line";
        }

        // Helper methods
        private bool IsIdentifier(string token)
        {
            Regex identifierRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*$");
            return identifierRegex.IsMatch(token) && !keywords.Contains(token);
        }

        private bool IsConstant(string token)
        {
            Regex constantRegex = new Regex(@"^[0-9]+(\.[0-9]+)?$");
            return constantRegex.IsMatch(token);
        }

        private bool IsOperator(string token)
        {
            string[] operators = { "+", "-", "*", "/", "=", "==", "!=", "<", ">", "<=", ">=" };
            return Array.Exists(operators, op => op == token);
        }

        private bool IsDataType(string token)
        {
            string[] dataTypes = { "int", "float", "double", "char", "string", "void" };
            return Array.Exists(dataTypes, type => type == token);
        }
    }
}