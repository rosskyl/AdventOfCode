using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017.Day_20
{
    public class Particle
    {
        public int Number { get; set; }

        public int[] Position { get; set; }

        public int[] Velocity { get; set; }

        public int[] Acceleration { get; set; }

        public Particle()
        {
            Position = new int[3] { 0, 0, 0 };
            Velocity = new int[3] { 0, 0, 0 };
            Acceleration = new int[3] { 0, 0, 0 };
        }

        public int TotalDistance()
        {
            return Position.Sum(p => Math.Abs(p));
        }

        public int Distance(int time)
        {
            int distance = 0;
            for (int i = 0; i < Position.Length; i++)
            {
                int p = Position[i];
                int v = Velocity[i];
                int a = Acceleration[i];

                int d = p + v * time + a * time * (time + 1) / 2;
                distance += d;
            }

            return distance;
        }

        public int TotalAcceleration()
        {
            return Acceleration.Sum(a => Math.Abs(a));
        }

        public int TotalVelocity()
        {
            return Velocity.Sum(v => Math.Abs(v));
        }

        public void Tick(int time = 1)
        {
            for (int i = 0; i < time; i++)
            {
                for (int j = 0; j < Position.Length; j++)
                {
                    Velocity[j] += Acceleration[j];
                }

                for (int j = 0; j < Position.Length; j++)
                {
                    Position[j] += Velocity[j];
                }
            }
        }

        public bool Equals(Particle otherParticle)
        {
            for (int i = 0; i < Position.Length; i++)
            {
                if (Position[i] != otherParticle.Position[i])
                    return false;
            }
            return true;
        }
    }
}
