using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day08
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(8);

            //lines = ReadInputFile.SplitLines("RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)");
            //lines = ReadInputFile.SplitLines("LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)");

            Part1(lines);

            //lines = ReadInputFile.SplitLines("LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)");

            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            var instructions = lines[0].ToCharArray();
            var nodes = lines.Skip(2).Select(line => new Node(line)).ToDictionary(node => node.Name, node => node);
            var current = "AAA";
            var currentInstructionIndex = 0;
            var steps = 0;

            while (current != "ZZZ")
            {
                if (currentInstructionIndex >= instructions.Length)
                    currentInstructionIndex = 0;

                if (instructions[currentInstructionIndex] == 'L')
                    current = nodes[current].Left;
                else
                    current = nodes[current].Right;

                steps++;

                currentInstructionIndex++;
            }

            Console.WriteLine($"Part 1: {steps}");
        }

        public static void Part2(string[] lines)
        {
            var instructions = lines[0].ToCharArray();
            var nodes = lines.Skip(2).Select(line => new Node(line)).ToDictionary(node => node.Name, node => node);
            var currentInstructionIndex = 0;
            long steps = 0;
            var currentNodes = nodes.Values.Where(node => node.IsStartNode()).ToList();
            var stepsToFinish = new List<long>();
            Console.WriteLine($"Starting with {currentNodes.Count} nodes");
            while (!currentNodes.All(node => node.IsEndNode()))
            {
                var stillWorkingNodes = new List<Node>();
                if (currentInstructionIndex >= instructions.Length)
                    currentInstructionIndex = 0;

                for (int i = 0; i < currentNodes.Count; i++)
                {
                    if (instructions[currentInstructionIndex] == 'L')
                        currentNodes[i] = nodes[nodes[currentNodes[i].Name].Left];
                    else
                        currentNodes[i] = nodes[nodes[currentNodes[i].Name].Right];

                    if (currentNodes[i].IsEndNode())
                    {
                        Console.WriteLine($"Removing spot {i} as it's finished on instruction {currentInstructionIndex} after {steps} steps");
                        stepsToFinish.Add(steps+1);
                    }
                    else
                        stillWorkingNodes.Add(currentNodes[i]);
                }

                currentNodes = stillWorkingNodes;

                steps++;

                currentInstructionIndex++;
            }

            steps = FindLeastCommonMultiple(stepsToFinish);

            Console.WriteLine($"Part 2: {steps}");
        }

        public static long FindLeastCommonMultiple(List<long> numbers)
        {
            long current = numbers.Max();
            long increment = current;

            while (numbers.Any(num => current % num != 0))
            {
                current += increment;
            }

            return current;
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }

        public Node(string line)
        {
            var regex = new Regex("(\\w\\w\\w) = \\((\\w\\w\\w), (\\w\\w\\w)\\)");
            var match = regex.Match(line);

            if (!match.Success)
                throw new Exception("Regex didn't match");

            Name = match.Groups[1].Value;
            Left = match.Groups[2].Value;
            Right = match.Groups[3].Value;
        }

        public bool IsStartNode()
        {
            return Name.EndsWith('A');
        }

        public bool IsEndNode()
        {
            return Name.EndsWith('Z');
        }
    }
}
