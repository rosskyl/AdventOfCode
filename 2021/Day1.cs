using System;
using System.Collections.Generic;
using System.Text;

namespace _2021
{
    static class Day1
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(1);
            var lines = ReadInputFile.SplitLines(contents);

            var numIncreased1 = 0;
            var numIncreased2 = 0;
            var previous = int.MaxValue;
            var previous2 = int.MaxValue;
            var previous3 = int.MaxValue;
            foreach (var num in lines)
            {
                if (int.Parse(num) > previous)
                    numIncreased1++;

                //Don't care about previous and previous 2 as they're on both sides
                if (int.Parse(num) > previous3)
                    numIncreased2++;

                previous3 = previous2;
                previous2 = previous;
                previous = int.Parse(num);
            }

            Console.WriteLine($"Part 1: {numIncreased1}");
            Console.WriteLine($"Part 2: {numIncreased2}");
        }
    }
}
