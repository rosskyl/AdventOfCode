using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_13
{
    public static class Day13
    {
        public static void Run()
        {
            string input = "0: 3\n1: 2\n4: 4\n6: 4";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 13);
            Part1(actualInput);

            Console.WriteLine();

            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            Dictionary<int, int> layers = ParseInput(input);
            
            int severity = 0;
            
            foreach (int layer in layers.Keys)
            {
                if (layer % ((layers[layer] - 1) * 2) == 0)
                {
                    severity += (layer * layers[layer]);
                }
            }

            Console.WriteLine($"Severity is {severity}");
        }

        private static void Part2(string input)
        {
            Dictionary<int, int> layers = ParseInput(input);
            int delay = 0;
            while (IsCaught(layers, delay))
                delay++;

            Console.WriteLine($"Need to delay by {delay}");
        }

        private static bool IsCaught(Dictionary<int, int> layers, int timeDelay)
        {
            foreach (int layer in layers.Keys)
            {
                if ((layer + timeDelay) % ((layers[layer] - 1) * 2) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static void MoveScanners(Dictionary<int, int[]> layers)
        {
            foreach (int key in layers.Keys)
            {
                if (layers[key][1] == layers[key][0] - 1)
                    layers[key][2] = -1;
                else if (layers[key][1] == 0)
                    layers[key][2] = 1;
                layers[key][1] += layers[key][2];
            }
        }

        private static Dictionary<int, int> ParseInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);

            Dictionary<int, int> layers = new Dictionary<int, int>();
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                int depth = int.Parse(string.Concat(splitLine[0].Where(c => char.IsDigit(c))));
                int range = int.Parse(string.Concat(splitLine[1].Where(c => char.IsDigit(c))));
                layers.Add(depth, range);
            }

            return layers;
        }
    }
}
