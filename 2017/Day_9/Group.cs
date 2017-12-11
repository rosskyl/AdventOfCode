using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_9
{
    public class Group
    {
        public List<Group> Children { get; set; }

        public int Garbage { get; set; }

        public Group()
        {
            Children = new List<Group>();
            Garbage = 0;
        }

        public int TotalGarbage()
        {
            int total = Garbage;
            foreach (Group child in Children)
                total += child.TotalGarbage();

            return total;
        }

        public int Score()
        {
            int total = 1;
            foreach (Group child in Children)
                total += child.InnerScore(1);

            return total;
        }

        protected int InnerScore(int level)
        {
            int total = level + 1;
            foreach (Group child in Children)
                total += child.InnerScore(level + 1);

            return total;
        }
    }
}
