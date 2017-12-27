using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_22
{
    public class Node
    {
        public Node()
        {
            X = 0;
            Y = 0;
            CurrentState = State.Clean;
        }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
            CurrentState = State.Clean;
        }

        public Node(int x, int y, State state)
        {
            X = x;
            Y = y;
            CurrentState = state;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public State CurrentState { get; set; }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Node))
            {
                Node otherNode = (Node)obj;
                return this.X == otherNode.X && this.Y == otherNode.Y;
            }
            else
                return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public enum State
        {
            Clean,
            Weakened,
            Infected,
            Flagged
        }
    }
}
