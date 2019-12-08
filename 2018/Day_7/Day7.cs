using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2018.Day_7
{
    class Day7
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(7);
            var lines = ReadInputFile.SplitLines(contents);

            var part1 = Part1(lines);
            Console.WriteLine($"Part 1: {part1}");
        }

        private static string Part1(string[] lines)
        {
            var tree = BuildTree(lines);

            var done = new List<string>();

            while (tree.Count > 0)
            {
                var next = tree.Where(kv => kv.Value.All(v => done.Contains(v) || v.Length == 0))
                               .OrderBy(kv => kv.Key)
                               .First()
                               .Key;

                done.Add(next);

                tree.Remove(next);
            }

            return string.Concat(done);
        }

        private static Dictionary<string, List<string>> BuildTree(string[] lines)
        {
            var tree = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                (string step, string req) = ParseLine(line);

                if (tree.ContainsKey(step))
                    tree[step].Add(req);
                else
                    tree.Add(step, new List<string>() { req });


                if (!tree.ContainsKey(req))
                    tree.Add(req, new List<string>());
            }

            return tree;
        }

        private static (string Step, string Requirement) ParseLine(string line)
        {
            var result = Regex.Match(line, @"Step (\w) must be finished before step (\w)");

            return (Step: result.Groups[2].Value, Requirement: result.Groups[1].Value);
        }
    }
}
