using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LexicalAnalyzerV1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your code (end with an empty line):");
            string userInput = "";
            string line;
            while ((line = Console.ReadLine()) != null && line != "")
            {
                userInput += line + "\n";
            }

            List<string> keywordList = new List<string> { "int", "float", "while", "main", "if", "else", "new" };
            int line_num = 0;
            ArrayList finalArray = new ArrayList();
            ArrayList tempArray = new ArrayList();
            char[] charInput = userInput.ToCharArray();
            Regex variable_Reg = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
            Regex constants_Reg = new Regex(@"^[0-9]+([.][0-9]+)?([e|E]([+|-])?[0-9]+)?$");
            Regex operators_Reg = new Regex(@"[*/+\-<&|!%]");
            Regex Special_Reg = new Regex(@"[=+,'.;\\[\\]{}()<>?:]$");

            for (int itr = 0; itr < charInput.Length; itr++)
            {
                Match Match_Variable = variable_Reg.Match(charInput[itr] + "");
                Match Match_Constant = constants_Reg.Match(charInput[itr] + "");
                Match Match_Operator = operators_Reg.Match(charInput[itr] + "");
                Match Match_Special = Special_Reg.Match(charInput[itr] + "");

                if (Match_Variable.Success || Match_Constant.Success || Match_Operator.Success || Match_Special.Success || charInput[itr].Equals(' '))
                {
                    tempArray.Add(charInput[itr]);
                }

                if (charInput[itr].Equals('\n'))
                {
                    if (tempArray.Count != 0)
                    {
                        int j = 0;
                        String fin = "";
                        for (; j < tempArray.Count; j++)
                        {
                            fin += tempArray[j];
                        }
                        finalArray.Add(fin);
                        tempArray.Clear();
                    }
                }
            }

            if (tempArray.Count != 0)
            {
                int j = 0;
                String fin = "";
                for (; j < tempArray.Count; j++)
                {
                    fin += tempArray[j];
                }
                finalArray.Add(fin);
                tempArray.Clear();
            }

            // Tokenization logic
            for (int i = 0; i < finalArray.Count; i++)
            {
                String l = finalArray[i].ToString();
                char[] lineChar = l.ToCharArray();
                line_num++;
                List<string> finalArrayc = new List<string>();
                tempArray.Clear();
                for (int itr = 0; itr < lineChar.Length; itr++)
                {
                    Match Match_Variable = variable_Reg.Match(lineChar[itr] + "");
                    Match Match_Constant = constants_Reg.Match(lineChar[itr] + "");
                    Match Match_Operator = operators_Reg.Match(lineChar[itr] + "");
                    Match Match_Special = Special_Reg.Match(lineChar[itr] + "");
                    if (Match_Variable.Success || Match_Constant.Success)
                    {
                        tempArray.Add(lineChar[itr]);
                    }
                    if (lineChar[itr].Equals(' '))
                    {
                        if (tempArray.Count != 0)
                        {
                            int j = 0;
                            String fin = "";
                            for (; j < tempArray.Count; j++)
                            {
                                fin += tempArray[j];
                            }
                            finalArrayc.Add(fin);
                            tempArray.Clear();
                        }
                    }
                    if (Match_Operator.Success || Match_Special.Success)
                    {
                        if (tempArray.Count != 0)
                        {
                            int j = 0;
                            String fin = "";
                            for (; j < tempArray.Count; j++)
                            {
                                fin += tempArray[j];
                            }
                            finalArrayc.Add(fin);
                            tempArray.Clear();
                        }
                        finalArrayc.Add(lineChar[itr].ToString());
                    }
                }
                if (tempArray.Count != 0)
                {
                    String fina = "";
                    for (int k = 0; k < tempArray.Count; k++)
                    {
                        fina += tempArray[k];
                    }
                    finalArrayc.Add(fina);
                    tempArray.Clear();
                }
                // Token output
                foreach (var lexeme in finalArrayc)
                {
                    string lex = lexeme.ToString();
                    Match operators = operators_Reg.Match(lex);
                    Match variables = variable_Reg.Match(lex);
                    Match digits = constants_Reg.Match(lex);
                    Match punctuations = Special_Reg.Match(lex);
                    if (operators.Success)
                    {
                        Console.Write("< op, " + lex + " > ");
                    }
                    else if (digits.Success)
                    {
                        Console.Write("< digit, " + lex + " > ");
                    }
                    else if (punctuations.Success)
                    {
                        Console.Write("< punc, " + lex + " > ");
                    }
                    else if (variables.Success)
                    {
                        if (!keywordList.Contains(lex))
                        {
                            Console.Write("< id, " + lex + " > ");
                        }
                        else
                        {
                            Console.Write("< keyword, " + lex + " > ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}