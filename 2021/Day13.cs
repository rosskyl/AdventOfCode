using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    public static class Day13
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(13);

            List<(int x, int y)> dots = lines.Where(l => !l.StartsWith("fold") && !string.IsNullOrWhiteSpace(l)).Select(l => (int.Parse(l.Split(",")[0]), int.Parse(l.Split(",")[1]))).ToList();
            List<(char axis, int pos)> folds = lines.Where(l => l.StartsWith("fold")).Select(l => (l.Remove(0, 11)[0], int.Parse(l.Remove(0, 11).Split('=')[1]))).ToList();

            var maxX = dots.Select(d => d.x).Max();
            var maxY = dots.Select(d => d.y).Max();

            var grid = new int[maxX + 1, maxY + 1];

            foreach (var dot in dots)
            {
                grid[dot.x, dot.y] = 1;
            }

            //PrintGrid(grid);

            Console.WriteLine();

            foreach (var fold in folds)
            {
                Console.WriteLine($"{fold.axis} = {fold.pos}");
                int[,] originalGrid = arrayCopy(grid);

                for (int i = 0; i < fold.pos; i++)
                {
                    if (fold.axis == 'x')
                    {
                        for (int y = 0; y < originalGrid.GetLength(1); y++)
                        {
                            originalGrid[fold.pos - i - 1, y] += originalGrid[fold.pos + i + 1, y];
                        }
                        grid = arrayCopy(originalGrid, fold.pos);
                    }
                    else
                    {
                        for (int x = 0; x < originalGrid.GetLength(0); x++)
                        {
                            originalGrid[x, fold.pos - i - 1] += originalGrid[x, fold.pos + i + 1];
                        }
                        grid = arrayCopy(originalGrid, 0, fold.pos);
                    }
                }
                //PrintGrid(grid);

                Console.WriteLine($"Part 1: {CountDots(grid)}");
            }

            PrintGrid(grid);

        }

        public static int[,] arrayCopy(int[,] input, int maxX = 0, int maxY = 0)
        {
            maxX = maxX == 0 ? input.GetLength(0) : maxX;
            maxY = maxY == 0 ? input.GetLength(1) : maxY;

            int[,] result = new int[maxX, maxY]; //Create a result array that is the same length as the input array
            for (int x = 0; x < maxX; ++x) //Iterate through the horizontal rows of the two dimensional array
            {
                for (int y = 0; y < maxY; ++y) //Iterate throught the vertical rows, to add more dimensions add another for loop for z
                {
                    result[x, y] = input[x, y]; //Change result x,y to input x,y
                }
            }
            return result;
        }

        private static int CountDots(int[,] grid)
        {
            var sum = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] > 0)
                        sum++;
                }
            }
            return sum;
        }

        private static void PrintGrid(int[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write(grid[x, y] > 0 ? '#' : ' ');
                }
                Console.WriteLine();
            }
        }
    }
}
