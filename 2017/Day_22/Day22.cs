using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_22
{
    public class Day22
    {
        public static void Run()
        {
            string input = "..#\n#..\n...";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 22);
            Part1(actualInput);

            Part2(input, 100);

            Part2(input);

            Part2(actualInput);
        }

        private static void Part1(string input, int iterations = 10000)
        {
            List<Node> infectedNodes = ParseInput1(input);

            int numInfections = 0;

            int x = 0;
            int y = 0;
            char dir = 'u';

            for (int i = 0; i < iterations; i++)
            {
                IEnumerable<Node> currentNode = infectedNodes.Where(n => n.Equals(x, y));
                bool isInfected = currentNode.Any();
                dir = GetNextDirection1(dir, isInfected);

                if (isInfected)
                {
                    infectedNodes.Remove(currentNode.First());
                }
                else
                {
                    numInfections++;
                    infectedNodes.Add(new Node(x, y));
                }

                switch (dir)
                {
                    case 'u':
                        y++;
                        break;
                    case 'r':
                        x++;
                        break;
                    case 'd':
                        y--;
                        break;
                    case 'l':
                        x--;
                        break;
                }
            }

            Console.WriteLine($"Number of bursts causing infections: {numInfections}");
        }

        private static void Part2(string input, int iterations = 10000000)
        {
            Dictionary<int, Node> nodes = ParseInput2(input);

            int numInfections = 0;

            int x = 0;
            int y = 0;
            char dir = 'u';

            for (int i = 0; i < iterations; i++)
            {
                Node currentNode = new Node(x, y);
                int hash = currentNode.GetHashCode();
                if (nodes.ContainsKey(hash))
                    currentNode = nodes[hash];
                else
                    nodes.Add(hash, currentNode);
                
                dir = GetNextDirection2(dir, currentNode.CurrentState);

                switch (currentNode.CurrentState)
                {
                    case Node.State.Clean:
                        currentNode.CurrentState = Node.State.Weakened;
                        break;
                    case Node.State.Weakened:
                        numInfections++;
                        currentNode.CurrentState = Node.State.Infected;
                        break;
                    case Node.State.Infected:
                        currentNode.CurrentState = Node.State.Flagged;
                        break;
                    case Node.State.Flagged:
                        currentNode.CurrentState = Node.State.Clean;
                        break;
                }

                nodes[hash] = currentNode;

                switch (dir)
                {
                    case 'u':
                        y++;
                        break;
                    case 'r':
                        x++;
                        break;
                    case 'd':
                        y--;
                        break;
                    case 'l':
                        x--;
                        break;
                }
            }

            Console.WriteLine($"Number of bursts causing infections: {numInfections}");
        }

        private static char GetNextDirection2(char dir, Node.State currentNodeState)
        {
            List<char> dirs = new List<char> { 'u', 'r', 'd', 'l' };

            int index = dirs.IndexOf(dir);

            switch (currentNodeState)
            {
                case Node.State.Clean:
                    index--;
                    break;
                case Node.State.Infected:
                    index++;
                    break;
                case Node.State.Flagged:
                    index += (dirs.Count / 2);
                    break;
            }

            if (index >= dirs.Count)
                index -= dirs.Count;
            else if (index < 0)
                index += dirs.Count;

            return dirs[index];
        }

        private static char GetNextDirection1(char dir, bool currentNodeInfected)
        {
            List<char> dirs = new List<char> { 'u', 'r', 'd', 'l' };

            int index = dirs.IndexOf(dir);
            if (currentNodeInfected)
            {
                index++;
            }
            else
            {
                index--;
            }

            if (index < 0)
                index += dirs.Count;
            else if (index >= dirs.Count)
                index -= dirs.Count;

            return dirs[index];
        }

        private static List<Node> ParseInput1(string input)
        {
            List<Node> nodes = new List<Node>();

            string[] lines = ReadInputFile.SplitLines(input);

            int center = lines.Length / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        nodes.Add(new Node(j - center, -(i - center)));
                    }
                }
            }

            return nodes;
        }

        private static Dictionary<int, Node> ParseInput2(string input)
        {
            Dictionary<int, Node> nodes = new Dictionary<int, Node>();

            string[] lines = ReadInputFile.SplitLines(input);

            int center = lines.Length / 2;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        Node newNode = new Node(j - center, -(i - center), Node.State.Infected);
                        nodes.Add(newNode.GetHashCode(), newNode);
                    }
                }
            }

            return nodes;
        }
    }
}
