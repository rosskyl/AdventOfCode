using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day06
    {
        public static void Run()
        {
            ////sample
            //var times = new List<int>() { 7, 15, 30 };
            //var distances = new List<int>() { 9, 40, 200 };

            //input
            var times = new List<int>() { 54, 81, 70, 88 };
            var distances = new List<int>() { 446, 1292, 1035, 1007 };


            Part1(times, distances);

            Part2(times, distances);
        }

        public static void Part1(List<int> times, List<int> distances)
        {
            long total = 1;

            for (int i = 0; i < times.Count; i++)
            {
                int numWins = 0;
                for (int j = 1; j < times[i]; j++)
                {
                    var distance = (times[i] - j) * j;

                    if (distance > distances[i])
                        numWins++;
                }

                total *= (numWins > 0 ? numWins : 1);
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2(List<int> times, List<int> distances)
        {
            var actualTime = int.Parse(string.Concat(times.Select(t => t.ToString())));
            var actualDistance = long.Parse(string.Concat(distances.Select(d => d.ToString())));

            int numWins = 0;
            for (long j = 1; j < actualTime; j++)
            {
                var distance = (actualTime - j) * j;

                if (distance > actualDistance)
                    numWins++;
            }
            Console.WriteLine($"Part 2: {numWins}");
        }
    }
}
