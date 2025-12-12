using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        ArrayList States = new ArrayList();
        Stack<string> Stack = new Stack<string>();
        string Parser;
        string[] Col = { "begin", "(", ")", "{", "int", "a", "b",
            "c", "=", "5", "10", "0", ";", "if", ">", "print",
            "else", "$", "}", "+", "end", "Program", "DecS", "AssS", "IffS", "PriS", "Var", "Const" };

        List<List<string>> Symboltable = new List<List<string>>();

        ArrayList LineNumber;
        ArrayList Variables;
        ArrayList KeyWords;
        ArrayList Constants;
        ArrayList finalArray;
        ArrayList tempArray;

        Regex variable_Reg;
        Regex constants_Reg;
        Regex operators_Reg;

        int lexemes_per_line;
        int ST_index;

        bool if_deleted;

        public Form1()
        {
            InitializeComponent();
        }

        private void Compile_Click(object sender, EventArgs e)
        {
            // Basic sanity: clear outputs
            Output.Clear();
            ST.Clear();
            CodeOutput.Clear();

            string[] k_ = { "int", "float", "begin", "end", "print", "if", "else" };
            ArrayList key = new ArrayList(k_);

            LineNumber = new ArrayList();
            Variables = new ArrayList();
            KeyWords = new ArrayList();
            Constants = new ArrayList();

            finalArray = new ArrayList();
            tempArray = new ArrayList();

            variable_Reg = new Regex("^[A-Za-z_][A-Za-z0-9]*$");
            constants_Reg = new Regex(@"^[0-9]+([.][0-9]+)?([e]([+\-])?[0-9]+)?$");
            operators_Reg = new Regex(@"[+\-/*=;>(){}]");

            int L = 1;

            Symboltable.Clear();

            if_deleted = false;

            string strinput = Input.Text ?? string.Empty;
            char[] charinput = strinput.ToCharArray();

            // Split into tokens (simple)
            for (int itr = 0; itr < charinput.Length; itr++)
            {
                string ch = charinput[itr].ToString();
                Match Match_Variable = variable_Reg.Match(ch);
                Match Match_Constant = constants_Reg.Match(ch);
                Match Match_Operator = operators_Reg.Match(ch);

                if (Match_Variable.Success || Match_Constant.Success)
                {
                    tempArray.Add(ch);
                }
                if (charinput[itr].Equals(' '))
                {
                    if (tempArray.Count != 0)
                    {
                        string fin = string.Empty;
                        for (int j = 0; j < tempArray.Count; j++) fin += tempArray[j];
                        finalArray.Add(fin);
                        tempArray.Clear();
                    }
                }
                if (Match_Operator.Success)
                {
                    if (tempArray.Count != 0)
                    {
                        string fin = string.Empty;
                        for (int j = 0; j < tempArray.Count; j++) fin += tempArray[j];
                        finalArray.Add(fin);
                        tempArray.Clear();
                    }
                    finalArray.Add(ch);
                }
            }
            if (tempArray.Count != 0)
            {
                string final = string.Empty;
                for (int k = 0; k < tempArray.Count; k++) final += tempArray[k];
                finalArray.Add(final);
            }

            // Very small demonstration: show tokens
            Output.AppendText("Tokens:\r\n");
            foreach (var t in finalArray) Output.AppendText(t + "\r\n");
        }
    }
}
