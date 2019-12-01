using System;
using System.Collections.Generic;
using System.Text;

namespace _2019.Day1
{
    class Day1
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(1);
            var lines = ReadInputFile.SplitLines(contents);

            long sum1 = 0;
            long sum2 = 0;
            foreach (var line in lines)
            {
                if (int.TryParse(line, out int mass))
                {
                    int fuel1 = Part1(mass);
                    sum1 += fuel1;

                    int fuel2 = Part2(mass);
                    sum2 += fuel2;
                }
            }
            Console.WriteLine($"Part 1: {sum1}");
            Console.WriteLine($"Part 2: {sum2}");

        }

        private static int Part1(int mass)
        {
            mass /= 3;
            mass -= 2;
            return mass;
        }

        private static int Part2(int mass)
        {
            int fuel = 0;
            while (mass >= 0)
            {
                mass /= 3;
                mass -= 2;

                if (mass > 0)
                    fuel += mass;
            }

            return fuel;
        }
    }
}
