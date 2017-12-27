using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_24
{
    public class Day24
    {
        public static void Run()
        {
            string input = "0/2\n2/2\n2/3\n3/4\n3/5\n0/1\n10/1\n9/10";

            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 24);
            Part1(actualInput);

            Console.WriteLine();

            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            List<int[]> bridges = ParseInput(input);

            int maxStrength = MakeBridge1(0, bridges);

            Console.WriteLine($"Max strength bridge is {maxStrength}");
        }

        private static int MakeBridge1(int start, List<int[]> bridges)
        {
            List<int[]> options = bridges.Where(b => b[0] == start || b[1] == start).ToList();

            int max = 0;
            foreach (int[] option in options)
            {
                List<int[]> newList = bridges.Select(b => b).ToList();
                newList.Remove(option);

                int strength;
                if (option[0] == start)
                    strength = MakeBridge1(option[1], newList);
                else
                    strength = MakeBridge1(option[0], newList);

                strength += option[0] + option[1];

                if (strength > max)
                    max = strength;
            }

            return max;
        }

        private static void Part2(string input)
        {
            List<int[]> bridges = ParseInput(input);

            int maxLength = 0;
            int maxStrength = MakeBridge2(0, bridges, out maxLength);

            Console.WriteLine($"Max strength bridge is {maxStrength}");
        }

        private static int MakeBridge2(int start, List<int[]> bridges, out int maxLength)
        {
            List<int[]> options = bridges.Where(b => b[0] == start || b[1] == start).ToList();

            int maxStrength = 0;
            maxLength = 0;
            foreach (int[] option in options)
            {
                List<int[]> newList = bridges.Select(b => b).ToList();
                newList.Remove(option);

                int strength;
                int length;
                if (option[0] == start)
                    strength = MakeBridge2(option[1], newList, out length);
                else
                    strength = MakeBridge2(option[0], newList, out length);

                strength += option[0] + option[1];

                if (length > maxLength)
                {
                    maxStrength = strength;
                    maxLength = length;
                }
                else if (strength > maxStrength && length == maxLength)
                {
                    maxStrength = strength;
                    maxLength = length;
                }
            }
            maxLength++;

            return maxStrength;
        }

        private static List<int[]> ParseInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);
            List<int[]> bridges = new List<int[]>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split('/');
                int a = int.Parse(splitLine[0]);
                int b = int.Parse(splitLine[1]);
                bridges.Add(new int[] { a, b });
            }

            return bridges;
        }
    }
}
