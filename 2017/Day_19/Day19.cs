using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_19
{
    public static class Day19
    {
        public static void Run()
        {
            string input = ReadInputFile.ReadFile("TestInput.txt", 19);
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 19);
            Part1(actualInput);
        }

        private static void Part1(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);
            int y = 0;
            int x = lines[0].IndexOf('|');

            string lettersSeen = string.Empty;
            long steps = 1;

            int dirY = 1;
            int dirX = 0;

            while (IsValidPoint(lines, x + dirX, y + dirY) && lines[y + dirY][x + dirX] != ' ')
            {
                y += dirY;
                x += dirX;

                if (char.IsLetter(lines[y][x]))
                {
                    lettersSeen = string.Join(string.Empty, lettersSeen, lines[y][x]);
                }
                else if (lines[y][x] == '+')
                {
                    FindNextDirection(lines, ref dirX, ref dirY, x, y);
                }
                steps += 1;
            }

            Console.WriteLine($"Saw the letters: {lettersSeen}, and took {steps} steps");
        }

        private static bool IsValidPoint(string[] lines, int x, int y)
        {
            return y >= 0 && x >= 0 && y < lines.Length && x < lines[y].Length;
        }

        private static void FindNextDirection(string[] lines, ref int dirX, ref int dirY, int x, int y)
        {
            int[][] possibilities = new int[][] 
            {
                new int[] { 0, 1},
                new int[] { 0, -1},
                new int[] { 1, 0},
                new int[] { -1, 0},
            };

            foreach (int[] option in possibilities)
            {
                if ((option[0] != -dirX || option[1] != -dirY)
                    && IsValidPoint(lines, x + option[0], y + option[1]))
                {
                    bool isNext;
                    if (option[0] != 0)
                    {
                        isNext = lines[y + option[1]][x + option[0]] == '-';
                    }
                    else
                    {
                        isNext = lines[y + option[1]][x + option[0]] == '|';
                    }

                    if (isNext || char.IsLetter(lines[y + option[1]][x + option[0]]))
                    {
                        dirX = option[0];
                        dirY = option[1];
                        return;
                    }
                }
            }
        }
    }
}
