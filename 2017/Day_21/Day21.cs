using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_21
{
    public static class Day21
    {
        private static Dictionary<string, List<string>> transformationsCache = new Dictionary<string, List<string>>();


        public static void Run()
        {
            string input = "../.# => ##./#../...\n.#./..#/### => #..#/..../..../#..#";
            Part1(input, 2);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 21);
            Part1(actualInput);

            Part1(actualInput, 18);
        }

        private static void Part1(string input, int iterations = 5)
        {
            Dictionary<string, string> rules = ParseInput(input);
            List<string> grid = new List<string>() { ".#.", "..#", "###" };

            for (int i = 0; i < iterations; i++)
            {
                List<List<string>> newGrid = new List<List<string>>();

                int breakSize;
                if (grid.Count % 2 == 0)
                    breakSize = 2;
                else
                    breakSize = 3;

                for (int j = 0; j < grid.Count / breakSize; j++)
                {
                    newGrid.Add(new List<string>());
                    for (int k = 0; k < grid.Count / breakSize; k++)
                    {
                        string square = string.Join("/", grid.Skip(j * breakSize).Take(breakSize).Select(l => string.Join(string.Empty, l.Skip(k * breakSize).Take(breakSize))));
                        square = Transform(square, rules);

                        newGrid[j].Add(square);
                    }
                }

                grid = Flatten(newGrid);
            }

            int pixelsOn = grid.Sum(l => l.Sum(c => c == '#' ? 1 : 0));

            Console.WriteLine($"There are {pixelsOn} pixels on after {iterations} iterations");
        }

        private static List<string> Flatten(List<List<string>> grid)
        {
            List<string> flattenedGrid = new List<string>();

            for (int i = 0; i < grid.Count; i++)
            {
                List<string> linesToAdd = new List<string>();
                foreach (string square in grid[i])
                {
                    string[] splitSquare = square.Split('/');

                    for (int j = 0; j < splitSquare.Length; j++)
                    {
                        if (linesToAdd.Count <= j)
                        {
                            linesToAdd.Add(splitSquare[j]);
                        }
                        else
                        {
                            linesToAdd[j] = string.Concat(linesToAdd[j], splitSquare[j]);
                        }
                    }
                }
                flattenedGrid.AddRange(linesToAdd);
            }

            return flattenedGrid;
        }

        private static string Transform(string square, Dictionary<string, string> rules)
        {
            foreach (string key in rules.Keys.Where(k => k.Length == square.Length))
            {
                if (Transformations(key).Contains(square))
                    return rules[key];
            }
            throw new Exception("Error");
        }

        private static List<string> Transformations(string pattern)
        {
            if (transformationsCache.Keys.Contains(pattern))
                return transformationsCache[pattern];
            else
            {
                List<string> transformations = new List<string>();

                transformations.Add(pattern);
                transformations.Add(Rotate(pattern, "cw"));
                transformations.Add(Rotate(pattern, "ccw"));
                transformations.Add(Rotate(Rotate(pattern, "cw"), "cw"));
                transformations.Add(Rotate(Rotate(pattern, "ccw"), "ccw"));
                transformations.Add(Reverse(pattern));
                transformations.Add(Flip(Reverse(pattern)));
                transformations.Add(Rotate(Flip(pattern), "ccw"));
                transformations.Add(Rotate(Reverse(pattern), "cw"));
                transformations.Add(Flip(pattern));
                transformations.Add(Reverse(Flip(pattern)));
                transformations.Add(Rotate(Flip(pattern), "cw"));
                transformations.Add(Rotate(Reverse(pattern), "ccw"));
                transformations.Add(Flip(Rotate(pattern, "cw")));
                transformations.Add(Flip(Rotate(pattern, "ccw")));
                transformations.Add(Reverse(Rotate(pattern, "cw")));
                transformations.Add(Reverse(Rotate(pattern, "ccw")));

                transformationsCache.Add(pattern, transformations);

                return transformations;
            }
        }

        private static string Rotate(string pattern, string direction)
        {
            string[] splitPattern = pattern.Split('/');

            if (direction == "cw")
            {
                splitPattern = splitPattern.Reverse().ToArray();

                string newString = string.Empty;

                for (int i = 0; i < splitPattern.Length; i++)
                {
                    newString = string.Concat(newString, '/', string.Join(string.Empty, splitPattern.Select(p => p[i])));
                }

                return newString.Substring(1);
            }
            else
            {
                string newString = string.Empty;

                for (int i = splitPattern.Length - 1; i >= 0; i--)
                {
                    newString = string.Concat(newString, '/', string.Join(string.Empty, splitPattern.Select(p => p[i])));
                }

                return newString.Substring(1);
            }
        }

        private static string Reverse(string pattern)
        {
            string[] splitPattern = pattern.Split('/');

            string newString = string.Join("/", splitPattern.Select(p => string.Join(string.Empty, p.Reverse())));

            return newString;
        }

        private static string Flip(string pattern)
        {
            string[] splitPattern = pattern.Split('/');

            return string.Join("/", splitPattern.Reverse());
        }

        private static Dictionary<string, string> ParseInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);
            Dictionary<string, string> rules = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split('=');
                string key = splitLine[0].Substring(0, splitLine[0].Length - 1);
                string value = splitLine[1].Substring(2);

                rules.Add(key, value);
            }

            return rules;
        }
    }
}
