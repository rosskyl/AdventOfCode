using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_16
{
    public static class Day16
    {
        public static void Run()
        {
            string input = "s1,x3/4,pe/b";
            Part1(input, true);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 16);
            Part1(actualInput);

            //Part2(input, true);
            Part2(actualInput);
        }

        private static void Part1(string input, bool testInput = false)
        {
            List<char> programs;
            if (testInput)
            {
                programs = new List<char>
                {
                    'a', 'b', 'c', 'd', 'e'
                };
            }
            else
            {
                programs = new List<char>
                {
                    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
                    'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'
                };
            }

            string[] commands = input.Split(',');

            foreach (string command in commands)
            {
                switch (command[0])
                {
                    case 's':
                        Switch(ref programs, int.Parse(command.Substring(1)));
                        break;
                    case 'x':
                        string[] split = command.Substring(1).Split('/');
                        int posA = int.Parse(split[0]);
                        int posB = int.Parse(split[1]);
                        Exchange(ref programs, posA, posB);
                        break;
                    case 'p':
                        string[] programSplit = command.Substring(1).Split('/');
                        char programA = programSplit[0].ToCharArray()[0];
                        char programB = programSplit[1].ToCharArray()[0];
                        Partner(ref programs, programA, programB);
                        break;
                }
            }

            string programOrder = string.Join(string.Empty, programs);

            Console.WriteLine($"Programs are in order {programOrder}");
        }

        private static void Part2(string input, bool testInput = false)
        {
            List<char> programs;
            if (testInput)
            {
                programs = new List<char>
                {
                    'a', 'b', 'c', 'd', 'e'
                };
            }
            else
            {
                programs = new List<char>
                {
                    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
                    'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'
                };
            }

            string[] commands = input.Split(',');
            string originalOrder = string.Join(string.Empty, programs);

            for (int i = 0; i < 1000000000; i++)
            {
                foreach (string command in commands)
                {
                    switch (command[0])
                    {
                        case 's':
                            Switch(ref programs, int.Parse(command.Substring(1)));
                            break;
                        case 'x':
                            string[] split = command.Substring(1).Split('/');
                            int posA = int.Parse(split[0]);
                            int posB = int.Parse(split[1]);
                            Exchange(ref programs, posA, posB);
                            break;
                        case 'p':
                            string[] programSplit = command.Substring(1).Split('/');
                            char programA = programSplit[0].ToCharArray()[0];
                            char programB = programSplit[1].ToCharArray()[0];
                            Partner(ref programs, programA, programB);
                            break;
                    }
                }

                if (string.Join(string.Empty, programs) == originalOrder)
                {
                    int cycle = i + 1;
                    while (i < 1000000000)
                        i += cycle;

                    i -= cycle;
                }
            }

            string programOrder = string.Join(string.Empty, programs);

            Console.WriteLine($"Programs are in order {programOrder}");
        }

        private static void Switch(ref List<char> programs, int num)
        {
            List<char> end = programs.Take(programs.Count - num).ToList();
            programs = programs.Skip(programs.Count - num).ToList();
            programs.AddRange(end);
        }

        private static void Exchange(ref List<char> programs, int posA, int posB)
        {
            char tmp = programs[posA];
            programs[posA] = programs[posB];
            programs[posB] = tmp;
        }

        private static void Partner(ref List<char> programs, char programA, char programB)
        {
            int posA = programs.IndexOf(programA);
            int posB = programs.IndexOf(programB);
            Exchange(ref programs, posA, posB);
        }
    }
}
