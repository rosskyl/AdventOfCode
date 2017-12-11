using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_10
{
    public static class Day10
    {
        public static void Run()
        {
            TestPart1();

            string actualInput = "83,0,193,1,254,237,187,40,88,27,2,255,149,29,42,100";
            Part1(actualInput);

            Console.WriteLine();
            TestPart2();

            Console.WriteLine();
            Part2(actualInput);
        }

        private static void TestPart1()
        {
            string input = "3,4,1,5";
            Part1(input, false, 5);
            Console.WriteLine(" should be 12");
        }

        private static void TestPart2()
        {
            string input = string.Empty;
            Part2(input);
            Console.WriteLine("Should be     a2582a3a0e66e6e86e3812dcb672a272");

            input = "AoC 2017";
            Part2(input);
            Console.WriteLine("Should be     33efeb34ea91902bb2f59c9920caa6cd");

            input = "1,2,3";
            Part2(input);
            Console.WriteLine("Should be     3efbe78a8d82f29979031a4aa0b16a9d");

            input = "1,2,4";
            Part2(input);
            Console.WriteLine("Should be     63960835bcdc130f0b66d7ff4f6a5a8e");
        }

        private static void Part1(string input, bool endLine = true, int listLength = 256)
        {
            int index = 0;
            int skip = 0;
            List<int> list = GenerateList(listLength);
            int[] lengths = ParseInput1(input);

            KnotHashRound(ref index, ref skip, list, lengths);

            string output = $"The result is {list[0]} * {list[1]} = {list[0] * list[1]}";
            if (endLine)
                Console.WriteLine(output);
            else
                Console.Write(output);
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

        private static void Part2(string input, bool endLine = true, int listLength = 256)
        {
            int index = 0;
            int skip = 0;
            List<int> list = GenerateList(listLength);
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

            Console.WriteLine($"The result is {hash}");
        }

        private static List<int> GenerateList(int size)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
                list.Add(i);

            return list;
        }

        private static int[] ParseInput1(string input)
        {
            string[] splitInput = input.Split(',');

            return splitInput.Select(s => int.Parse(string.Concat(s.Where(c => char.IsDigit(c))))).ToArray();
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
