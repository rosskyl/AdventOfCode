using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
    public static class Day05
    {
        public static void Run()
        {
            var lines = ReadInputFile.ReadAndSplitFile(5);
            Part1(lines);

            //lines = ReadInputFile.SplitLines("seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4");
            Part2(lines);
        }

        public static void Part1(string[] lines)
        {
            var almanac = new Almanac(lines);

            var lowestNumber = long.MaxValue;

            foreach (var seed in almanac.Seeds)
            {
                var location = almanac.MapSeedToLocation(seed);
                if (location < lowestNumber)
                    lowestNumber = location;
            }

            Console.WriteLine($"Part 1: {lowestNumber}");
        }

        public static void Part2(string[] lines)
        {
            Console.WriteLine(DateTime.Now);
            var almanac = new Almanac(lines);

            var lowestNumber = long.MaxValue;

            var seedPairs = almanac.Seeds
                                   .Select((value, index) => new { value, index })
                                   .GroupBy(pair => pair.index / 2)
                                   .Select(group => (group.First().value, group.Last().value));

            almanac.Seeds = new List<long>();
            foreach (var pair in seedPairs)
            {
                for (int i = 0; i < pair.Item2; i++)
                {
                    almanac.Seeds.Add(pair.Item1 + i);
                }
            }
            Console.WriteLine(DateTime.Now);

            Parallel.ForEach(almanac.Seeds, seed =>
            {
                var location = almanac.MapSeedToLocation(seed);
                if (location < lowestNumber)
                    lowestNumber = location;
            });
            Console.WriteLine(DateTime.Now);
            Console.WriteLine($"Part 2: {lowestNumber}");
        }
    }

    public class Almanac
    {
        public List<long> Seeds { get; set; }
        public List<Map> SeedsToSoil { get; set; }
        public List<Map> SoilToFertilizer { get; set; }
        public List<Map> FertilizerToWater { get; set; }
        public List<Map> WaterToLight { get; set; }
        public List<Map> LightToTemperature { get; set; }
        public List<Map> TemperatureToHumidity { get; set; }
        public List<Map> HumidityToLocation { get; set; }

        public Almanac(string[] lines)
        {
            SeedsToSoil = new List<Map>();
            SoilToFertilizer = new List<Map>();
            FertilizerToWater = new List<Map>();
            WaterToLight = new List<Map>();
            LightToTemperature = new List<Map>();
            TemperatureToHumidity = new List<Map>();
            HumidityToLocation = new List<Map>();

            var currentMap = SeedsToSoil;
            foreach (var line in lines)
            {
                if (line.StartsWith("seeds"))
                    Seeds = line.Replace("seeds:", "").Split(' ').Where(seed => !string.IsNullOrEmpty(seed)).Select(seed => long.Parse(seed)).ToList();
                else if (string.IsNullOrWhiteSpace(line))
                    continue;
                else if (line.Contains("map"))
                    switch (line)
                    {
                        case "seed-to-soil map:":
                            currentMap = SeedsToSoil; break;
                        case "soil-to-fertilizer map:":
                            currentMap = SoilToFertilizer; break;
                        case "fertilizer-to-water map:":
                            currentMap = FertilizerToWater; break;
                        case "water-to-light map:":
                            currentMap = WaterToLight; break;
                        case "light-to-temperature map:":
                            currentMap = LightToTemperature; break;
                        case "temperature-to-humidity map:":
                            currentMap = TemperatureToHumidity; break;
                        case "humidity-to-location map:":
                            currentMap = HumidityToLocation; break;
                        default:
                            throw new Exception("No map found");
                    }
                else
                {
                    currentMap.Add(new Map(line));
                }
            }
        }

        public long MapSeedToLocation(long seed)
        {
            seed = MapNumber(seed, SeedsToSoil);
            seed = MapNumber(seed, SoilToFertilizer);
            seed = MapNumber(seed, FertilizerToWater);
            seed = MapNumber(seed, WaterToLight);
            seed = MapNumber(seed, LightToTemperature);
            seed = MapNumber(seed, TemperatureToHumidity);
            seed = MapNumber(seed, HumidityToLocation);
            return seed;
        }

        private long MapNumber(long number, List<Map> maps)
        {
            var mapToUse = maps.FirstOrDefault(map => map.InRange(number));

            if (mapToUse == null)
                return number;
            else
                return mapToUse.MapNumber(number);
        }
    }

    public class Map
    {
        public long Destination { get; set; }

        public long Source { get; set; }

        public long Length { get; set; }

        public Map(string line)
        {
            var numbers = line.Split(' ').Select(num => long.Parse(num)).ToList();
            Destination = numbers[0];
            Source = numbers[1];
            Length = numbers[2];
        }

        public bool InRange(long number)
        {
            return Source <= number && number <= Source + Length - 1;
        }

        public long MapNumber(long number)
        {
            return Destination + (number - Source);
        }
    }
}
