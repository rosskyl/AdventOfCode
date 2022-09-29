using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day6
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(6);
            var part1 = Part1(contents, 80);
            var part2 = Part2(contents, 256);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(string contents, int days)
        {
            var lanternfish = contents.Split(",").Select(c => int.Parse(c)).ToList();

            for (int i = 1; i <= days; i++)
            {
                var numToAdd = 0;
                lanternfish = lanternfish.Select(f =>
                {
                    f--;
                    if (f == -1)
                    {
                        numToAdd++;
                        f = 6;
                    }
                    return f;
                }).ToList();

                lanternfish.AddRange(Enumerable.Repeat(8, numToAdd));

                //Console.Write($"{i + 1} Days: ");
                //Console.WriteLine(string.Join(",", lanternfish));
            }

            return lanternfish.Count;
        }

        private static long Part2(string contents, int days)
        {
            var lanternfish = new Dictionary<int, long>
            {
                {0, 0 },
                {1, 0 },
                {2, 0 },
                {3, 0 },
                {4, 0 },
                {5, 0 },
                {6, 0 },
                {7, 0 },
                {8, 0 }
            };

            foreach (var initial in contents.Split(","))
            {
                lanternfish[int.Parse(initial)]++;
            }

            for (int i = 1; i <= days; i++)
            {
                var numToAdd = lanternfish[0];
                for (int j = 0; j < 8; j++)
                {
                    lanternfish[j] = lanternfish[j + 1];
                }
                lanternfish[8] = numToAdd;
                lanternfish[6] += numToAdd;

                //Console.WriteLine($"{i} days: {lanternfish.Values.Sum()}");
            }
            return lanternfish.Values.Select(f => (long)f).Sum();

        }
    }
}
