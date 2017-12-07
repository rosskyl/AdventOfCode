using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_7
{
    public class Program
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public List<string> Children { get; set; }

        public List<Program> ChildrenNodes { get; set; }

        public Program() { }

        public Program(string input)
        {
            string[] words = input.Split(' ');
            Name = words[0];
            string weight = words[1];
            weight = weight.Replace("(", "");
            weight = weight.Replace(")", "");
            Weight = int.Parse(weight);

            Children = words.Skip(3).Select(w => w.TrimEnd(',')).ToList();

            ChildrenNodes = new List<Program>();
        }

        public override string ToString()
        {
            return Name;
        }

        public int TowerWeight()
        {
            int total = Weight;

            foreach (Program node in ChildrenNodes)
            {
                total += node.TowerWeight();
            }

            return total;
        }

        public bool IsBalanced()
        {
            if (ChildrenNodes.Count > 0)
            {
                int targetWeight = ChildrenNodes[0].TowerWeight();
                foreach (Program node in ChildrenNodes)
                {
                    if (node.TowerWeight() != targetWeight)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
