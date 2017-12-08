using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_8
{
    public static class Day8
    {
        public static void Run()
        {
            string input = "b inc 5 if a > 1\na inc 1 if b < 5\nc dec -10 if a >= 1\nc inc -20 if c == 10";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 8);
            Part1(actualInput);
        }

        private static void Part1(string input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            List<Instruction> instructions = SanitizeInput(input);

            int largestValue = 0;
            foreach (Instruction instruction in instructions)
            {
                PerformInstruction(instruction, ref registers);
                int largestCurrentValue = registers.Values.Max();
                if (largestCurrentValue > largestValue)
                    largestValue = largestCurrentValue;
            }

            int largestEndValue = registers.Values.Max();
            Console.WriteLine($"The max register at the end is {largestEndValue} and the max overall is {largestValue}");
        }

        private static void PerformInstruction(Instruction instruction, ref Dictionary<string, int> registers)
        {
            if (!registers.ContainsKey(instruction.Register))
            {
                registers[instruction.Register] = 0;
            }
            if (!registers.ContainsKey(instruction.DependentRegister))
            {
                registers[instruction.DependentRegister] = 0;
            }

            bool doInstruction = false;
            switch (instruction.Operator)
            {
                case ">":
                    doInstruction = registers[instruction.DependentRegister] > instruction.DependentNumber;
                    break;
                case "<":
                    doInstruction = registers[instruction.DependentRegister] < instruction.DependentNumber;
                    break;
                case ">=":
                    doInstruction = registers[instruction.DependentRegister] >= instruction.DependentNumber;
                    break;
                case "<=":
                    doInstruction = registers[instruction.DependentRegister] <= instruction.DependentNumber;
                    break;
                case "==":
                    doInstruction = registers[instruction.DependentRegister] == instruction.DependentNumber;
                    break;
                case "!=":
                    doInstruction = registers[instruction.DependentRegister] != instruction.DependentNumber;
                    break;
            }

            if (doInstruction)
            {
                registers[instruction.Register] += instruction.Amount;
            }
        }

        private static List<Instruction> SanitizeInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);

            List<Instruction> instructions = lines.Select(line =>
            {
                string[] splitLine = line.Split(' ');
                return new Instruction
                {
                    Register = splitLine[0],
                    Amount = int.Parse(splitLine[2]) * (splitLine[1] == "inc" ? 1 : -1),
                    DependentRegister = splitLine[4],
                    Operator = splitLine[5],
                    DependentNumber = int.Parse(splitLine[6])
                };
            }).ToList();

            return instructions;
        }
    }
}
