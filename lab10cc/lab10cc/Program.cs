using System;
using System.Collections.Generic;

class Program
{
 static void Main()
 {
 // Terminals and non-terminals
 string[] col = { "begin","(",")","{","int","a","b","c","=","5","10","0",";","if","#gt","print","else","$","}","+","end" };

 // Productions (LHS -> RHS tokens)
 var productions = new List<(string LHS, string[] RHS)>
 {
 ("Program", new[] { "begin","(",")","{","DecS","AssS","IffS","}","end" }),
 ("DecS", new[] { "int","Var","=","Const",";" }),
 ("AssS", new[] { "Var","=","Var","+","Var",";" }),
 ("IffS", new[] { "if","(","Var","#gt","Var",")","{","PriS","}","else","{","PriS","}" }),
 ("PriS", new[] { "print","Var",";" }),
 ("Var", new[] { "a" }),
 ("Var", new[] { "b" }),
 ("Var", new[] { "c" }),
 ("Const", new[] { "5" }),
 ("Const", new[] { "10" }),
 ("Const", new[] { "0" })
 };

 // Sample input tokens (valid program)
 var finalArray = new List<string>
 {
 "begin","(",")","{",
 "int","a","=","5",";",
 "b","=","10",";",
 "if","(","a","#gt","b",")","{","print","a",";","}","else","{","print","b",";","}",
 "}","end","$"
 };

 // Stack for symbols (strings)
 var stack = new List<string>();

 int pointer =0;

 void PrintStack()
 {
 Console.WriteLine("Stack: [" + string.Join(" ", stack) + "]");
 Console.WriteLine("Input: " + string.Join(" ", finalArray.GetRange(pointer, finalArray.Count - pointer)) );
 Console.WriteLine();
               
        }

 Console.WriteLine("Starting shift-reduce parser\n");
 // main loop
 while (true)
 {
 if (pointer >= finalArray.Count)
 {
 Console.WriteLine("Unexpected end of input");
               

 break;
 }

 string lookahead = finalArray[pointer];

 if (lookahead == "$")
 {
 // try final reductions
 bool reduced;
 do
 {
 reduced = TryReduce(stack, productions);
 } while (reduced);

 if (stack.Count ==1 && stack[0] == "Program")
 {
 Console.WriteLine("Parsed");
 }
 else
 {
 Console.WriteLine("Unable to Parse");
 }
 break;
 }

 // Shift
 Console.WriteLine($"Shift: {lookahead}");
 stack.Add(lookahead);
 pointer++;
 PrintStack();

 // Try reductions as much as possible
 bool anyReduced;
 do
 {
 anyReduced = TryReduce(stack, productions);
 } while (anyReduced);
 }
 }

 static bool TryReduce(List<string> stack, List<(string LHS, string[] RHS)> productions)
 {
 // Try to find a production whose RHS matches the top of the stack
 foreach (var prod in productions)
 {
 var rhs = prod.RHS;
 if (rhs.Length > stack.Count) continue;
 bool match = true;
 for (int i =0; i < rhs.Length; i++)
 {
 if (stack[stack.Count - rhs.Length + i] != rhs[i])
 {
 match = false; break;
 }
 }
 if (match)
 {
 // perform reduction
 Console.WriteLine($"Reduce: {prod.LHS} -> {string.Join(" ", rhs)}");
 // remove RHS
 stack.RemoveRange(stack.Count - rhs.Length, rhs.Length);
 // push LHS
 stack.Add(prod.LHS);
 Console.WriteLine();
 return true;
 }
 }
 return false;
  
 }
}
