using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_4
{
    public class Day4
    {
        internal static void Run()
        {
            string actualInput = ReadInputFile.ReadFile("input.txt", 4);

            string input = "aa bb cc dd ee\naa bb cc dd aa\naa bb cc dd aaa";
            Part1(input);

            Part1(actualInput);

            Console.WriteLine();
            input = "abcde fghij\nabcde xyz ecdab\n a ab abc abd abf abj\n iiii oiii ooii oooi oooo\n oiii ioii iioi iiio";
            Part2(input);

            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            int sum = 0;
            string[] lines = input.Split('\n');
            foreach (string line in lines)
            {
                string parsedLine = line.Replace("\r", "");
                if (IsValid1(parsedLine))
                    sum += 1;
            }

            Console.WriteLine($"sum: {sum}");
        }

        private static void Part2(string input)
        {
            int sum = 0;
            string[] lines = input.Split('\n');
            foreach (string line in lines)
            {
                string parsedLine = line.Replace("\r", "");
                if (IsValid1(parsedLine) && IsValid2(parsedLine))
                    sum += 1;
            }

            Console.WriteLine($"sum: {sum}");
        }

        private static bool IsValid1(string input)
        {
            string[] splitInput = input.Split(' ');
            foreach (string item in splitInput)
            {
                if (splitInput.Count(c => c == item) > 1)
                    return false;
            }
            return true;
        }

        private static bool IsValid2(string input)
        {
            string[] splitInput = input.Split(' ');
            foreach (string item in splitInput)
            {
                foreach (string otherItem in splitInput)
                {
                    if (item != otherItem)
                        if (IsAnagram(item, otherItem))
                            return false;
                }
            }
            return true;
        }

        private static bool IsAnagram(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                return false;

            char[] letters1 = word1.ToArray();
            char[] letters2 = word2.ToArray();
            foreach (char letter in letters1)
            {
                if (letters2.Count(l => l == letter) != letters1.Count(l => l == letter))
                    return false;
            }
            return true;
        }
    }
}
