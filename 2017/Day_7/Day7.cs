using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_7
{
    public static class Day7
    {
        public static void Run()
        {
            string input = ReadInputFile.ReadFile("TestInput.txt", 7);
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 7);
            Part1(actualInput);

            Console.WriteLine();
            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            List<Program> allPrograms = SanitizeInput(input);
            Program bottomNode = GetBottomNode(allPrograms);
            Console.WriteLine($"The bottom program is {bottomNode.Name}");
        }

        private static void Part2(string input)
        {
            List<Program> allPrograms = SanitizeInput(input);

            Program baseNode = null;

            CreateTowers(ref allPrograms, ref baseNode);

            Program problemNode = GetProblemNode(baseNode, out int difference);

            Console.Write($"The problem node is {problemNode.Name} and needs to be changed by {difference}");
            Console.WriteLine($" which would give it a weight of {problemNode.Weight + difference}");
        }

        private static Program GetBottomNode(List<Program> allPrograms)
        {
            List<Program> possibleBottomNodes = allPrograms.Where(p => p.Children.Count > 0).ToList();
            List<string> childrenNodes = new List<string>();
            foreach (Program node in possibleBottomNodes)
            {
                childrenNodes.AddRange(node.Children);
            }

            Program bottomNode = possibleBottomNodes.First(n => !childrenNodes.Contains(n.Name));
            return bottomNode;
        }

        private static Program GetProblemNode(Program baseNode, out int difference)
        {
            if (!baseNode.IsBalanced())
            {
                foreach (Program childNode in baseNode.ChildrenNodes)
                {
                    if (!childNode.IsBalanced())
                    {
                        return GetProblemNode(childNode, out difference);
                    }
                }

                if (baseNode.ChildrenNodes.Count > 0)
                {
                    List<int> weights = baseNode.ChildrenNodes.Select(n => n.TowerWeight()).ToList();
                    int targetWeight = weights.First(w => weights.Count(we => we == w) > 1);
                    int problemWeight = weights.First(w => w != targetWeight);
                    difference = targetWeight - problemWeight;
                    return baseNode.ChildrenNodes.First(n => n.TowerWeight() == problemWeight);
                }
                else
                {
                    difference = 0;
                    return baseNode;
                }
            }
            difference = 0;
            return null;
        }

        private static void CreateTowers(ref List<Program> allPrograms, ref Program baseNode)
        {
            if (baseNode == null)
                baseNode = GetBottomNode(allPrograms);

            foreach (string nodeName in baseNode.Children)
            {
                Program childNode = allPrograms.First(n => n.Name == nodeName);
                allPrograms.Remove(childNode);
                CreateTowers(ref allPrograms, ref childNode);
                baseNode.ChildrenNodes.Add(childNode);
            }
        }

        private static List<Program> SanitizeInput(string input)
        {
            List<Program> programs;
            string[] lines = ReadInputFile.SplitLines(input);
            programs = lines.Select(l => new Program(l)).ToList();
            return programs;
        }
    }
}
