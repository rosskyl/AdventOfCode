using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2023
{
    public static class Day03
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(3);
            Part1(lines);

            //lines = ReadInputFile.SplitLines("467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..");
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            long total = 0;

            var regex = new Regex("(\\d+)");
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (Match match in regex.Matches(lines[i]))
                {
                    var isValid = false;
                    if (match.Index > 0 && lines[i][match.Index - 1] != '.')
                    {
                        isValid = true;
                    }
                    else if (match.Index + match.Length < lines[i].Length - 1 && lines[i][match.Index + match.Length] != '.')
                    {
                        isValid = true;
                    }
                    else if (i > 0 && CheckRowForSymbols(lines[i - 1], match))
                    {
                        isValid = true;
                    }
                    else if (i < lines.Length - 1 && CheckRowForSymbols(lines[i + 1], match))
                    {
                        isValid = true;
                    }

                    if (isValid)
                        total += int.Parse(match.Value);
                }
            }

            Console.WriteLine("Part 1: " + total);
        }

        private static bool CheckRowForSymbols(string line, Match match)
        {
            var start = match.Index - 1;
            var end = match.Length + 1;
            if (match.Index > 0 && line.Length > match.Index + match.Length)
                end++;
            return line.Skip(start).Take(end).Any(ch => ch != '.');
        }

        public static void Part2(string[] lines)
        {
            long total = 0;
            var regexGear = new Regex("\\*");
            var regexDigit = new Regex("\\d+");
            var regexDigitRight = new Regex("\\d+", RegexOptions.RightToLeft);
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (Match match in regexGear.Matches(lines[i]))
                {
                    var parts = new List<string>();
                    if (match.Index > 0 && lines[i][match.Index - 1] != '.')
                    {
                        var part = regexDigitRight.Match(string.Concat(lines[i].Take(match.Index))).Value;
                        parts.Add(part);
                    }
                    if (match.Index + match.Length < lines[i].Length - 1 && lines[i][match.Index + match.Length] != '.')
                    {
                        var part = regexDigit.Match(string.Concat(lines[i].Skip(match.Index))).Value;
                        parts.Add(part);
                    }
                    if (i > 0)
                    {
                        parts.AddRange(GetPartAdjacentRow(lines[i - 1], match));
                    }
                    if (i < lines.Length - 1)
                    {
                        parts.AddRange(GetPartAdjacentRow(lines[i + 1], match));
                    }

                    if (parts.Count == 2)
                        total += int.Parse(parts[0]) * int.Parse(parts[1]);
                }
            }

            Console.WriteLine($"Part 2: {total}");
        }



        private static List<string> GetPartAdjacentRow(string line, Match match)
        {
            var parts = new List<string>();

            if (int.TryParse(line[match.Index].ToString(), out int digit))
                parts.Add(GetDigitsFromIndex(line, match.Index));
            else
            {
                if (match.Index > 0 && int.TryParse(line[match.Index - 1].ToString(), out digit))
                    parts.Add(GetDigitsFromIndex(line, match.Index - 1));
                if (match.Index + 1 < line.Length && int.TryParse(line[match.Index + 1].ToString(), out digit))
                    parts.Add(GetDigitsFromIndex(line, match.Index + 1));
            }
            return parts;
        }

        private static string GetDigitsFromIndex(string line, int partIndex)
        {
            var digits = line[partIndex].ToString();

            var index = partIndex - 1;
            while (index >= 0 && int.TryParse(line[index].ToString(), out int digit))
            {
                digits = line[index] + digits;
                index--;
            }
            index = partIndex + 1;
            while (index < line.Length && int.TryParse(line[index].ToString(), out int digit))
            {
                digits = digits + line[index];
                index++;
            }

            return digits;
        }
    }
}
