using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_23
{
    public class Day23
    {
        public static void Run()
        {
            string input = ReadInputFile.ReadFile("Input.txt", 23);
            Part1(input);

            Part2();
        }

        private static void Part1(string input)
        {
            string[] commands = ReadInputFile.SplitLines(input);

            Dictionary<string, long> registers = new Dictionary<string, long>();

            long index = 0;
            int mulInvoked = 0;

            while (index >= 0 && index < commands.Length)
            {
                string[] splitCommand = commands[index].Split();

                long value;
                switch (splitCommand[0])
                {
                    case "set":
                        value = GetValue(splitCommand[2], registers);
                        registers[splitCommand[1]] = value;
                        break;
                    case "sub":
                        value = GetValue(splitCommand[2], registers);
                        value = GetValue(splitCommand[1], registers) - value;
                        registers[splitCommand[1]] = value;
                        break;
                    case "mul":
                        mulInvoked++;
                        value = GetValue(splitCommand[2], registers);
                        value = GetValue(splitCommand[1], registers) * value;
                        registers[splitCommand[1]] = value;
                        break;
                    case "jnz":
                        if (GetValue(splitCommand[1], registers) != 0)
                            index += (GetValue(splitCommand[2], registers) - 1);
                        break;
                }

                index++;
            }

            Console.WriteLine($"Mul command was invoked {mulInvoked} times");
        }

        private static void Part2()
        {
            //It all comes down to the number of non-primes between 107900-124900 jumping by 17
            int numNotPrime = 0;
            for (long a = 107900; a <= 124900; a += 17)
            {
                if (!IsPrime(a))
                    numNotPrime++;
            }
            Console.WriteLine(numNotPrime);
        }

        private static bool IsPrime(long num)
        {
            for (long i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        private static long GetValue(string register, Dictionary<string, long> registers)
        {
            if (int.TryParse(register, out int value))
                return value;
            else if (registers.Keys.Contains(register))
                return registers[register];
            else
            {
                registers[register] = 0;
                return 0;
            }
        }
    }
}
