using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Follow the given Grammar");
        Console.WriteLine("FIRST and FOLLOW Set Calculator");
        Console.WriteLine("Enter grammar productions one per line in the form A->aB|b|null");
        Console.WriteLine("Use 'null' to denote epsilon (empty string). Enter an empty line to finish.");
        Console.WriteLine("\nExample for the sample grammar (press Enter to use sample):");
        Console.WriteLine("S->aBAh");
        Console.WriteLine("B->xX");
        Console.WriteLine("X->bX|null");
        Console.WriteLine("A->bX|null");
        Console.WriteLine("E->G|null");
        Console.WriteLine("F->f|null\n");

        var productions = new Dictionary<char, List<string>>();
        var inputLines = new List<string>();

        Console.WriteLine("Enter your productions below (or press Enter immediately to use the sample):");

        while (true)
        {
            string line = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(line))
                break;
            inputLines.Add(line);
        }

        
        if (inputLines.Count == 0)
        {
            inputLines.AddRange(new[]
            {
                "S->aBAh",
                "B->xX",
                "X->bX|null",
                "A->bX|null",
                "E->G|null",
                "F->f|null"
            });
        }

        
        foreach (var line in inputLines)
        {
            var parts = line.Split("->", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;

            string left = parts[0].Trim();
            if (left.Length != 1 || !char.IsUpper(left[0])) continue;

            char A = left[0];
            var rhsList = parts[1].Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim() == "null" ? "ε" : s.Trim())
                .ToList();

            if (!productions.ContainsKey(A))
                productions[A] = new List<string>();

            productions[A].AddRange(rhsList);
        }

        if (productions.Count == 0)
        {
            Console.WriteLine("No valid productions found.");
            return;
        }

        var nonTerminals = productions.Keys.ToHashSet();
        var FIRST = new Dictionary<char, HashSet<string>>();
        var FOLLOW = new Dictionary<char, HashSet<string>>();

        foreach (var A in nonTerminals)
        {
            FIRST[A] = new HashSet<string>();
            FOLLOW[A] = new HashSet<string>();
        }

        char start = productions.Keys.First();
        FOLLOW[start].Add("$");

        HashSet<string> FirstOfSymbol(char sym)
        {
            var set = new HashSet<string>();
            if (char.IsUpper(sym))
            {
                if (FIRST.TryGetValue(sym, out var s))
                    foreach (var item in s) set.Add(item);
            }
            else
            {
                set.Add(sym.ToString());
            }
            return set;
        }

        
        bool changed = true;
        while (changed)
        {
            changed = false;
            foreach (var A in productions.Keys)
            {
                foreach (var rhs in productions[A])
                {
                    if (rhs == "ε")
                    {
                        if (FIRST[A].Add("ε")) changed = true;
                        continue;
                    }

                    bool allEps = true;
                    for (int i = 0; i < rhs.Length; i++)
                    {
                        char X = rhs[i];
                        var firstX = FirstOfSymbol(X);

                        foreach (var sym in firstX)
                        {
                            if (sym != "ε" && FIRST[A].Add(sym))
                                changed = true;
                        }

                        if (!firstX.Contains("ε"))
                        {
                            allEps = false;
                            break;
                        }
                    }

                    if (allEps && FIRST[A].Add("ε"))
                        changed = true;
                }
            }
        }

        
        changed = true;
        while (changed)
        {
            changed = false;
            foreach (var A in productions.Keys)
            {
                foreach (var rhs in productions[A])
                {
                    for (int i = 0; i < rhs.Length; i++)
                    {
                        char B = rhs[i];
                        if (!char.IsUpper(B)) continue;

                        var betaFirst = new HashSet<string>();
                        bool betaAllEps = true;

                        if (i + 1 < rhs.Length)
                        {
                            for (int j = i + 1; j < rhs.Length; j++)
                            {
                                char sym = rhs[j];
                                var firstSet = FirstOfSymbol(sym);
                                foreach (var t in firstSet)
                                {
                                    if (t != "ε") betaFirst.Add(t);
                                }
                                if (!firstSet.Contains("ε"))
                                {
                                    betaAllEps = false;
                                    break;
                                }
                            }
                        }

                        foreach (var t in betaFirst)
                        {
                            if (FOLLOW[B].Add(t))
                                changed = true;
                        }

                        if (betaAllEps || i == rhs.Length - 1)
                        {
                            foreach (var t in FOLLOW[A])
                            {
                                if (FOLLOW[B].Add(t))
                                    changed = true;
                            }
                        }
                    }
                }
            }
        }

   
        Console.WriteLine("\n--------------------------------");
        Console.WriteLine("Results for the given grammar:");
        Console.WriteLine("--------------------------------");

        Console.WriteLine("\nFIRST sets:");
        foreach (var A in productions.Keys.OrderBy(c => c))
        {
            var items = FIRST[A].OrderBy(s => s).ToArray();
            Console.WriteLine($"FIRST({A}) = {{ {string.Join(", ", items)} }}");
        }

        Console.WriteLine("\nFOLLOW sets:");
        foreach (var A in productions.Keys.OrderBy(c => c))
        {
            var items = FOLLOW[A].OrderBy(s => s).ToArray();
            Console.WriteLine($"FOLLOW({A}) = {{ {string.Join(", ", items)} }}");
        }
    }
}
