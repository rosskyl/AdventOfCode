using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_2
{
    public static class Day2
    {
        public static void Run()
        {
            string input = "5 1 9 5\n7 5 3\n2 4 6 8";
            Part1(input);

            string actualInput = "5806 6444 1281 38 267 1835 223 4912 5995 230 4395 2986 6048 4719 216 1201"
                                    + "\n74 127 226 84 174 280 94 159 198 305 124 106 205 99 177 294"
                                    + "\n1332 52 54 655 56 170 843 707 1273 1163 89 23 43 1300 1383 1229"
                                    + "\n5653 236 1944 3807 5356 246 222 1999 4872 206 5265 5397 5220 5538 286 917"
                                    + "\n3512 3132 2826 3664 2814 549 3408 3384 142 120 160 114 1395 2074 1816 2357"
                                    + "\n100 2000 112 103 2122 113 92 522 1650 929 1281 2286 2259 1068 1089 651"
                                    + "\n646 490 297 60 424 234 48 491 245 523 229 189 174 627 441 598"
                                    + "\n2321 555 2413 2378 157 27 194 2512 117 140 2287 277 2635 1374 1496 1698"
                                    + "\n101 1177 104 89 542 2033 1724 1197 474 1041 1803 770 87 1869 1183 553"
                                    + "\n1393 92 105 1395 1000 85 391 1360 1529 1367 1063 688 642 102 999 638"
                                    + "\n4627 223 188 5529 2406 4980 2384 2024 4610 279 249 2331 4660 4350 3264 242"
                                    + "\n769 779 502 75 1105 53 55 931 1056 1195 65 292 1234 1164 678 1032"
                                    + "\n2554 75 4406 484 2285 226 5666 245 4972 3739 5185 1543 230 236 3621 5387"
                                    + "\n826 4028 4274 163 5303 4610 145 5779 157 4994 5053 186 5060 3082 2186 4882"
                                    + "\n588 345 67 286 743 54 802 776 29 44 107 63 303 372 41 810"
                                    + "\n128 2088 3422 111 3312 740 3024 1946 920 131 112 477 3386 2392 1108 2741";

            Console.WriteLine();
            Part1(actualInput);

            Console.WriteLine();
            input = "5 9 2 8\n9 4 7 3\n3 8 6 5";
            Part2(input);

            Console.WriteLine();
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            string[] lines = input.Split('\n');
            int sum = 0;
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');

                int max = int.Parse(splitLine[0]);
                int min = int.Parse(splitLine[0]);
                for (int i = 1; i < splitLine.Length; i++)
                {
                    int num = int.Parse(splitLine[i]);
                    if (num > max)
                        max = num;
                    if (num < min)
                        min = num;
                }
                sum += (max - min);
            }

            Console.WriteLine($"{input}\nSum: {sum}");
        }

        private static void Part2(string input)
        {
            string[] lines = input.Split('\n');
            int sum = 0;
            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');

                Combinations combos = new Combinations { Items = splitLine.ToList() };
                int added = 0;
                foreach (Combo combo in combos.GetCombinations())
                {
                    if ((float)combo.Item1 / (float)combo.Item2 == combo.Item1 / combo.Item2)
                    {
                        added = (combo.Item1 / combo.Item2);
                    }
                }
                sum += added;
            }

            Console.WriteLine($"{input}\nSum: {sum}");
        }
    }
}
