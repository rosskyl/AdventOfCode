using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2019.Day_3
{
    class Day3
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(3);
            //contents = "R8,U5,L5,D3\nU7,R6,D4,L4";
            //contents = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83";
            //contents = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            var lines = ReadInputFile.SplitLines(contents);

            var part1 = Part1(lines);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(lines);
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(string[] paths)
        {
            var grid = new Dictionary<(int x, int y), (bool Wire1, bool Wire2)>();
            RunWirePart1(grid, paths[0], 1);
            RunWirePart1(grid, paths[1], 2);

            var intersections = grid.Where(kv => kv.Value.Wire1 && kv.Value.Wire2).Select(kv => kv.Key);
            var closest = intersections.Min(xy => Math.Abs(xy.x) + Math.Abs(xy.y));
            return closest;
        }

        private static int Part2(string[] paths)
        {
            var grid = new Dictionary<(int x, int y), (int Wire1, int Wire2)>();

            RunWirePart2(grid, paths[0], 1);
            RunWirePart2(grid, paths[1], 2);

            var intersections = grid.Where(kv => kv.Value.Wire1 != 0 && kv.Value.Wire2 != 0).Select(kv => kv.Value);
            var closest = intersections.Min(steps => Math.Abs(steps.Wire1) + Math.Abs(steps.Wire2));
            return closest;
        }

        private static void RunWirePart2(Dictionary<(int x, int y), (int Wire1, int Wire2)> grid, string path, int wire)
        {
            var x = 0;
            var y = 0;
            var steps = 0;
            foreach (var instruction in path.Split(','))
            {
                var count = int.Parse(instruction.Remove(0, 1));

                for (int i = 1; i <= count; i++)
                {
                    steps++;
                    if (instruction[0] == 'R')
                        x++;
                    else if (instruction[0] == 'L')
                        x--;
                    else if (instruction[0] == 'U')
                        y++;
                    else if (instruction[0] == 'D')
                        y--;

                    if (grid.ContainsKey((x, y)))
                    {
                        if (wire == 1 && grid[(x,y)].Wire1 == 0)
                            grid[(x, y)] = (Wire1: steps, Wire2: grid[(x, y)].Wire2);
                        else if (grid[(x, y)].Wire2 == 0)
                            grid[(x, y)] = (Wire1: grid[(x, y)].Wire1, Wire2: steps);
                    }
                    else
                    {

                        if (wire == 1)
                            grid[(x, y)] = (Wire1: steps, Wire2: 0);
                        else
                            grid[(x, y)] = (Wire1: 0, Wire2: steps);
                    }
                }
            }
        }

        private static void RunWirePart1(Dictionary<(int x, int y), (bool Wire1, bool Wire2)> grid, string path, int wire)
        {
            var x = 0;
            var y = 0;
            foreach (var instruction in path.Split(','))
            {
                var count = int.Parse(instruction.Remove(0, 1));

                for (int i = 1; i <= count; i++)
                {
                    if (instruction[0] == 'R')
                        x++;
                    else if (instruction[0] == 'L')
                        x--;
                    else if (instruction[0] == 'U')
                        y++;
                    else if (instruction[0] == 'D')
                        y--;

                    if (grid.ContainsKey((x, y)))
                    {
                        if (wire == 1)
                            grid[(x, y)] = (Wire1: true, Wire2: grid[(x, y)].Wire2);
                        else
                            grid[(x, y)] = (Wire1: grid[(x, y)].Wire1, Wire2: true);
                    }
                    else
                    {

                        if (wire == 1)
                            grid[(x, y)] = (Wire1: true, Wire2: false);
                        else
                            grid[(x, y)] = (Wire1: false, Wire2: true);
                    }
                }
            }
        }
    }
}
