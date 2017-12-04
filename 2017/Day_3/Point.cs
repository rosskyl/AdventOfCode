using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_3
{
    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Point))
            {
                Point other = (Point)obj;
                return this.X == other.X && this.Y == other.Y;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public Point Increment()
        {
            if (X > 0)
            {
                if (Y == X)
                    return new Point(X - 1, Y);
                else if (Y > X)
                    return new Point(X - 1, Y);
                else if (-Y > X)
                    return new Point(X + 1, Y);
                else if (-Y == X)
                    return new Point(X + 1, Y);
                else
                    return new Point(X, Y + 1);
            }
            else
            {
                if (Y == X)
                    return new Point(X + 1, Y);
                else if (-Y > -X)
                    return new Point(X + 1, Y);
                else if (Y > -X)
                    return new Point(X - 1, Y);
                else
                    return new Point(X, Y - 1);
            }
        }
    }
}
