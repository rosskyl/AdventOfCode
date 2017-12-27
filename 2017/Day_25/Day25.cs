using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_25
{
    public class Day25
    {
        public static void Run()
        {
            Part1(true);
            Part1();
        }

        private static void Part1(bool isTest = false)
        {
            int steps;
            Dictionary<char, State> states;

            if (isTest)
            {
                SetupTestInput(out steps, out states);
            }
            else
            {
                SetupInput(out steps, out states);
            }

            Dictionary<int, int> tape = new Dictionary<int, int>();

            char currentState = 'A';
            int position = 0;
            for (int i = 0; i < steps; i++)
            {
                states[currentState].CurrentValue = GetValue(tape, position);
                tape[position] = states[currentState].Write();
                position += states[currentState].Move();

                currentState = states[currentState].NextState();
            }

            Console.WriteLine($"The checksum is {tape.Values.Count(v => v == 1)}");
        }

        private static int GetValue(Dictionary<int, int> tape, int position)
        {
            if (tape.ContainsKey(position))
                return tape[position];
            else
                return 0;
        }

        private static void SetupTestInput(out int steps, out Dictionary<char, State> states)
        {
            steps = 6;

            states = new Dictionary<char, State>();

            states.Add('A', new State
            {
                Move0 = 1,
                Move1 = -1,
                State0 = 'B',
                State1 = 'B',
                Write0 = 1,
                Write1 = 0
            });

            states.Add('B', new State
            {
                Move0 = -1,
                Move1 = 1,
                State0 = 'A',
                State1 = 'A',
                Write0 = 1,
                Write1 = 1
            });
        }

        private static void SetupInput(out int steps, out Dictionary<char, State> states)
        {
            steps = 12656374;
            states = new Dictionary<char, State>();

            states.Add('A', new State
            {
                Move0 = 1,
                Move1 = -1,
                State0 = 'B',
                State1 = 'C',
                Write0 = 1,
                Write1 = 0
            });

            states.Add('B', new State
            {
                Move0 = -1,
                Move1 = -1,
                State0 = 'A',
                State1 = 'D',
                Write0 = 1,
                Write1 = 1
            });

            states.Add('C', new State
            {
                Move0 = 1,
                Move1 = 1,
                State0 = 'D',
                State1 = 'C',
                Write0 = 1,
                Write1 = 0
            });
            
            states.Add('D', new State
            {
                Move0 = -1,
                Move1 = 1,
                State0 = 'B',
                State1 = 'E',
                Write0 = 0,
                Write1 = 0
            });

            states.Add('E', new State
            {
                Move0 = 1,
                Move1 = -1,
                State0 = 'C',
                State1 = 'F',
                Write0 = 1,
                Write1 = 1
            });

            states.Add('F', new State
            {
                Move0 = -1,
                Move1 = 1,
                State0 = 'E',
                State1 = 'A',
                Write0 = 1,
                Write1 = 1
            });
        }
    }
}
