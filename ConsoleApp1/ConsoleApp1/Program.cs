using System;
using System.Text.RegularExpressions;


class  findkeywords
{
   static void Main()
    {

        Console.WriteLine("Enter a statment");
        String inputline = Console.ReadLine();

        Regex pattern = new Regex(@"\b(int|float|double)\b");

        Console.WriteLine("Detected Keyword");

        foreach(Match match in pattern.Matches(inputline))
        {
            Console.WriteLine(match.Value);
        }

    }
}