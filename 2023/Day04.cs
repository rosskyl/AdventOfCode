using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day04
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(4);
            Part1(lines);

            //lines = ReadInputFile.SplitLines("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11");
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            long total = 0;

            var regex = new Regex("Card\\s+\\d+:\\s+((?:\\d+\\s+)+(?:\\d+))\\s+\\|\\s+((?:\\d+\\s+)+(?:\\d+))");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                var winningNumbers = match.Groups[1].Value.Split(' ').Where(num => !string.IsNullOrEmpty(num));
                var gameNumbers = match.Groups[2].Value.Split(' ').Where(num => !string.IsNullOrEmpty(num));

                var matches = gameNumbers.Count(num => winningNumbers.Contains(num));
                if (matches > 0)
                    total += (long)Math.Pow(2, matches - 1);
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2(string[] lines)
        {
            var regex = new Regex("Card\\s+\\d+:\\s+((?:\\d+\\s+)+(?:\\d+))\\s+\\|\\s+((?:\\d+\\s+)+(?:\\d+))");
            var cards = lines.Select((line, index) => index).ToDictionary(index => index, index => 1);
            for (int i = 0; i < cards.Count; i++)
            {
                var match = regex.Match(lines[i]);
                var winningNumbers = match.Groups[1].Value.Split(' ').Where(num => !string.IsNullOrEmpty(num));
                var gameNumbers = match.Groups[2].Value.Split(' ').Where(num => !string.IsNullOrEmpty(num));

                var matches = gameNumbers.Count(num => winningNumbers.Contains(num));

                var increment = cards[i];
                for (int j = 1; j <= matches; j++)
                    cards[i + j] += increment;
            }

            var total = cards.Values.Aggregate((a, x) => a + x);
            Console.WriteLine($"Part 2: {total}");
        }
    }
}
