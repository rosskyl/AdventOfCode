using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_15
{
    public static class Day15
    {
        private static Dictionary<long, string> binaryHashmap = new Dictionary<long, string>();


        public static void Run()
        {
            //Part1(65, 8921);
            //Part1(783, 325);


            //Part2(65, 8921);
            Part2(783, 325);
        }

        private static void Part1(long genAstart, long genBstart)
        {
            int denominator = 2147483647;
            int genAfactor = 16807;
            int genBfactor = 48271;

            long matches = 0;
            for (int i = 0; i < 40000000; i++)
            {
                genAstart = genAstart * genAfactor % denominator;
                genBstart = genBstart * genBfactor % denominator;

                string binA = ConvertToBinary(genAstart);
                string binB = ConvertToBinary(genBstart);

                if (binA.Equals(binB))
                    matches++;
            }

            Console.WriteLine($"Found {matches} matches");
        }

        private static void Part2(long genAstart, long genBstart)
        {
            int matches = 0;
            for (int i = 0; i < 5000000; i++)
            {
                genAstart = GenerateA(genAstart);
                genBstart = GenerateB(genBstart);

                string binA = ConvertToBinary(genAstart);
                string binB = ConvertToBinary(genBstart);

                if (binA.Equals(binB))
                    matches++;
            }

            Console.WriteLine($"Found {matches} matches");
        }

        private static long GenerateA(long start)
        {
            int denominator = 2147483647;
            int factor = 16807;

            do
            {
                start = start * factor % denominator;
            } while (start % 4 != 0);

            return start;
        }

        private static long GenerateB(long start)
        {
            int denominator = 2147483647;
            int factor = 48271;

            do
            {
                start = start * factor % denominator;
            } while (start % 8 != 0);

            return start;
        }

        private static string ConvertToBinary(long num)
        {
            string binary = Convert.ToString(num, 2);
            binary = string.Join(string.Empty, binary.Skip(binary.Length - 16));
            return binary;
        }
    }
}
