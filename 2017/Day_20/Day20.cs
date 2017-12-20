using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2017.Day_20
{
    public static class Day20
    {
        public static void Run()
        {
            string input = "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>\np=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>";
            Part1(input);

            string actualInput = ReadInputFile.ReadFile("Input.txt", 20);
            Part1(actualInput);

            input = "p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>\np=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>\np=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>\np=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>";
            //Part2(input);

            Part2(actualInput);
        }

        private static void Part1(string input)
        {
            List<Particle> particles = ParseInput(input);

            int minAccelleration = particles.Select(p => p.TotalAcceleration()).Min();
            particles = particles.Where(p => p.TotalAcceleration() == minAccelleration).ToList();

            if (particles.Count > 1)
            {
                int minVelocity = particles.Select(p => p.TotalAcceleration()).Min();
                particles = particles.Where(p => p.TotalVelocity() == minAccelleration).ToList();

                if (particles.Count > 1)
                {
                    int minDistance = particles.Select(p => p.TotalDistance()).Min();
                    particles = particles.Where(p => p.TotalDistance() == minDistance).ToList();
                }
            }

            Console.WriteLine($"The closest particle will be {particles.First().Number}");
        }

        private static void Part2(string input)
        {
            List<Particle> particles = ParseInput(input);

            int numTimes = 0;
            int numRemoved = 0;
            int numNoCollissions = 0;
            while (numTimes < 5000 && numNoCollissions < 500)
            {
                particles.ForEach(p => p.Tick());

                List<int> particlesToRemove = new List<int>();
                for (int i = 0; i < particles.Count; i++)
                {
                    for (int j = i + 1; j < particles.Count; j++)
                    {
                        if (particles[i].Equals(particles[j]))
                        {
                            particlesToRemove.Add(i);
                            particlesToRemove.Add(j);
                        }
                    }
                }

                particlesToRemove = particlesToRemove.Distinct().OrderByDescending(i => i).ToList();
                if (particlesToRemove.Count == 0)
                    numNoCollissions++;
                else
                {
                    numRemoved += particlesToRemove.Count;
                    numNoCollissions = 0;
                    particlesToRemove.ForEach(i => particles.RemoveAt(i));
                }
                numTimes++;
            }

            Console.WriteLine($"Number of particles left: {particles.Count}");
        }

        private static List<Particle> ParseInput(string input)
        {
            string[] lines = ReadInputFile.SplitLines(input);
            List<Particle> particles = new List<Particle>();

            int number = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, @"<[^>]*>");
                Particle newParticle = new Particle();
                newParticle.Position = SplitCoordinates(matches[0].Value);
                newParticle.Velocity = SplitCoordinates(matches[1].Value);
                newParticle.Acceleration = SplitCoordinates(matches[2].Value);
                newParticle.Number = number;

                particles.Add(newParticle);
                number++;
            }

            return particles;
        }

        private static int[] SplitCoordinates(string input)
        {
            //skip < and >
            input = input.Substring(1, input.Length - 2);
            string[] split = input.Split(',');
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);
            int z = int.Parse(split[2]);

            return new int[] { x, y, z };
        }
    }
}
