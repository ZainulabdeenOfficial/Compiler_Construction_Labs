using System;
using System.Collections.Generic;

namespace CompilerConstruction
{
    class Program
    {
        static Dictionary<string, List<List<string>>> productions = new Dictionary<string, List<List<string>>>{
            {"E", new List<List<string>> { new List<string> {"T", "E'"} }},
            {"E'", new List<List<string>> { new List<string> {"+", "T", "E'"}, new List<string> {"ε"} }},
            {"T", new List<List<string>> { new List<string> {"F", "T'"} }},
            {"T'", new List<List<string>> { new List<string> {"*", "F", "T'"}, new List<string> {"ε"} }},
            {"F", new List<List<string>> { new List<string> {"(", "E", ")"}, new List<string> {"id"} }}
        };
        static Dictionary<string, HashSet<string>> firstSets = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, HashSet<string>> followSets = new Dictionary<string, HashSet<string>>();
        static string startSymbol = "E";

        static void Main(string[] args)
        {
            foreach (var nonTerminal in productions.Keys)
            {
                firstSets[nonTerminal] = new HashSet<string>();
                followSets[nonTerminal] = new HashSet<string>();
            }

            foreach (var nonTerminal in productions.Keys)
                ComputeFirst(nonTerminal, new HashSet<string>());

            ComputeFollow();

            Console.WriteLine("FIRST Sets:");
            foreach (var kvp in firstSets)
                Console.WriteLine($"FIRST({kvp.Key}) = {{ {string.Join(", ", kvp.Value)} }}");

            Console.WriteLine("\nFOLLOW Sets:");
            foreach (var kvp in followSets)
                Console.WriteLine($"FOLLOW({kvp.Key}) = {{ {string.Join(", ", kvp.Value)} }}");
        }

        static HashSet<string> ComputeFirst(string symbol, HashSet<string> visited)
        {
            if (!productions.ContainsKey(symbol))
                return new HashSet<string> { symbol };
            if (visited.Contains(symbol))
                return new HashSet<string>();
            visited.Add(symbol);
            foreach (var production in productions[symbol])
            {
                for (int i = 0; i < production.Count; i++)
                {
                    var part = production[i];
                    var firstOfPart = ComputeFirst(part, new HashSet<string>(visited));
                    foreach (var f in firstOfPart)
                    {
                        if (f != "ε")
                            firstSets[symbol].Add(f);
                    }
                    if (!firstOfPart.Contains("ε"))
                        break;
                    if (i == production.Count - 1)
                        firstSets[symbol].Add("ε");
                }
            }
            return firstSets[symbol];
        }

        static void ComputeFollow()
        {
            followSets[startSymbol].Add("$");
            bool updated;
            do
            {
                updated = false;
                foreach (var head in productions.Keys)
                {
                    foreach (var production in productions[head])
                    {
                        for (int i = 0; i < production.Count; i++)
                        {
                            string symbol = production[i];
                            if (!productions.ContainsKey(symbol))
                                continue;
                            var followBefore = new HashSet<string>(followSets[symbol]);
                            if (i + 1 < production.Count)
                            {
                                var next = production[i + 1];
                                var firstNext = ComputeFirst(next, new HashSet<string>());
                                foreach (var f in firstNext)
                                {
                                    if (f != "ε")
                                        followSets[symbol].Add(f);
                                }
                                if (firstNext.Contains("ε"))
                                {
                                    foreach (var f in followSets[head])
                                        followSets[symbol].Add(f);
                                }
                            }
                            else
                            {
                                foreach (var f in followSets[head])
                                    followSets[symbol].Add(f);
                            }
                            if (followSets[symbol].Count > followBefore.Count)
                                updated = true;
                        }
                    }
                }
            } while (updated);
        }
    }
}
