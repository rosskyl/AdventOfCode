using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2018.Day_2
{
    class Day2
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(2);
            //var contents = "abcdef\nbababc\nabbcde\nabcccd\naabcdd\nabcdee\nababab";
            var lines = ReadInputFile.SplitLines(contents);

            var part1 = Part1(lines);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(lines);
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(string[] lines)
        {
            var sum2s = 0;
            var sum3s = 0;

            foreach (var line in lines)
            {
                bool extra2 = false;
                bool extra3 = false;
                foreach (var character in line.ToArray().Distinct())
                {
                    int count = line.Count(c => c == character);
                    if (count == 2)
                        extra2 = true;
                    if (count == 3)
                        extra3 = true;
                }

                if (extra2)
                    sum2s++;
                if (extra3)
                    sum3s++;
            }

            return sum2s * sum3s;
        }

        private static string Part2(string[] lines)
        {
            for (int index1 = 0; index1 < lines.Length; index1++)
            {
                for (int index2 = index1 + 1; index2 < lines.Length; index2++)
                {
                    int differences = 0;
                    string same = "";
                    for (int charIndex = 0; charIndex < lines[index1].Length; charIndex++)
                    {
                        if (lines[index1][charIndex] != lines[index2][charIndex])
                        {
                            differences++;
                        }
                        else
                            same += lines[index1][charIndex];
                    }
                    if (differences == 1)
                        return same;
                }
            }

            return "";
        }
    }
}
