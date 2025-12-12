using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WinFormsApp1

 { 
    partial class Form1
    {

        private List<string> keywordList = new List<string>
        {
            "int", "float", "while", "main", "if", "else", "new", "return"
        };
        private string[,] symbolTable = new string[20, 2];
        private List<string> varListInSymbolTable = new List<string>();
        private int row = 0;

        protected override  void Dispose  (bool disposing)
        {
          InitializeComponent ();
        }

      

        public  void   regular_Expression ( string expression)
        {
            Console.WriteLine("Enter Expression");

            string userresults = Console.ReadLine();

            string stringvalueregx = [''A-Z'];
            int intergervalueregex = ["0-9"];
            float floatvalueregex = 

       

            if (string.IsNullOrEmpty(userresults))
            {
                Console.WriteLine("Please enter any value");
                return;
            }
            else if (userresults.GetType() == typeof(string))
            {
                Console.WriteLine("String value",userresults);
                return;
            }
            else if (userresults.GetType()==typeof(int))
            {
                Console.WriteLine("Value is float",userresults);
            }
            else if (userresults.GetType()==typeof(float))
            {
                Console.WriteLine("value is float", userresults);
            }

            else  if (userresults.GetType()==typeof(double))
            {
                Console.WriteLine("value is double", userresults);
            }

           else
            {
                Console.WriteLine();
            }

    private void checkkeywords(List<string> keywordList)
        {
            foreach (string keyword in keywordList)
            {


            }

        }





        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}
