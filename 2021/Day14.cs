using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    public static class Day14
    {
        public static void Run()
        {
            const int steps1 = 10;
            const int steps2 = 40;
            var lines = ReadInputFile.ReadAndSplitFile(14);

            var polymer = lines[0];

            var rules = lines.Skip(2).ToDictionary(l => l.Split(" -> ")[0], l => l.Split(" -> ")[1]);
            
            var part1 = RunSteps(steps1, polymer, rules);
            var part2 = RunSteps2(steps2, polymer, rules);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int RunSteps(int steps, string polymer, Dictionary<string, string> rules)
        {
            for (int i = 1; i <= steps; i++)
            {
                var newPolymer = "";
                for (int j = 0; j < polymer.Length - 1; j++)
                {
                    newPolymer += polymer[j];
                    if (rules.ContainsKey(polymer.Substring(j, 2)))
                    {
                        newPolymer += rules[polymer.Substring(j, 2)];
                    }
                }
                newPolymer += polymer.Last();

                polymer = newPolymer;
            }

            var letters = polymer.Distinct().ToDictionary(p => p, p => polymer.Count(ch => ch == p));

            var min = letters.Values.Min();
            var max = letters.Values.Max();
            return max - min;
        }

        private static long RunSteps2(int steps, string polymer, Dictionary<string, string> rules)
        {
            var pairs = rules.ToDictionary(r => r.Key, r => (long)0);

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                if (rules.ContainsKey(polymer.Substring(i, 2)))
                    pairs[polymer.Substring(i, 2)]++;
            }
            var numLetters = string.Concat(pairs.Keys).Distinct().ToDictionary(p => p, p => (long)polymer.Count(l => l == p));


            for (int i = 1; i <= steps; i++)
            {
                var newPairs = pairs.ToDictionary(p => p.Key, p => (long)0);
                foreach (var pair in pairs.ToDictionary(p => p.Key, p => p.Value))
                {
                    if (pair.Value > 0)
                    {
                        var replacement = pair.Key[0] + rules[pair.Key] + pair.Key[1];
                        numLetters[rules[pair.Key][0]] += pair.Value;
                        newPairs[replacement.Substring(0, 2)] += pair.Value;
                        newPairs[replacement.Substring(1, 2)] += pair.Value;
                    }
                }
                pairs = newPairs;
            }

            

            var min = numLetters.Values.Min();
            var max = numLetters.Values.Max();

            return max - min;
        }
    }
}
