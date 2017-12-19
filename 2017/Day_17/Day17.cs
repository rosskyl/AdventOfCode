using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_17
{
    public static class Day17
    {
        public static void Run()
        {
            Part1(3);

            Part1(316);

            Part2(316);
        }

        private static void Part1(int step)
        {
            int current = 0;

            List<int> items = new List<int>() { 0 };

            for (int i = 1; i <= 2017; i++)
            {
                current = CalculateNextIndex(step, current, items.Count);
                current++;
                items.Insert(current, i);
            }

            Console.WriteLine($"The item after 2017 is {items[current + 1]}");
        }

        private static void Part2(int step)
        {
            int current = 0;
            int count = 1;
            int item = 0;

            for (int i = 1; i <= 50000000; i++)
            {
                current = CalculateNextIndex(step, current, count);
                current++;
                count++;
                if (current == 1)
                    item = i;
            }
            
            Console.WriteLine($"The item after 0 is {item}");
        }

        private static int CalculateNextIndex(int step, int current, int numItems)
        {
            step = step % numItems;
            current += step;
            if (current >= numItems)
                current -= numItems;

            return current;
        }
    }
}
