using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_2
{
    public class Combinations
    {
        public List<string> Items { get; set; }

        public List<Combo> GetCombinations()
        {
            List<Combo> results = new List<Combo>();

            for (int i = 0; i < Items.Count; i++)
            {
                for (int j = 0; j < Items.Count; j++)
                {
                    if (i != j)
                    {
                        int bigger = int.Parse(int.Parse(Items[i]) > int.Parse(Items[j]) ? Items[i] : Items[j]);
                        int smaller = int.Parse(int.Parse(Items[i]) < int.Parse(Items[j]) ? Items[i] : Items[j]);
                        Combo combo = new Combo
                        {
                            Item1 = bigger,
                            Item2 = smaller
                        };
                        results.Add(combo);
                    }
                }
            }

            return results;
        }
    }

    public class Combo
    {
        public int Item1 { get; set; }

        public int Item2 { get; set; }
    }
}
