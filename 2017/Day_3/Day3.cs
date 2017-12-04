using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_3
{
    public static class Day3
    {
        public static void Run()
        {
            int actualInput = 361527;
            Part1(1);
            Part1(12);
            Part1(23);
            Part1(1024);

            Console.WriteLine();
            Part1(actualInput);

            for (int i = 1; i < 61; i++)
            {
                Part2(i);
            }

            Console.WriteLine();
            Part2(actualInput + 1);
        }

        private static void Part1(int target)
        {
            Coordinate(target, out int x, out int y);

            Console.WriteLine($"{target}: {Math.Abs(x) + Math.Abs(y)}");
        }

        private static void Part2(int target)
        {
            Dictionary<Point, int> points = new Dictionary<Point, int>();
            points.Add(new Point(0, 0), 1);
            Point next = new Point(1, 0);
            points.Add(next, 1);

            int value = 1;
            while (value < target)
            {
                next = next.Increment();
                value = CalculateValue(points, next);
                points.Add(next, value);
            }

            Console.WriteLine($"{value}: {next.X}, {next.Y}: {Math.Abs(next.X) + Math.Abs(next.Y)}");
        }

        private static int CalculateValue(Dictionary<Point, int> points, Point next)
        {
            int[,] combos = new int[8, 2] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                if (points.TryGetValue(new Point(next.X + combos[i,0], next.Y + combos[i, 1]), out int value))
                    {
                    sum += value;
                }
            }

            return sum;
        }

        private static void Coordinate(int target, out int x, out int y)
        {
            double k = Math.Ceiling((Math.Sqrt(target) - 1) / 2);
            double t = 2 * k + 1;
            double m = t * t;

            t = t - 1;

            if (target >= m - t)
            {
                x = (int)(k - (m - target));
                y = (int)(-k);
                return;
            }
            else
                m = m - t;

            if (target >= m - t)
            {
                x = (int)(-k);
                y = (int)(-k + (m - target));
                return;
            }
            else
                m = m - t;

            if (target >= m - t)
            {
                x = (int)(-k + (m - target));
                y = (int)k;
            }
            else
            {
                x = (int)k;
                y = (int)(k - (m - target - t));
            }
        }
    }
}
