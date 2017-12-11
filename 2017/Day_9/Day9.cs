using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_9
{
    public static class Day9
    {
        public static void Run()
        {
            TestPart1();
            string actualInput = ReadInputFile.ReadFile("Input.txt", 9);
            Part1(actualInput);

            Console.WriteLine();

            TestPart2();
            Part2(actualInput);
        }

        private static void TestPart2()
        {
            string input = "{<>}";
            Part2(input, false);
            Console.WriteLine(" should be 0");

            input = "{<random characters>}";
            Part2(input, false);
            Console.WriteLine(" should be 17");

            input = "{<<<<>}";
            Part2(input, false);
            Console.WriteLine(" should be 3");

            input = "{<{!>}>}";
            Part2(input, false);
            Console.WriteLine(" should be 2");

            input = "{<!!>}";
            Part2(input, false);
            Console.WriteLine(" should be 0");

            input = "{<!!!>>}";
            Part2(input, false);
            Console.WriteLine(" should be 0");

            input = "{<{o\"i!a,<{i<a>}";
            Part2(input, false);
            Console.WriteLine(" should be 10");
        }

        private static void TestPart1()
        {
            string input = "{{{}}}";
            Part1(input, false);
            Console.WriteLine(" Should be 6");

            input = "{{},{}}";
            Part1(input, false);
            Console.WriteLine(" Should be 5");

            input = "{{{},{},{{}}}}";
            Part1(input, false);
            Console.WriteLine(" Should be 16");

            input = "{<a>,<a>,<a>,<a>}";
            Part1(input, false);
            Console.WriteLine(" Should be 1");

            input = "{{<ab>},{<ab>},{<ab>},{<ab>}}";
            Part1(input, false);
            Console.WriteLine(" Should be 9");

            input = "{{<!!>},{<!!>},{<!!>},{<!!>}}";
            Part1(input, false);
            Console.WriteLine(" Should be 9");

            input = "{{<a!>},{<a!>},{<a!>},{<ab>}}";
            Part1(input, false);
            Console.WriteLine(" Should be 3");
        }

        private static void Part1(string input, bool endLine = true)
        {
            input = input.Substring(1);
            Group mainGroup = CreateGroups(ref input);
            if (endLine)
                Console.WriteLine($"Score is {mainGroup.Score()}");
            else
                Console.Write($"Score is {mainGroup.Score()}");
        }

        private static void Part2(string input, bool endLine = true)
        {
            input = input.Substring(1);
            Group mainGroup = CreateGroups(ref input);
            if (endLine)
                Console.WriteLine($"Total Garbage is {mainGroup.TotalGarbage()}");
            else
                Console.Write($"Total Garbage is {mainGroup.TotalGarbage()}");
        }

        private static Group CreateGroups(ref string input)
        {
            Group mainGroup = new Group();

            int i = 0;
            while (i < input.Length)
            {
                switch (input[i])
                {
                    case '{':
                        input = input.Substring(i + 1);
                        mainGroup.Children.Add(CreateGroups(ref input));
                        i = 0;
                        break;
                    case '}':
                        input = input.Substring(i + 1);
                        return mainGroup;
                    case '<':
                        i += 1;
                        while (i < input.Length && input[i] != '>')
                        {
                            if (input[i] == '!')
                                i += 2;
                            else
                            {
                                i += 1;
                                mainGroup.Garbage += 1;
                            }
                        }
                        i += 1;
                        break;
                    case '!':
                        i += 2;
                        break;
                    default:
                        i += 1;
                        break;
                }
            }

            return null;
        }
    }
}
