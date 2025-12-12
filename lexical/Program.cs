using System;
using System.Text.RegularExpressions;

class LexicalAnalyzer{
    static void Main(){

        //input to be provided by user
        string username = "M_Zain_Ul_Abidren";
        string email = "fa22-bcs-090@studeNts.cuisahiwal.edu.pk";

        //RE for username
        string usernamePattern = @"^[a-z0-9_]+$";
        //RE for email
        string emailPattern = @"^[a-z0-9-]+@[a-z]+\.[a-z]{2,}(\.[a-z]{2,})*$";

        //username validity
        if (Regex.IsMatch(username, usernamePattern))
{
            Console.WriteLine("Valid username");
        }
        else
        {
            Console.WriteLine("Invalid username");
        }

        //email validity
        
        if (Regex.IsMatch(email, emailPattern))
        {
            Console.WriteLine("Valid email address.");
        }
        else
        {
            Console.WriteLine("Invalid email address.");
        }
    }
}