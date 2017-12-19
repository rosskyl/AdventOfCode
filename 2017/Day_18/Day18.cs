using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_18
{
    public static class Day18
    {
        private static int NumberSentB = 0;

        public static void Run()
        {
            string input = "set a 1\nadd a 2\nmul a a\nmod a 5\nsnd a\nset a 0\nrcv a\njgz a -1\nset a 1\njgz a -2";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 18);
            Part1(actualInput);

            Console.WriteLine();

            input = "snd 1\nsnd 2\nsnd p\nrcv a\nrcv b\nrcv c\nrcv d";
            Part2(input);

            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            string[] instructions = ReadInputFile.SplitLines(input);
            Dictionary<string, long> registers = new Dictionary<string, long>();
            long sound = 0;
            long index = 0;
            bool rcvCalled = false;

            while (index < instructions.Length && !rcvCalled)
            {
                long value;
                string[] splitInstruction = instructions[index].Split(' ');
                switch (splitInstruction[0])
                {
                    case "snd":
                        sound = GetValue(splitInstruction[1], registers);
                        break;
                    case "set":
                        registers[splitInstruction[1]] = GetValue(splitInstruction[2], registers);
                        break;
                    case "add":
                        value = GetValue(splitInstruction[2], registers);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] += value;
                        else
                            registers[splitInstruction[1]] = 0 + value;
                        break;
                    case "mul":
                        value = GetValue(splitInstruction[2], registers);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] *= value;
                        else
                            registers[splitInstruction[1]] = 0;
                        break;
                    case "mod":
                        value = GetValue(splitInstruction[2], registers);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] %= value;
                        else
                            registers[splitInstruction[1]] = 0;
                        break;
                    case "rcv":
                        if (GetValue(splitInstruction[1], registers) != 0)
                        {
                            registers[splitInstruction[1]] = sound;
                            rcvCalled = true;
                        }
                        break;
                    case "jgz":
                        if (GetValue(splitInstruction[1], registers) > 0)
                        {
                            index += GetValue(splitInstruction[2], registers);
                            index--;
                        }
                        break;
                }

                index += 1;
            }

            Console.WriteLine($"The recovered frequency is {sound}");
        }

        private static void Part2(string input)
        {
            string[] instructions = ReadInputFile.SplitLines(input);
            Dictionary<string, long> registersA = new Dictionary<string, long>();
            Dictionary<string, long> registersB = new Dictionary<string, long>();
            Queue<long> queueA = new Queue<long>();
            Queue<long> queueB = new Queue<long>();
            long indexA = 0;
            long indexB = 0;

            NumberSentB = 0;

            bool isAfinished = false;
            bool isBfinished = false;
            while (!isAfinished && !isBfinished)
            {
                if (queueA.Count == 0 && indexA != 0)
                {
                    isAfinished = true;
                    isBfinished = true;
                }
                if (!isAfinished)
                {
                    isAfinished = RunProgram(instructions, ref registersA, ref indexA, 0, ref queueB, ref queueA);
                }


                if (queueB.Count == 0 && indexB != 0)
                {
                    isAfinished = true;
                    isBfinished = true;
                }
                if (!isBfinished)
                {
                    isBfinished = RunProgram(instructions, ref registersB, ref indexB, 1, ref queueA, ref queueB);
                }
            }

            Console.WriteLine($"B sent a value {NumberSentB} times");
        }

        //Returns if the program is done running or not
        private static bool RunProgram(string[] instructions, ref Dictionary<string, long> registers, ref long index,
            int defaultValue, ref Queue<long> output, ref Queue<long> input)
        {
            while (index < instructions.Length && index >= 0)
            {
                long value;
                string[] splitInstruction = instructions[index].Split(' ');
                switch (splitInstruction[0])
                {
                    case "snd":
                        output.Enqueue(GetValue(splitInstruction[1], registers, defaultValue));
                        if (defaultValue == 1)
                            NumberSentB++;
                        break;
                    case "set":
                        registers[splitInstruction[1]] = GetValue(splitInstruction[2], registers, defaultValue);
                        break;
                    case "add":
                        value = GetValue(splitInstruction[2], registers, defaultValue);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] += value;
                        else
                            registers[splitInstruction[1]] = defaultValue + value;
                        break;
                    case "mul":
                        value = GetValue(splitInstruction[2], registers, defaultValue);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] *= value;
                        else
                            registers[splitInstruction[1]] = defaultValue * value;
                        break;
                    case "mod":
                        value = GetValue(splitInstruction[2], registers, defaultValue);
                        if (registers.Keys.Contains(splitInstruction[1]))
                            registers[splitInstruction[1]] %= value;
                        else
                            registers[splitInstruction[1]] = defaultValue % value;
                        break;
                    case "rcv":
                        //No input so need to wait and come back later
                        if (input.Count == 0)
                            return false;

                        value = input.Dequeue();
                        registers[splitInstruction[1]] = value;
                        break;
                    case "jgz":
                        if (GetValue(splitInstruction[1], registers, defaultValue) > 0)
                        {
                            index += GetValue(splitInstruction[2], registers, defaultValue);
                            index--;
                        }
                        break;
                }

                index += 1;
            }

            return true;
        }

        private static long GetValue(string register, Dictionary<string, long> registers, int defaultValue = 0)
        {
            if (int.TryParse(register, out int value))
                return value;
            else if (registers.Keys.Contains(register))
                return registers[register];
            else
            {
                registers[register] = defaultValue;
                return defaultValue;
            }
        }
    }
}
