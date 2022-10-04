using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    public static class Day11
    {
        public static void Run()
        {
            const int NumTurns = 100;

            var contents = ReadInputFile.ReadFile(11);
            var lines = ReadInputFile.SplitLines(contents);

            var grid = new Grid(lines);
            grid.Print("Before any steps:");

            long numFlashes = 0;
            var part2 = -1;
            for (int i = 1; i <= NumTurns; i++)
            {
                numFlashes += grid.RunTurn();
                if (part2 == -1 && grid.WasSynchronous())
                    part2 = i;
                grid.ResetTurn();
                grid.Print($"After step {i}:");
            }
            var day = NumTurns;
            while (part2 == -1)
            {
                day++;
                grid.RunTurn();
                if (grid.WasSynchronous())
                    part2 = day;
                grid.ResetTurn();
            }

            Console.WriteLine($"Part 1: {numFlashes}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private class Grid
        {
            private List<List<int>> _grid;
            private List<(int x, int y)> _flashed = new List<(int x, int y)>();

            public Grid(string[] lines)
            {
                _grid = lines.Select(line => line.Select(ch => int.Parse(ch.ToString())).ToList()).ToList();
            }

            public void Print(string header = null)
            {
                //if (header != null)
                //    Console.WriteLine(header);
                //foreach (var row in _grid)
                //{
                //    Console.WriteLine(string.Join("", row));
                //}
                //Console.WriteLine();
            }

            public int RunTurn()
            {
                for (int x = 0; x < _grid[0].Count; x++)
                {
                    for (int y = 0; y < _grid.Count; y++)
                    {
                        _grid[y][x]++;
                        if (_grid[y][x] > 9 && !_flashed.Contains((x, y)))
                        {
                            Flash(x, y);
                        }
                    }
                }

                foreach ((int x, int y) in _flashed)
                {
                    _grid[y][x] = 0;
                }

                return _flashed.Count;
            }

            public bool WasSynchronous()
            {
                return _flashed.Count == _grid.Select(g => g.Count).Sum();
            }

            public void ResetTurn()
            {
                _flashed.Clear();
            }

            private void Flash(int x, int y)
            {
                _flashed.Add((x, y));

                if (x > 0 && y > 0)
                    if (Increase(x - 1, y - 1))
                        Flash(x - 1, y - 1);
                if (y > 0)
                    if (Increase(x, y - 1))
                        Flash(x, y - 1);
                if (x < _grid[0].Count-1 && y > 0)
                    if (Increase(x + 1, y - 1))
                        Flash(x + 1, y - 1);
                if (x < _grid[0].Count-1)
                    if (Increase(x + 1, y))
                        Flash(x + 1, y);
                if (x < _grid[0].Count-1 && y < _grid.Count-1)
                    if (Increase(x + 1, y + 1))
                        Flash(x + 1, y + 1);
                if (y < _grid.Count-1)
                    if (Increase(x, y + 1))
                        Flash(x, y + 1);
                if (x > 0 && y < _grid.Count-1)
                    if (Increase(x - 1, y + 1))
                        Flash(x - 1, y + 1);
                if (x > 0)
                    if (Increase(x - 1, y))
                        Flash(x - 1, y);
            }

            private bool Increase(int x, int y)
            {
                _grid[y][x]++;
                return _grid[y][x] > 9 && !_flashed.Contains((x, y));
            }
        }
    }
}
