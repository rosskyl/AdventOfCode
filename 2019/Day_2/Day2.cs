using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2019.Day_2
{
    class Day2
    {
        public static void Run()
        {
            //var contents = "1,9,10,3,2,3,11,0,99,30,40,50";
            //var contents = "1,0,0,0,99";
            var contents = ReadInputFile.ReadFile(2);

            var instructions = contents.Split(',').Select(c => int.Parse(c)).ToArray();

            var part1 = Part1(instructions);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(instructions);
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(int[] instructions)
        {
            var noun1 = 12;
            var noun2 = 2;
            return RunProgram(instructions, noun1, noun2);
        }

        private static int Part2(int[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                for (int j = 0; j < instructions.Length; j++)
                {
                    if (RunProgram(instructions, i, j) == 19690720)
                        return 100 * i + j;
                }
            }

            return 0;
        }

        private static int RunProgram(int[] instructions, int noun1, int noun2)
        {
            int[] memory = new int[instructions.Length];
            instructions.CopyTo(memory, 0);

            memory[1] = noun1;
            memory[2] = noun2;
            int index = 0;

            while (memory[index] != 99)
            {
                var index1 = memory[index + 1];
                var index2 = memory[index + 2];
                var resultIndex = memory[index + 3];

                if (memory[index] == 1)
                    memory[resultIndex] = memory[index1] + memory[index2];
                else if (memory[index] == 2)
                    memory[resultIndex] = memory[index1] * memory[index2];

                index += 4;
            }

            return memory[0];
        }
    }
}
