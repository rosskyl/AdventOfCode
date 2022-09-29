using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day5
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(5);
            var lines = ReadInputFile.SplitLines(contents);

            const int max = 1000;
            var grid1 = new int[max, max];
            var grid2 = new int[max, max];

            foreach (var line in lines)
            {
                var coords = line.Split(" -> ");
                var coord1 = coords[0].Split(",");
                var coord2 = coords[1].Split(",");

                if (coord1[0] == coord2[0])
                {
                    for (int i = Math.Min(int.Parse(coord1[1]), int.Parse(coord2[1])); i <= Math.Max(int.Parse(coord1[1]), int.Parse(coord2[1])); i++)
                    {
                        grid1[int.Parse(coord1[0]), i]++;
                        grid2[int.Parse(coord1[0]), i]++;
                    }
                }
                else if (coord1[1] == coord2[1])
                {
                    for (int i = Math.Min(int.Parse(coord1[0]), int.Parse(coord2[0])); i <= Math.Max(int.Parse(coord1[0]), int.Parse(coord2[0])); i++)
                    {
                        grid1[i, int.Parse(coord1[1])]++;
                        grid2[i, int.Parse(coord1[1])]++;
                    }
                }
                else
                {
                    var xChange = int.Parse(coord2[0]) - int.Parse(coord1[0]);
                    var yChange = int.Parse(coord2[1]) - int.Parse(coord1[1]);
                    if (Math.Abs(xChange) != Math.Abs(yChange))
                    {
                        Console.WriteLine("Problem");
                    }

                    for (int i = 0; i <= Math.Abs(xChange); i++)
                    {
                        var x = int.Parse(coord1[0]) + (Math.Sign(xChange) * i);
                        var y = int.Parse(coord1[1]) + (Math.Sign(yChange) * i);
                        grid2[x, y]++;
                    }
                }
            }

            var overlap1 = 0;
            var overlap2 = 0;
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (grid1[i, j] >= 2)
                        overlap1++;
                    if (grid2[i, j] >= 2)
                        overlap2++;
                    //Console.Write($"{grid2[j, i] }");
                }
                //Console.WriteLine();
            }



            Console.WriteLine($"Part 1: {overlap1}");
            Console.WriteLine($"Part 2: {overlap2}");
        }
    }
}
