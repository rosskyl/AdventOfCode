using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public static class ReadInputFile
    {
        public static string ReadFile(int day)
        {
            return File.ReadAllText($"Inputs\\day{day}.txt");
        }

        public static string[] SplitLines(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Select(l => l.Replace("\r", "")).ToArray();
        }

        public static string[] ReadAndSplitFile(int day)
        {
            var contents = ReadFile(day);
            return SplitLines(contents);
        }
    }
}
