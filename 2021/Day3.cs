using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day3
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(3);
            var lines = ReadInputFile.SplitLines(contents);

            var bits = new List<int[]> { };


            for (int i = 0; i < lines[0].Length; i++)
                bits.Add(new int[] { 0, 0 });

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    bits[i][int.Parse(line[i].ToString())]++;
                }
            }

            var gamma = "";
            var epsilon = "";
            foreach (var bit in bits)
            {
                if (bit[0] > bit[1])
                {
                    gamma += "0";
                    epsilon += "1";
                }
                else
                {
                    gamma += "1";
                    epsilon += "0";
                }
            }

            var oxygenItems = lines;
            var co2Items = lines;

            var position = 0;
            while (oxygenItems.Length > 1 && position < lines[0].Length)
            {
                var bit = FindMostCommonBit(oxygenItems, position);
                oxygenItems = oxygenItems.Where(item => item[position] == bit).ToArray();
                position++;
            }

            position = 0;
            while (co2Items.Length > 1 && position < lines[0].Length)
            {
                var bit = FindMostCommonBit(co2Items, position);
                co2Items = co2Items.Where(item => item[position] != bit).ToArray();
                position++;
            }

            Console.WriteLine($"Part 1: {Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)}");
            Console.WriteLine($"Part 2: {Convert.ToInt32(oxygenItems[0], 2) * Convert.ToInt32(co2Items[0], 2)}");
        }

        private static char FindMostCommonBit(string[] lines, int position)
        {
            var bits = new int[] { 0, 0};

            foreach (var line in lines)
            {
                bits[int.Parse(line[position].ToString())]++;
            }

            if (bits[0] > bits[1])
                return '0';
            else
                return '1';
        }
    }
}
