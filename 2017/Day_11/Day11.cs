using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_11
{
    public static class Day11
    {
        public static void Run()
        {
            TestPart1();
            string actualInput = ReadInputFile.ReadFile("Input.txt", 11);
            BothParts(actualInput);
        }

        private static void TestPart1()
        {
            string input = "ne,ne,ne";
            BothParts(input, false);
            Console.WriteLine(" should be 3");

            input = "ne,ne,sw,sw";
            BothParts(input, false);
            Console.WriteLine(" should be 0");

            input = "ne,ne,s,s";
            BothParts(input, false);
            Console.WriteLine(" should be 2");

            input = "se,sw,se,sw,sw";
            BothParts(input, false);
            Console.WriteLine(" should be 3");

        }

        private static void BothParts(string input, bool endLine = true)
        {
            int north = 0;
            int east = 0;
            int maxSteps = 0;
            int steps = 0;

            string[] path = ParseInput(input);
            foreach (string step in path)
            {
                switch (step)
                {
                    case "n":
                        north += 2;
                        break;
                    case "ne":
                        north += 1;
                        east += 1;
                        break;
                    case "nw":
                        north += 1;
                        east -= 1;
                        break;
                    case "s":
                        north -= 2;
                        break;
                    case "se":
                        north -= 1;
                        east += 1;
                        break;
                    case "sw":
                        north -= 1;
                        east -= 1;
                        break;
                }
                steps = CalculateMinSteps(north, east);
                if (steps > maxSteps)
                    maxSteps = steps;
            }

            string output = $"Steps needed: {steps}, max is {maxSteps}";
            if (endLine)
                Console.WriteLine(output);
            else
                Console.Write(output);
        }

        private static int CalculateMinSteps(int north, int east)
        {
            int steps = 0;
            north = Math.Abs(north);
            east = Math.Abs(east);
            if (north > east)
            {
                steps = east;
                north = (north - (east * steps)) / 2;
                steps += north;
            }
            else
            {
                steps = east;
            }

            return steps;
        }

        private static string[] ParseInput(string input)
        {
            return input.Split(',');
        }
    }
}
