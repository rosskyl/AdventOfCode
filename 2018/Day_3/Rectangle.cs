using System;
using System.Collections.Generic;
using System.Text;

namespace _2018.Day_3
{
    class Rectangle
    {
        public string Id { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle(string line)
        {
            string[] split = line.Split(" @ ");
            Id = split[0];

            split = split[1].Split(": ");

            Left = int.Parse(split[0].Split(',')[0]);
            Top = int.Parse(split[0].Split(',')[1]);

            split = split[1].Split('x');

            Width = int.Parse(split[0]);
            Height = int.Parse(split[1]);
        }
    }
}
