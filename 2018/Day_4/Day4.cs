using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2018.Day_4
{
    class Day4
    {
        public static void Run()
        {
            var contents = ReadInputFile.ReadFile(4);
            var lines = ReadInputFile.SplitLines(contents);

            Dictionary<DateTime, string> records = new Dictionary<DateTime, string>();
            foreach (var line in lines)
            {
                var split = line.Split('[')[1].Split("] ");
                var time = DateTime.Parse(split[0]);
                records.Add(time, split[1]);
            }

            Dictionary<string, (List<int> Minutes, int Asleep)> guards = PerformActions(records);

            var part1 = Part1(guards);
            Console.WriteLine($"Part 1: {part1}");

            var part2 = Part2(guards);
            Console.WriteLine($"Part 2: {part2}");
        }

        private static int Part1(Dictionary<string, (List<int> Minutes, int Asleep)> guards)
        {
            var guardId = guards.OrderByDescending(kv => kv.Value.Asleep).FirstOrDefault().Key;
            var guard = guards[guardId];

            var guardNum = int.Parse(guardId.Split('#')[1]);
            var minute = guard.Minutes.GroupBy(m => m)
                                      .OrderByDescending(m => m.Count())
                                      .FirstOrDefault()
                                      .Key;

            return guardNum * minute;
        }

        private static int Part2(Dictionary<string, (List<int> Minutes, int Asleep)> guards)
        {
            var guard = guards.Select(kv => (GuardId: kv.Key,
                                             Minutes: kv.Value.Minutes
                                                              .GroupBy(m => m)
                                                              .OrderByDescending(m => m.Count())
                                                              .Select(m => (Count: m.Count(), Minute: m.Key))
                                                              .FirstOrDefault()))
                .OrderByDescending(g => g.Minutes.Count)
                .FirstOrDefault();

            var guardNum = int.Parse(guard.GuardId.Split('#')[1]);
            return guardNum * guard.Minutes.Minute;
        }

        private static Dictionary<string, (List<int> Minutes, int Asleep)> PerformActions(Dictionary<DateTime, string> records)
        {
            Dictionary<string, (List<int> Minutes, int Asleep)> guards = new Dictionary<string, (List<int> Minutes, int Asleep)>();

            var currentId = string.Empty;
            foreach (var key in records.Keys.OrderBy(r => r))
            {
                var action = ParseLine(records[key]);
                if (action == Action.Begin)
                {
                    currentId = records[key].Split().FirstOrDefault(l => l.StartsWith('#'));
                    if (!guards.ContainsKey(currentId))
                    {
                        guards.Add(currentId, (Minutes: new List<int>(), Asleep: 0));
                    }
                }
                else if (action == Action.Sleep)
                {
                    guards[currentId].Minutes.Add(key.Minute);
                }
                else
                {
                    var currentGuard = guards[currentId];
                    var last = currentGuard.Minutes.Last();
                    var minutes = key.Minute - last;

                    currentGuard.Asleep += minutes;
                    for (int i = last; i < key.Minute; i++)
                        currentGuard.Minutes.Add(i);

                    guards[currentId] = currentGuard;
                }
            }

            return guards;
        }

        private static Action ParseLine(string line)
        {
            if (line == "falls asleep")
                return Action.Sleep;
            else if (line == "wakes up")
                return Action.Wakeup;
            else
                return Action.Begin;
        }
    }
}
