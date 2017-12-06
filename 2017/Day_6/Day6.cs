using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_6
{
    public static class Day6
    {
        public static void Run()
        {
            string input = "0 2 7 0";
            Part1(input);

            string actualInput = "11 11 13 7 0 15 5 5 4 4 1 1 7 1 15 11";
            Part1(actualInput);

            Console.WriteLine();
            Part2(input);
            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            List<int> registers = SplitLine(input);
            List<List<int>> pastStates = new List<List<int>>();

            while (!ListContainsList(pastStates, registers))
            {
                pastStates.Add(registers.Select(r => r).ToList());

                registers = ReallocateRegisters(registers);
            }

            Console.WriteLine($"Duplicate state met at {pastStates.Count}");
        }

        private static void Part2(string input)
        {
            List<int> registers = SplitLine(input);
            List<List<int>> pastStates = new List<List<int>>();

            while (!ListContainsList(pastStates, registers))
            {
                pastStates.Add(registers.Select(r => r).ToList());

                registers = ReallocateRegisters(registers);
            }

            int index = IndexOfListInList(pastStates, registers);

            Console.WriteLine($"Infinite loop is {pastStates.Count - index} number of states");
        }

        private static int IndexOfListInList(List<List<int>> listOfLists, List<int> list)
        {
            for (int i = 0; i < listOfLists.Count; i++)
            {
                if (listOfLists[i].Count == list.Count)
                {
                    bool matchFound = true;
                    for (int j = 0; j < listOfLists[i].Count; j++)
                    {
                        if (listOfLists[i][j] != list[j])
                            matchFound = false;
                    }
                    if (matchFound)
                        return i;
                }
            }
            return -1;
        }

        private static bool ListContainsList(List<List<int>> listOfLists, List<int> list)
        {
            return listOfLists.Exists(l =>
            {
                if (l.Count != list.Count)
                    return false;
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i] != list[i])
                        return false;
                }
                return true;
            });
        }

        private static List<int> ReallocateRegisters(List<int> registers)
        {
            int max = registers[0];
            int index = 0;

            for (int i = 0; i < registers.Count; i++)
            {
                if (registers[i] > max)
                {
                    max = registers[i];
                    index = i;
                }
            }

            registers[index] = 0;
            index++;
            while (max > 0)
            {
                if (index == registers.Count)
                {
                    index = 0;
                }
                registers[index] += 1;
                index++;
                max--;
            }

            return registers;
        }

        private static List<int> SplitLine(string line)
        {
            List<int> numbers = new List<int>();

            string[] splitLine = line.Split(' ');

            foreach (string num in splitLine)
            {
                numbers.Add(int.Parse(num));
            }

            return numbers;
        }
    }
}
