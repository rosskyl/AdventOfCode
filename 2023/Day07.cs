using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day07
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(7);

            //Test input
            //lines = ReadInputFile.SplitLines("32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483");

            Part1(lines);
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            var hands = lines.Select(line => new Hand(line)).Order();
            var values = hands.Select((hand, index) => hand.Bid * (index + 1)).ToList();
            var total = values.Sum();

            Console.WriteLine($"Part 1: {total}");
        }

        public static void Part2(string[] lines)
        {
            var hands = lines.Select(line => new Hand2(line)).Order();
            var values = hands.Select((hand, index) => hand.Bid * (index + 1)).ToList();
            var total = values.Sum();

            Console.WriteLine($"Part 2: {total}");
        }
    }

    public class Hand : IComparable<Hand>
    {
        public string OriginalCards { get; set; }
        public List<int> Cards { get; set; }
        public int Bid { get; set; }
        public HandType Type { get; set; }

        public Hand(string line)
        {
            OriginalCards = line.Split(' ')[0];
            Bid = int.Parse(line.Split(' ')[1]);

            SetCards();
            SetHandType();
        }

        internal virtual void SetCards()
        {
            var cards = OriginalCards.ToCharArray();
            Cards = cards.Select(c =>
            {
                if (c == 'A')
                    return 14;
                else if (c == 'K')
                    return 13;
                else if (c == 'Q')
                    return 12;
                else if (c == 'J')
                    return 11;
                else if (c == 'T')
                    return 10;
                else
                    return int.Parse(c.ToString());
            }).ToList();
        }

        internal virtual void SetHandType()
        {
            var numCards = Cards.GroupBy(c => c).OrderByDescending(g => g.Count()).ToList();
            if (numCards.Count() == 1)
                Type = HandType.FiveOfKind;
            else if (numCards.Count() == 2 && numCards[0].Count() == 4)
                Type = HandType.FourOfKind;
            else if (numCards.Count() == 2)
                Type = HandType.FullHouse;
            else if (numCards.Count == 3 && numCards[0].Count() == 3)
                Type = HandType.ThreeOfKind;
            else if (numCards.Count == 3 && numCards[0].Count() == 2)
                Type = HandType.TwoPair;
            else if (numCards.Count == 5)
                Type = HandType.HighCard;
            else
                Type = HandType.OnePair;
        }

        public int CompareTo(Hand? other)
        {
            //Return -1 if this < other
            //Return 0 if this = other
            //Return 1 if this > other

            if (other == null) return 0;

            if (this.Type == other.Type)
            {
                for (int i = 0; i < this.Cards.Count; i++)
                {
                    if (this.Cards[i] != other.Cards[i])
                        return this.Cards[i].CompareTo(other.Cards[i]);
                }
                return 0;
            }
            else
                return this.Type.CompareTo(other.Type);
        }

        public override string ToString()
        {
            return $"{OriginalCards} {Bid}";
        }
    }

    public class Hand2 : Hand
    {
        public Hand2(string line) : base(line)
        {
        }

        internal override void SetCards()
        {
            var cards = OriginalCards.ToCharArray();
            Cards = cards.Select(c =>
            {
                if (c == 'A')
                    return 14;
                else if (c == 'K')
                    return 13;
                else if (c == 'Q')
                    return 12;
                else if (c == 'J')
                    return 1;
                else if (c == 'T')
                    return 10;
                else
                    return int.Parse(c.ToString());
            }).ToList();
        }

        internal override void SetHandType()
        {
            if (Cards.Any(c => c == 1))
            {
                var numCards = Cards.Where(c => c != 1).GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToList();
                var numJokers = Cards.Count(c => c == 1);
                if (numJokers == 5)
                {
                    Type = HandType.FiveOfKind;
                    return;
                }
                    
                numCards[0] = (numCards[0].Key, numCards[0].Item2 + numJokers);
                if (numCards.Count() == 1)
                    Type = HandType.FiveOfKind;
                else if (numCards.Count() == 2 && numCards[0].Item2 == 4)
                    Type = HandType.FourOfKind;
                else if (numCards.Count() == 2)
                    Type = HandType.FullHouse;
                else if (numCards.Count == 3 && numCards[0].Item2 == 3)
                    Type = HandType.ThreeOfKind;
                else if (numCards.Count == 3 && numCards[0].Item2 == 2)
                    Type = HandType.TwoPair;
                else if (numCards.Count == 5)
                    Type = HandType.HighCard;
                else
                    Type = HandType.OnePair;
            }
            else
            {
                base.SetHandType();
            }
        }
    }

    public enum HandType
    {
        FiveOfKind = 6,
        FourOfKind = 5,
        FullHouse = 4,
        ThreeOfKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }
}
