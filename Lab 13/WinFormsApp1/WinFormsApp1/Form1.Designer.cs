using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        //Symbol Table 2D List
        List<List<string>> Symboltable = new List<List<string>>();

        ArrayList LineNumber;
        ArrayList Variables;
        ArrayList KeyWords;
        ArrayList Constants;
        ArrayList Operators;



        ArrayList finalArray;
        ArrayList tempArray;

        Regex variable_Reg;
        Regex constants_Reg;
        Regex operators_Reg;
        Regex special_Reg;

        int lexemes_per_line;
        int ST_index;

        // Controls declared here so other methods can reference them
        private Button btn_Input;
        private TextBox tfInput;
        private TextBox tfTokens;
        private TextBox symbolTable;
        private TextBox Output;
        private TextBox ST;
        
        


        // Constructor removed from designer. Keep constructor in Form1.cs partial class.

        /// <summary>
        /// Helper to concatenate all items in an ArrayList into a single string using each item's ToString().
        /// This avoids casting the ArrayList to a char[] which fails when items are strings.
        /// </summary>
        private string ConcatArrayList(ArrayList list)
        {
            if (list == null || list.Count == 0) return string.Empty;
            var sb = new StringBuilder(list.Count);
            foreach (var item in list)
            {
                if (item != null)
                    sb.Append(item.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Initialize lexical analyzer specific fields. This is called after
        /// InitializeComponent so controls like Output and ST are available.
        /// </summary>
        private void InitializeLexicalAnalyzer()
        {
            LineNumber = new ArrayList();
            Variables = new ArrayList();
            KeyWords = new ArrayList();
            Constants = new ArrayList();

            finalArray = new ArrayList();
            tempArray = new ArrayList();

            // Regex
            variable_Reg = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
            constants_Reg = new Regex(@"^[0-9]+([.][0-9]+)?$");
            operators_Reg = new Regex(@"^[-+*/=<>]$");
            special_Reg = new Regex(@"^[.,'\[\]{}();:?]$");

            Symboltable.Clear();

            // Controls are created in InitializeComponent; ensure they're not null
            if (Output != null) Output.Text = "";
            if (ST != null) ST.Text = "";
        }

        /// <summary>
        /// Minimal InitializeComponent implementation to provide the controls referenced
        /// in the form logic. This keeps the project compiling and lets the lexical
        /// analyzer code run. Adjust layout in the WinForms designer as needed.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Input = new System.Windows.Forms.Button();
            this.tfInput = new System.Windows.Forms.TextBox();
            this.tfTokens = new System.Windows.Forms.TextBox();
            this.symbolTable = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TextBox();
            this.ST = new System.Windows.Forms.TextBox();

            // 
            // btn_Input
            // 
            this.btn_Input.Name = "btn_Input";
            this.btn_Input.Text = "Analyze";
            this.btn_Input.Size = new System.Drawing.Size(75, 23);
            this.btn_Input.Location = new System.Drawing.Point(10, 10);
            this.btn_Input.Click += new System.EventHandler(this.btn_Input_Click);
            this.btn_Input.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;

            // 
            // tfInput
            // 
            this.tfInput.Name = "tfInput";
            this.tfInput.Multiline = true;
            this.tfInput.Size = new System.Drawing.Size(400, 120);
            this.tfInput.Location = new System.Drawing.Point(10, 40);
            this.tfInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tfInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // 
            // tfTokens
            // 
            this.tfTokens.Name = "tfTokens";
            this.tfTokens.Multiline = true;
            this.tfTokens.ReadOnly = true;
            this.tfTokens.Size = new System.Drawing.Size(400, 120);
            this.tfTokens.Location = new System.Drawing.Point(10, 170);
            this.tfTokens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tfTokens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;

            // 
            // symbolTable
            // 
            this.symbolTable.Name = "symbolTable";
            this.symbolTable.Multiline = true;
            this.symbolTable.ReadOnly = true;
            this.symbolTable.Size = new System.Drawing.Size(400, 120);
            this.symbolTable.Location = new System.Drawing.Point(10, 300);
            this.symbolTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.symbolTable.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;

            // 
            // Output
            // 
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(200, 20);
            this.Output.Location = new System.Drawing.Point(420, 40);
            this.Output.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            // 
            // ST
            // 
            this.ST.Name = "ST";
            this.ST.Size = new System.Drawing.Size(200, 20);
            this.ST.Location = new System.Drawing.Point(420, 70);
            this.ST.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.btn_Input);
            this.Controls.Add(this.tfInput);
            this.Controls.Add(this.tfTokens);
            this.Controls.Add(this.symbolTable);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.ST);
            this.Text = "Lexical Analyzer";
        }

        private void btn_Input_Click(object sender, EventArgs e)
        {
            string userInput = tfInput.Text;

            List<string> keywordList = new List<string>()
            {
                "int", "float", "while", "main", "if", "else", "new"
            };

            String[,] SymbolTable = new String[50, 6];

            int row = 1;
            int count = 1;
            int line_num = 0;

            finalArray.Clear();
            tempArray.Clear();
            tfTokens.Clear();
            symbolTable.Clear();

            char[] charinput = userInput.ToCharArray();

            // FIRST PASS — SPLIT LINES
            for (int i = 0; i < charinput.Length; i++)
            {
                if (charinput[i] != '\n')
                {
                    tempArray.Add(charinput[i]);
                }
                else
                {
                    finalArray.Add(ConcatArrayList(tempArray));
                    tempArray.Clear();
                }
            }

            if (tempArray.Count != 0)
            {
                finalArray.Add(ConcatArrayList(tempArray));
                tempArray.Clear();
            }

            // SECOND PASS — TOKENIZATION
            ArrayList finalArrayc = new ArrayList();

            for (int i = 0; i < finalArray.Count; i++)
            {
                string line = finalArray[i].ToString();
                line_num++;

                finalArrayc.Clear();
                tempArray.Clear();

                char[] arr = line.ToCharArray();

                for (int j = 0; j < arr.Length; j++)
                {
                    string ch = arr[j].ToString();

                    Match isVar = variable_Reg.Match(ch);
                    Match isConst = constants_Reg.Match(ch);
                    Match isOp = operators_Reg.Match(ch);
                    Match isSpec = special_Reg.Match(ch);

                    if (isVar.Success || isConst.Success)
                    {
                        tempArray.Add(ch);
                    }
                    else if (ch == " ")
                    {
                        if (tempArray.Count != 0)
                        {
                            finalArrayc.Add(ConcatArrayList(tempArray));
                            tempArray.Clear();
                        }
                    }
                    else if (isOp.Success || isSpec.Success)
                    {
                        if (tempArray.Count != 0)
                        {
                            finalArrayc.Add(ConcatArrayList(tempArray));
                            tempArray.Clear();
                        }
                        finalArrayc.Add(ch);
                    }
                }

                if (tempArray.Count != 0)
                {
                    finalArrayc.Add(ConcatArrayList(tempArray));
                    tempArray.Clear();
                }

                // THIRD PASS — GENERATE TOKENS
                for (int x = 0; x < finalArrayc.Count; x++)
                {
                    string lex = finalArrayc[x].ToString();

                    Match op = operators_Reg.Match(lex);
                    Match dig = constants_Reg.Match(lex);
                    Match var = variable_Reg.Match(lex);
                    Match punct = special_Reg.Match(lex);

                    if (op.Success)
                    {
                        tfTokens.AppendText("<op, " + lex + "> ");
                    }
                    else if (dig.Success)
                    {
                        tfTokens.AppendText("<digit, " + lex + "> ");
                    }
                    else if (punct.Success)
                    {
                        tfTokens.AppendText("<punc, " + lex + "> ");
                    }
                    else if (var.Success)
                    {
                        if (!keywordList.Contains(lex))
                        {
                            // Variable Token
                            tfTokens.AppendText("<var" + count + ", " + row + "> ");

                            SymbolTable[row, 1] = row.ToString();
                            SymbolTable[row, 2] = lex;
                            SymbolTable[row, 3] = "unknown";
                            SymbolTable[row, 4] = "null";
                            SymbolTable[row, 5] = line_num.ToString();

                            symbolTable.AppendText(
                                SymbolTable[row, 1] + "\t" +
                                SymbolTable[row, 2] + "\t" +
                                SymbolTable[row, 3] + "\t" +
                                SymbolTable[row, 4] + "\t" +
                                SymbolTable[row, 5] + "\n"
                            );

                            row++;
                            count++;
                        }
                        else
                        {
                            // Keyword token
                            tfTokens.AppendText("<keyword, " + lex + "> ");
                        }
                    }
                }

                tfTokens.AppendText("\n");
            }
        }
    }
}
