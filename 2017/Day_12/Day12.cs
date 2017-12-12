using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_12
{
    public static class Day12
    {
        public static void Run()
        {
            string input = ReadInputFile.ReadFile("TestInput.txt", 12);
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 12);
            Part1(actualInput);

            Console.WriteLine();
            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            Dictionary<int, List<int>> nodes = ParseInput(input);

            List<int> neighbourNodes = new List<int>();

            neighbourNodes.Add(0);
            List<int> newNodes = null;
            while (newNodes == null || newNodes.Count > 0)
            {
                newNodes = nodes.Where(kv => kv.Value.Intersect(neighbourNodes).Any() && !neighbourNodes.Contains(kv.Key)).Select(kv => kv.Key).ToList();
                neighbourNodes.AddRange(newNodes);
            }

            Console.WriteLine($"Total of {neighbourNodes.Count} nodes in group with 0.");
        }

        private static void Part2(string input)
        {
            int numberOfGroups = 0;
            Dictionary<int, List<int>> nodes = ParseInput(input);

            while (nodes.Count > 0)
            {
                List<int> neighbourNodes = new List<int>();

                neighbourNodes.Add(nodes.Keys.First());
                List<int> newNodes = null;
                while (newNodes == null || newNodes.Count > 0)
                {
                    newNodes = nodes.Where(kv => kv.Value.Intersect(neighbourNodes).Any() && !neighbourNodes.Contains(kv.Key)).Select(kv => kv.Key).ToList();
                    neighbourNodes.AddRange(newNodes);
                }

                neighbourNodes.ForEach(n => nodes.Remove(n));
                numberOfGroups += 1;
            }

            Console.WriteLine($"There are {numberOfGroups} groups");
        }

        private static Dictionary<int, List<int>> ParseInput(string input)
        {
            Dictionary<int, List<int>> nodes = new Dictionary<int, List<int>>();

            string[] lines = ReadInputFile.SplitLines(input);
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                int nodeName = int.Parse(splitLine[0]);
                List<int> neighbors = new List<int>();
                foreach (string neighbour in splitLine.Skip(2))
                {
                    neighbors.Add(int.Parse(string.Concat(neighbour.Where(c => char.IsDigit(c)))));
                }
                nodes.Add(nodeName, neighbors);
            }

            return nodes;
        }
    }
}
