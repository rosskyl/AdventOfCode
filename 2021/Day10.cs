using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    public static class Day10
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(10);
            var lines = ReadInputFile.SplitLines(contents);

            var part1 = 0;

            var scores = new List<long>();
            foreach (var line in lines)
            {
                var isCorrupted = IsCorrupted(line, out int score);
                part1 += score;

                if (!isCorrupted)
                {
                    scores.Add(CompleteLine(line));
                }
            }
            
            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {scores.OrderBy(s => s).ToList()[(scores.Count / 2)]}");
        }

        private static Dictionary<char, char> characters = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        private static bool IsCorrupted(string line, out int score)
        {
            var stack = new Stack<char>();
            while (line.Length > 0)
            {
                if (characters.ContainsKey(line[0]))
                {
                    stack.Push(line[0]);
                    line = line[1..];
                }
                else
                {
                    if (line[0] == characters[stack.Peek()])
                    {
                        stack.Pop();
                        line = line[1..];
                    }
                    else
                    {
                        score = GetScoreFromChar(line[0]);
                        return true;
                    }
                }
            }

            score = 0;
            return false;
        }

        private static long CompleteLine(string line)
        {
            long score = 0;

            //Console.Write($"{line}: ");

            var stack = new Stack<char>();
            while (line.Length > 0)
            {
                if (characters.ContainsKey(line[0]))
                {
                    stack.Push(line[0]);
                    line = line[1..];
                }
                else
                {
                    if (line[0] == characters[stack.Peek()])
                    {
                        stack.Pop();
                        line = line[1..];
                    }
                }
            }

            while (stack.Count > 0)
            {
                var character = characters[stack.Pop()];
                //Console.Write(character);
                score *= 5;
                switch (character)
                {
                    case ')':
                        score += 1;
                        break;
                    case ']':
                        score += 2;
                        break;
                    case '}':
                        score += 3;
                        break;
                    case '>':
                        score += 4;
                        break;
                }
            }
            //Console.WriteLine($" - {score}");
            return score;
        }

        private static int GetScoreFromChar(char character)
        {
            switch (character)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }
    }
}
