using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2018.Day_3
{
    class Day3
    {
        private const int size = 1000;

        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(3);
            var lines = ReadInputFile.SplitLines(contents);

            var rectangles = lines.Select(l => new Rectangle(l));

            var part1 = Part1(rectangles);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(rectangles.ToList());
            Console.WriteLine($"Part 2: {part2}");
        }

        public static int Part1(IEnumerable<Rectangle> rectangles)
        {
            int[,] grid = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    grid[i, j] = 0;

            foreach (var rect in rectangles)
            {
                for (int i = rect.Left; i < rect.Left + rect.Width; i++)
                {
                    for (int j = rect.Top; j < rect.Top + rect.Height; j++)
                    {
                        grid[i, j]++;
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (grid[i, j] >= 2)
                        sum++;
            return sum;
        }

        private static string Part2(List<Rectangle> rectangles)
        {
            List<string>[,] grid = new List<string>[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new List<string>();
                }

            foreach (var rect in rectangles)
            {
                for (int i = rect.Left; i < rect.Left + rect.Width; i++)
                {
                    for (int j = rect.Top; j < rect.Top + rect.Height; j++)
                    {
                        grid[i, j].Add(rect.Id);
                    }
                }
            }

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (grid[i,j].Count > 1)
                        foreach (var id in grid[i,j])
                        {
                            var rect = rectangles.FirstOrDefault(r => r.Id == id);
                            if (rect != null)
                                rectangles.Remove(rect);
                        }
                }

            Console.WriteLine(rectangles.Count);
            return rectangles.FirstOrDefault()?.Id;
        }
    }
}
