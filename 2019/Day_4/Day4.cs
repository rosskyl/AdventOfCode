using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2019.Day_4
{
    class Day4
    {
        public static void Run()
        {
            var min = 272091;
            var max = 815432;

            var part1 = Part1(min, max);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(min, max);
            Console.WriteLine($"Part 2: {part2}");

            //var test = "112233";
            //Console.WriteLine($"{test}: {IsValid(test, true)}");
            //test = "123444";
            //Console.WriteLine($"{test}: {IsValid(test, true)}");
            //test = "111122";
            //Console.WriteLine($"{test}: {IsValid(test, true)}");
        }

        private static int Part1(int min, int max)
        {
            var count = 0;
            for (int pass = min; pass <= max; pass++)
            {
                if (IsValid(pass.ToString()))
                    count++;
            }

            return count;
        }

        private static int Part2(int min, int max)
        {
            var count = 0;
            for (int pass = min; pass <= max; pass++)
            {
                if (IsValid(pass.ToString(), true))
                    count++;
            }

            return count;
        }

        private static bool IsValid(string password, bool isPart2 = false)
        {
            var isDouble = false;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                    isDouble = true;

                if (password[i] > password[i + 1])
                    return false;
            }

            if (isPart2)
            {
                var counts = password.AsEnumerable().Distinct().Select(p => password.Count(c => c == p));
                if (!counts.Contains(2))
                    return false;
            }
            
            return isDouble;
        }
    }
}
