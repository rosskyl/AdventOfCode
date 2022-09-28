using System;
using System.Collections.Generic;
using System.Text;

namespace _2021
{
    static class Day2
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(2);
            var lines = ReadInputFile.SplitLines(contents);

            var horizontal = 0;
            var depth1 = 0;
            var depth2 = 0;
            var aim = 0;

            foreach (var line in lines)
            {
                var words = line.Split();
                var shift = int.Parse(words[1]);
                switch (words[0].ToLower())
                {
                    case "up":
                        depth1 -= shift;
                        aim -= shift;
                        break;
                    case "down":
                        depth1 += shift;
                        aim += shift;
                        break;
                    case "forward":
                        horizontal += shift;
                        depth2 += (shift * aim);
                        break;
                }
            }

            Console.WriteLine($"Part 1: {depth1 * horizontal}");
            Console.WriteLine($"Part 2: {depth2 * horizontal}");
        }
    }
}
