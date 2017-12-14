using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_14
{
    public static class Day14
    {
        public static void Run()
        {
            string input = "flqrgnkx";
            Part1(input);

            string actualInput = "nbysizxe";
            Part1(actualInput);

            Console.WriteLine();
            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            int usedSquares = 0;
            for (int i = 0; i < 128; i++)
            {
                string rowKey = $"{input}-{i}";
                string hash = KnotHash(rowKey);
                string binary = HexToBinary(hash);
                usedSquares += binary.Where(d => d == '1').Count();
            }

            Console.WriteLine($"Used squares: {usedSquares}");
        }

        private static void Part2(string input)
        {
            List<List<string>> grid = new List<List<string>>();

            for (int i = 0; i < 128; i++)
            {
                string rowKey = $"{input}-{i}";
                string binary = HexToBinary(KnotHash(rowKey));
                grid.Add(binary.Replace('1', '#').Replace('0', '.').Select(c => c.ToString()).ToList());
            }

            List<int> hashes = new List<int>();

            int[][] adjacentOptions = new int[][] {
                new int[] { 0, 1 },
                new int[] { 1, 0 },
                new int[] { 0, -1 },
                new int[] { -1, 0 }
            };

            int groupNumber = 1;
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == "#")
                    {
                        if (!hashes.Contains($"{x},{y}".GetHashCode()))
                        {
                            hashes.Add($"{x},{y}".GetHashCode());
                            List<int[]> groupsToAdd = new List<int[]>() { new int[] { x, y } };

                            while (groupsToAdd.Count > 0)
                            {
                                int currentX = groupsToAdd[0][0];
                                int currentY = groupsToAdd[0][1];

                                grid[currentY][currentX] = groupNumber.ToString();
                                groupsToAdd.RemoveAt(0);
                                foreach (int[] option in adjacentOptions)
                                {
                                    int checkX = currentX + option[0];
                                    int checkY = currentY + option[1];
                                    if (checkX >= 0 && checkX < grid[y].Count
                                        && checkY >= 0 && checkY < grid.Count)
                                    {
                                        if (grid[checkY][checkX] == "#")
                                            groupsToAdd.Add(new int[] { checkX, checkY });
                                    }
                                }
                            }

                            groupNumber++;
                        }
                    }
                }
            }

            Console.WriteLine($"There are {groupNumber - 1} groups");
        }

        private static string HexToBinary(string hex)
        {
            return string.Join(string.Empty, hex.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }

        private static string KnotHash(string input)
        {
            int index = 0;
            int skip = 0;
            List<int> list = GenerateList(256);
            int[] lengths = ParseInput2(input);

            for (int i = 0; i < 64; i++)
                KnotHashRound(ref index, ref skip, list, lengths);

            string hash = string.Empty;
            while (list.Count > 0)
            {
                int total = 0;
                for (int j = 0; j < 16; j++)
                {
                    total = total ^ list[j];
                }
                string hashValue = total.ToString("X");
                if (hashValue.Length == 1)
                    hashValue = $"0{hashValue}";

                hash = $"{hash}{hashValue.ToLower()}";
                list = list.Skip(16).ToList();
            }

            return hash;
        }

        private static void KnotHashRound(ref int index, ref int skip, List<int> list, int[] lengths)
        {
            foreach (int length in lengths)
            {
                List<int> portion;
                if (list.Count > index + length)
                    portion = list.GetRange(index, length);
                else
                {
                    portion = list.Skip(index).ToList();
                    portion.AddRange(list.Take(length - portion.Count));
                }
                portion.Reverse();
                for (int i = 0; i < portion.Count; i++)
                {
                    if (list.Count > i + index)
                        list[i + index] = portion[i];
                    else
                    {
                        int actualIndex = i + index - list.Count;
                        list[actualIndex] = portion[i];
                    }
                }
                index += length + skip;
                while (index > list.Count)
                    index -= list.Count;
                skip += 1;
            }
        }

        private static List<int> GenerateList(int size)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
                list.Add(i);

            return list;
        }

        private static int[] ParseInput2(string input)
        {
            List<int> numbers = new List<int>();
            foreach (char c in input)
            {
                numbers.Add((int)c);
            }
            numbers.Add(17);
            numbers.Add(31);
            numbers.Add(73);
            numbers.Add(47);
            numbers.Add(23);

            return numbers.ToArray();
        }
    }
}
