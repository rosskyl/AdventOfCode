using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2018.Day_5
{
    class Day5
    {
        public static void Run()
        {
            //var contents = "dabAcCaCBAcCcaDA";
            //var contents = "dadD";
            var contents = ReadInputFile.ReadFile(5);

            var part1 = Part1(contents);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(contents);
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(string polymer)
        {
            int index = 0;
            index = FindAdjacentIndex(polymer, ref index);
            while (index != -1)
            {
                polymer = polymer.Remove(index, 2);
                if (index > 0)
                    index--;
                index = FindAdjacentIndex(polymer, ref index);
            }

            return polymer.Length;
        }

        private static int Part2(string polymer)
        {
            int lowest = int.MaxValue;
            foreach (var unit in polymer.ToLower().Distinct())
            {
                var testPolymer = polymer.Replace(unit.ToString(), "")
                                         .Replace(unit.ToString().ToUpper(), "");

                var length = Part1(testPolymer);

                if (length < lowest)
                    lowest = length;
            }
            return lowest;
        }

        private static int FindAdjacentIndex(string polymer, ref int index)
        {
            for (; index < polymer.Length - 1; index++)
            {
                var current = polymer[index].ToString();
                var next = polymer[index + 1].ToString();

                if (current.ToLower() == current)
                {
                    if (next == current.ToUpper())
                        return index;
                }
                else
                {
                    if (next == current.ToLower())
                        return index;
                }
            }
            return -1;
        }
    }
}
