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
        public static string ReadFile(int day, string filename = "input.txt")
        {
            return File.ReadAllText($"Day_{day}\\{filename}");
        }

        public static string[] SplitLines(string text)
        {
            string[] lines = text.Split('\n');

            return lines.Select(l => l.Replace("\r", "")).ToArray();
        }
    }
}
