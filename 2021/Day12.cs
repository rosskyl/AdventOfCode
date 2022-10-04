using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2021
{
    public static class Day12
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(12);

            var tunnels = lines.Select(line => (line.Split('-')[0], line.Split('-')[1])).ToList();
            tunnels.AddRange(tunnels.ToList().Select(t => (t.Item2, t.Item1)));

            var paths = FindPaths(tunnels);

            var paths2 = FindPaths(tunnels, 2);

            Console.WriteLine($"Part 1: {paths.Count}");
            Console.WriteLine($"Part 1: {paths2.Count}");

        }

        private static List<List<string>> FindPaths(IEnumerable<(string start, string end)> tunnels, int numSmallCaveVisits = 1, List<string> currentPath = null)
        {
            var paths = new List<List<string>>();

            currentPath = currentPath?.ToList() ?? new List<string>() { "start" };

            var options = tunnels.Where(path => path.start == currentPath.Last())
                                  .Where(path => path.end.All(c => char.IsUpper(c)) || (currentPath.Count(cp => cp == path.end) < numSmallCaveVisits && path.end != "start"))
                                  .ToList();

            if (options.Count() == 0)
            {
                return null;
            }

            foreach (var option in options)
            {
                List<string> copyOfCurrent = currentPath.ToList();
                copyOfCurrent.Add(option.end);
                if (option.end == "end")
                {
                    paths.Add(copyOfCurrent);
                }
                else
                {
                    var smallTunels = copyOfCurrent.Distinct()
                                                   .Where(tunnel => tunnel.All(c => char.IsLower(c)))
                                                   .ToDictionary(tunnel => tunnel, tunnel => copyOfCurrent.Count(cp => cp == tunnel));

                    var isMultipleSmall = smallTunels.Values.Any(st => st == numSmallCaveVisits);
                    var childPaths = FindPaths(tunnels, isMultipleSmall ? 1 : numSmallCaveVisits, copyOfCurrent);
                    if (childPaths != null)
                        paths.AddRange(childPaths);
                }
            }


            return paths;
        }
    }
}
