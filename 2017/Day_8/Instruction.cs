using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_8
{
    public class Instruction
    {
        public string Register { get; set; }

        public int Amount { get; set; }

        public string DependentRegister { get; set; }

        public string Operator { get; set; }

        public int DependentNumber { get; set; }
    }
}
