using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day7
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(7);
            var positions = contents.Split(",").Select(p => int.Parse(p));

            var min = positions.Min();
            var max = positions.Max();

            var lowestPosition = 0;
            long lowestFuel = long.MaxValue;
            for (int i = min; i <= max; i++)
            {
                var fuel = positions.Select(p => (long)Math.Abs(p - i)).Sum();
                if (fuel < lowestFuel)
                {
                    lowestFuel = fuel;
                    lowestPosition = i;
                }
            }


            //Need to check both round up and round down from average
            var averageHigh = Math.Ceiling(positions.Average());
            var fuelUsedHigh = positions.Select(p =>
            {
                var diff = (long)Math.Abs(averageHigh - p);
                return ((diff * diff) + diff) / 2;
            }).Sum();

            var averageLow = Math.Floor(positions.Average());
            var fuelUsedLow = positions.Select(p =>
            {
                var diff = (long)Math.Abs(averageLow - p);
                return ((diff * diff) + diff) / 2;
            }).Sum();

            Console.WriteLine($"Part 1: {lowestFuel}");
            Console.WriteLine($"Part 2: {Math.Min(fuelUsedLow, fuelUsedHigh)}");
        }
    }
}
