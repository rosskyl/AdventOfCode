using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day01
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(1);
            Part1(lines);

            //lines = ReadInputFile.SplitLines("two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen");
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            int total = 0;
            var regexFirst = new Regex("\\d");
            var regexLast = new Regex("\\d", RegexOptions.RightToLeft);
            foreach (var line in lines)
            {
                var first = regexFirst.Match(line);
                var last = regexLast.Match(line);
                var digits = first.Value + last.Value;
                total += int.Parse(digits);
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2(string[] lines)
        {
            int total = 0;
            var dictionary = new Dictionary<string, string>()
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };
            var regexFirst = new Regex("\\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
            var regexLast = new Regex("\\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)", RegexOptions.RightToLeft);
            foreach (var line in lines)
            {
                var first = regexFirst.Match(line);
                var last = regexLast.Match(line);
                var digits = (dictionary.ContainsKey(first.Value) ? dictionary[first.Value] : first.Value)
                    + (dictionary.ContainsKey(last.Value) ? dictionary[last.Value] : last.Value);
                
                total += int.Parse(digits);
            }

            Console.WriteLine($"Part 1: {total}");
        }
    }
}
