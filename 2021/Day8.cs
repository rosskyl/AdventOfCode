using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day8
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(8);
            var lines = ReadInputFile.SplitLines(contents);

            var numEasyDigits = 0;
            var easyDigits = new int[] { 2, 3, 4, 7 };
            var part2 = 0;

            foreach (var line in lines)
            {
                var outputValues = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                numEasyDigits += outputValues.Count(v => easyDigits.Contains(v.Length));
                part2 += CalculateLine(line);
            }


            Console.WriteLine($"Part 1: {numEasyDigits}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int CalculateLine(string line)
        {
            var input = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            var output = line.Split('|', StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            var one = input.First(i => i.Length == 2);
            var four = input.First(i => i.Length == 4);
            var seven = input.First(i => i.Length == 3);
            var eight = input.First(i => i.Length == 7);
            var top = seven.First(s => !one.Contains(s));

            var six = input.First(i => i.Length == 6 && !(i.Contains(one[0]) && i.Contains(one[1])));
            var bottomRight = one.First(o => six.Contains(o));
            var topRight = seven.First(s => s != top && s != bottomRight);


            //var five = input.First(i => i.Length == 5 && !(i.Contains(one[0]) && i.Contains(one[1])));


            var three = input.First(i => i.Length == 5 && i.Intersect(seven).Count() == 3);
            var topLeft = four.First(f => !three.Contains(f));
            var bottom = three.First(t => !four.Contains(t) && t != top);
            var five = input.First(i => i.Length == 5 && i.Contains(topLeft));
            var two = input.First(i => i.Length == 5 && i != three && i != five);

            var bottomLeft = two.First(t => !five.Contains(t) && t != topRight);
            var zero = input.First(i => i.Length == 6 && i.Contains(bottomLeft) && i.Contains(topRight));
            var nine = input.First(i => i.Length == 6 && !i.Contains(bottomLeft) && i.Contains(topRight));

            var numbers = new Dictionary<string, string>
            {
                    { string.Concat(zero.OrderBy(c => c)),  "0" },
                    { string.Concat(one.OrderBy(c => c)), "1" },
                    { string.Concat(two.OrderBy(c => c)), "2" },
                    { string.Concat(three.OrderBy(c => c)), "3" },
                    { string.Concat(four.OrderBy(c => c)), "4" },
                    { string.Concat(five.OrderBy(c => c)), "5" },
                    { string.Concat(six.OrderBy(c => c)), "6" },
                    { string.Concat(seven.OrderBy(c => c)), "7" },
                    { string.Concat(eight.OrderBy(c => c)), "8" },
                    { string.Concat(nine.OrderBy(c => c)), "9" }
            };

        var outputValue = "";

            foreach (var code in output)
            {
                outputValue += numbers[string.Concat(code.OrderBy(c => c))];
            }

            return int.Parse(outputValue);
        }
    }
}
