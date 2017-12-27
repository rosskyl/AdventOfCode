using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_25
{
    public class State
    {
        public int CurrentValue { get; set; }

        public int Write1 { get; set; }

        public int Write0 { get; set; }

        public int Move1 { get; set; }

        public int Move0 { get; set; }

        public char State1 { get; set; }

        public char State0 { get; set; }

        public int Write()
        {
            if (CurrentValue == 1)
                return Write1;
            else
                return Write0;
        }

        public int Move()
        {
            if (CurrentValue == 1)
                return Move1;
            else
                return Move0;
        }

        public char NextState()
        {
            if (CurrentValue == 1)
                return State1;
            else
                return State0;
        }
    }
}
