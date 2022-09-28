using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    static class Day4
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(4);
            var lines = ReadInputFile.SplitLines(contents);

            var numbersCalled = lines[0].Split(",");

            var boards = new List<Board>();

            lines = lines.Skip(2).ToArray();

            while (lines.Length > 0)
            {
                boards.Add(new Board(lines.Take(5).ToArray()));
                lines = lines.Skip(6).ToArray();
            }

            var score1 = PlayGameFirstWin(boards, numbersCalled);
            var score2 = PlayGameLastWin(boards, numbersCalled);

            Console.WriteLine($"Part 1: {score1}");
            Console.WriteLine($"Part 2: {score2}");
        }

        private static int PlayGameFirstWin(List<Board> boards, string[] numbersCalled)
        {
            foreach (var number in numbersCalled)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(number);
                    if (board.HasWon())
                        return board.BoardScore(number);
                }
            }
            return 0;
        }

        private static int PlayGameLastWin(List<Board> boards, string[] numbersCalled)
        {
            foreach (var number in numbersCalled)
            {
                foreach (var board in boards.ToList())
                {
                    board.MarkNumber(number);
                    if (board.HasWon())
                    {
                        boards.Remove(board);
                        if (boards.Count == 0)
                            return board.BoardScore(number);
                    }
                }
            }
            return 0;
        }
    }

    class Board
    {
        private List<Dictionary<string, bool>> _numbers = new List<Dictionary<string, bool>>();
        public Board(string[] lines)
        {
            if (lines.Length != 5)
                throw new Exception("Number of lines incorrect");

            for (int i = 0; i < 5; i++)
                _numbers.Add(new Dictionary<string, bool>());

            foreach (var line in lines)
            {
                var numbers = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < numbers.Length; i++) {
                    _numbers[i].Add(numbers[i], false);
                }
            }
        }

        public void MarkNumber(string number)
        {
            foreach (var column in _numbers)
            {
                if (column.ContainsKey(number))
                {
                    column[number] = true;
                }
            }
        }

        public bool HasWon()
        {
            foreach (var column in _numbers)
                if (column.Values.All(v => v == true))
                    return true;

            for (int i = 0; i < 5; i++)
                if (_numbers.Select(c => c.Values.ToList()[i]).All(v => v == true))
                    return true;

            return false;
        }

        public int BoardScore(string numberCalled)
        {
            var uncalled = 0;
            foreach (var column in _numbers)
                foreach (var key in column.Keys)
                    if (column[key] == false)
                        uncalled += int.Parse(key);

            return uncalled * int.Parse(numberCalled);
        }
    }
}
