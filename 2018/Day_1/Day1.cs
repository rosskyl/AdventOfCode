using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2018.Day_1
{
    class Day1
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(1);
            //var contents = "+1\n-2\n+3\n+1";\
            //var contents = "+3\n+3\n+4\n-2\n-4";
            var lines = ReadInputFile.SplitLines(contents);

            var frequency1 = Part1(lines);
            Console.WriteLine($"Part 1: {frequency1}");

            var frequency2 = Part2(lines);
            Console.WriteLine($"Part 2: {frequency2}");
        }

        private static int Part1(string[] lines)
        {
            var frequency = 0;
            foreach (var line in lines)
            {
                if (int.TryParse(line, out int change))
                {
                    frequency += change;
                }
            }

            return frequency;
        }

        private static int Part2(string[] lines)
        {
            List<int> frequencies = new List<int>();
            var frequency = 0;

            var index = 0;
            while (frequencies.Count(f => f == frequency) <= 1)
            {
                if (int.TryParse(lines[index], out int change))
                {
                    frequency += change;
                    frequencies.Add(frequency);
                }
                index++;
                if (index >= lines.Length)
                    index = 0;
            }

            return frequency;
        }
    }
}
