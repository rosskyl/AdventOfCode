using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day02
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(2);
            Console.WriteLine("Day 02");
            Part1(lines);

            //lines = ReadInputFile.SplitLines("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green");
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            const int red = 12;
            const int green = 13;
            const int blue = 14;
            var availableColors = new Dictionary<string, int>()
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            var regexGame = new Regex("Game (\\d+):");
            var regexRound = new Regex("((?: \\d+ [a-zA-Z]+(?:,)?)+;*)");
            var regexColor = new Regex("(\\d+) ([a-zA-Z]+)+");
            var total = 0;
            foreach (var line in lines)
            {
                var game = regexGame.Match(line).Groups[1].Value;
                var isValid = true;
                foreach (Match matchRound in regexRound.Matches(line))
                {
                    foreach (Match match in regexColor.Matches(matchRound.Value))
                    {
                        var color = match.Groups[2].Value;
                        var digit = int.Parse(match.Groups[1].Value);
                        if (availableColors[color] < digit)
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (!isValid)
                        break;
                }

                if (isValid)
                    total += int.Parse(game);
            }

            Console.WriteLine("Part 1: " + total);
        }
        
        public static void Part2(string[] lines)
        {
            var regexGame = new Regex("Game (\\d+):");
            var regexRound = new Regex("((?: \\d+ [a-zA-Z]+(?:,)?)+;*)");
            var regexColor = new Regex("(\\d+) ([a-zA-Z]+)+");
            long total = 0;
            foreach (var line in lines)
            {
                var game = regexGame.Match(line).Groups[1].Value;
                var colors = new Dictionary<string, int>()
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                foreach (Match matchRound in regexRound.Matches(line))
                {
                    foreach (Match match in regexColor.Matches(matchRound.Value))
                    {
                        var color = match.Groups[2].Value;
                        var digit = int.Parse(match.Groups[1].Value);
                        if (colors[color] < digit)
                            colors[color] = digit;
                    }
                }

                var power = colors.Values.Aggregate((a, x) => a * x);
                total += power;
            }

            Console.WriteLine("Part 2: " + total);
        }
    }
}
