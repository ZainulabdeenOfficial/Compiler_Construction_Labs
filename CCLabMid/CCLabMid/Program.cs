// Either "electric" or "Inductive" followed by "charging" followed by either "road"
//or "Highway" like 'Inductive charging raod' with space in between.

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


Console.WriteLine("Q1 (i)");

Console.WriteLine("Enter  electric  charging  inductive & road highway ");
 string userinput =  Console.ReadLine();
string regxpattern = @"^[electric|inductive|Electric|Inductive]+\s[charging|Charging ]+\s[raod|highway|Road|Highway]";


if (Regex.IsMatch(userinput,regxpattern))
{
    Console.WriteLine("String matched");
}

else
    {
        Console.WriteLine("Not pattren match ");
}


// (ii)
//  username and email 

Console.WriteLine("ii)username and email match");

//string username = "zain";
//string email = "fa22-bcs-090@students.cuisahiwal.edu.pk";

string username = Console.ReadLine();
string email = Console.ReadLine();
       
        string usernamePattern = @"^[a-z0-9_]+$";

     string emailPattern = @"^[a-z0-9-]+@[a-z]+\.[a-z]{2,}(\.[a-z]{2,})*$";
     //string emailPattern = @"^[a-z]{2}\d{2}-[a-z]{2468}@[a-z]+\.[a-z]{2}*$";

if (Regex.IsMatch(username, usernamePattern))
        {
            Console.WriteLine("Valid username");
        }
        else
        {
            Console.WriteLine("Invalid username");
        }

        if (Regex.IsMatch(email,emailPattern))
        {
            Console.WriteLine("Valid email address.");
        }
        else
        {
            Console.WriteLine("Invalid email address.");
        }








    




