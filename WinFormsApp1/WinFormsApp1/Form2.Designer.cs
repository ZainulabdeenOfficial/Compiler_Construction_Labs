using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LexicalAnalyzerV1
{
    public class LexicalAnalyzer
    {
        private List<string> keywordList = new List<string>
        {
            "int", "float", "while", "main", "if", "else", "new", "return"
        };

        private string[,] symbolTable = new string[20, 2]; // store variable name & type
        private List<string> varListInSymbolTable = new List<string>();
        private int row = 0;

        // Regex patterns
        private Regex variable_Reg = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
        private Regex constants_Reg = new Regex(@"^[0-9]+(\.[0-9]+)?$");
        private Regex operators_Reg = new Regex(@"^(==|!=|<=|>=|&&|\|\||[+\-*/=<>&|])$");
        private Regex special_Reg = new Regex(@"^[(){};,]$");

        public ArrayList Analyze(string userInput)
        {
            ArrayList tokens = new ArrayList();

            // Split input by whitespace and operators/special chars
            string pattern = @"(==|!=|<=|>=|&&|\|\||[+\-*/=<>&|(){};,])";
            string[] parts = Regex.Split(userInput, pattern);

            foreach (string part in parts)
            {
                string token = part.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                // 1. Check for keyword
                if (keywordList.Contains(token))
                {
                    tokens.Add($"Keyword -> {token}");
                }
                // 2. Check for identifier (variable)
                else if (variable_Reg.IsMatch(token))
                {
                    tokens.Add($"Identifier -> {token}");
                    AddToSymbolTable(token, "Identifier");
                }
                // 3. Check for number/constant
                else if (constants_Reg.IsMatch(token))
                {
                    tokens.Add($"Constant -> {token}");
                }
                // 4. Check for operator
                else if (operators_Reg.IsMatch(token))
                {
                    tokens.Add($"Operator -> {token}");
                }
                // 5. Check for special symbol
                else if (special_Reg.IsMatch(token))
                {
                    tokens.Add($"Special Symbol -> {token}");
                }
                else
                {
                    tokens.Add($"Unknown Token -> {token}");
                }
            }

            return tokens;
        }

        private void AddToSymbolTable(string variable, string type)
        {
            if (!varListInSymbolTable.Contains(variable))
            {
                varListInSymbolTable.Add(variable);
                symbolTable[row, 0] = variable;
                symbolTable[row, 1] = type;
                row++;
            }
        }
    }
}
b