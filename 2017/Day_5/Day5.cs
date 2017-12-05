using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_5
{
    public static class Day5
    {
        public static void Run()
        {
            string input = "0\n3\n0\n1\n-3";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 5);
            Part1(actualInput);

            Console.WriteLine();

            Part2(input);

            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            List<int> instructions = SanitizeInput(input);
            int position = 0;
            int numSteps = 0;

            while (position < instructions.Count)
            {
                int jump = instructions[position];
                instructions[position] += 1;
                position += jump;
                numSteps += 1;
            }

            Console.WriteLine($"Number of steps: {numSteps}");
        }

        private static void Part2(string input)
        {
            List<int> instructions = SanitizeInput(input);
            int position = 0;
            int numSteps = 0;

            while (position < instructions.Count)
            {
                int jump = instructions[position];
                if (jump >= 3)
                    instructions[position] -= 1;
                else
                    instructions[position] += 1;
                position += jump;
                numSteps += 1;
            }

            Console.WriteLine($"Number of steps: {numSteps}");
        }

        private static List<int> SanitizeInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);

            List<int> instructions = new List<int>();
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int num))
                    instructions.Add(num);
            }

            return instructions;
        }
    }
}
