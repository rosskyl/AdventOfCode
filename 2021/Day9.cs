using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day9
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(9);
            var lines = ReadInputFile.SplitLines(contents);

            var grid = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            var lowPoints = new List<(int, int)>();
            var riskScore = 0;

            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    var current = grid[y][x];

                    if (y > 0 && grid[y - 1][x] <= current)
                        continue;
                    if (y < grid.Length - 1 && grid[y + 1][x] <= current)
                        continue;
                    if (x > 0 && grid[y][x - 1] <= current)
                        continue;
                    if (x < grid[0].Length - 1 && grid[y][x + 1] <= current)
                        continue;

                    lowPoints.Add((x, y));
                    riskScore += current + 1;
                }
            }

            var part2 = Part2(grid, lowPoints);

            Console.WriteLine($"Part 1: {riskScore}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part2(int[][] grid, IEnumerable<(int x, int y)> lowPoints)
        {
            var basins = new List<int>();

            foreach (var point in lowPoints)
            {
                var coords = FindHigherNeighbors(grid, point.x, point.y);
                basins.Add(coords.Count);
            }



            return basins.OrderByDescending(b => b).Take(3).Aggregate((result, item) => result * item); 
        }

        private static List<(int, int)> FindHigherNeighbors(int[][] grid, int x, int y)
        {
            var higherCoords = new List<(int, int)> { (x, y) };
            var current = grid[y][x];

            if (y > 0 && grid[y - 1][x] > current && grid[y - 1][x] != 9)
            {
                higherCoords.Add((x, y - 1));
                higherCoords.AddRange(FindHigherNeighbors(grid, x, y - 1));
            }
            if (y < grid.Length - 1 && grid[y + 1][x] > current && grid[y + 1][x] != 9)
            {
                higherCoords.Add((x, y + 1));
                higherCoords.AddRange(FindHigherNeighbors(grid, x, y + 1));
            }
            if (x > 0 && grid[y][x - 1] > current && grid[y][x - 1] != 9)
            {
                higherCoords.Add((x - 1, y));
                higherCoords.AddRange(FindHigherNeighbors(grid, x - 1, y));
            }
            if (x < grid[0].Length - 1 && grid[y][x + 1] > current && grid[y][x + 1] != 9)
            {
                higherCoords.Add((x + 1, y));
                higherCoords.AddRange(FindHigherNeighbors(grid, x + 1, y));
            }

            //Add 1 for low point
            return higherCoords.Distinct().ToList();
        }
    }
}
