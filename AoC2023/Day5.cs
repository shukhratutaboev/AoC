public class Day5
{
    public static void Part1()
    {
        var input = File.ReadAllText("day5.txt");

//         var input = @"seeds: 79 14 55 13

// seed-to-soil map:
// 50 98 2
// 52 50 48

// soil-to-fertilizer map:
// 0 15 37
// 37 52 2
// 39 0 15

// fertilizer-to-water map:
// 49 53 8
// 0 11 42
// 42 0 7
// 57 7 4

// water-to-light map:
// 88 18 7
// 18 25 70

// light-to-temperature map:
// 45 77 23
// 81 45 19
// 68 64 13

// temperature-to-humidity map:
// 0 69 1
// 1 0 69

// humidity-to-location map:
// 60 56 37
// 56 93 4";

        var lines = input.Split("\r\n");

        var seedSoil = new List<long[]>();
        var soilFert = new List<long[]>();
        var fertWater = new List<long[]>();
        var waterLight = new List<long[]>();
        var lightTemp = new List<long[]>();
        var tempHumid = new List<long[]>();
        var humidLocation = new List<long[]>();

        var seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("seed-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    seedSoil.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("soil-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    soilFert.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("fertilizer-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    fertWater.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("water-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    waterLight.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("light-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    lightTemp.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("temperature-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    tempHumid.Add(line);
                    i++;
                } while (lines[i] != "");
            }
            else if (lines[i].StartsWith("humidity-to"))
            {
                i++;
                do {
                    var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    humidLocation.Add(line);
                    i++;
                } while (i < lines.Length && lines[i] != "");
            }
        }

        var minLocation = long.MaxValue;

        // foreach (var seed in seeds)
        // {
        //     var soil = FindMap(seedSoil, seed);
        //     var fertilizer = FindMap(soilFert, soil);
        //     var water = FindMap(fertWater, fertilizer);
        //     var light = FindMap(waterLight, water);
        //     var temperature = FindMap(lightTemp, light);
        //     var humidity = FindMap(tempHumid, temperature);
        //     var location = FindMap(humidLocation, humidity);

        //     if (location < minLocation)
        //     {
        //         minLocation = location;
        //     }
        // }

        for (var i = 0; i < seeds.Length / 2; i++)
        {
            var start = seeds[2*i];
            var end = start + seeds[2*i + 1];

            for (var j = start; j < end; j++)
            {
                var soil = FindMap(seedSoil, j);
                var fertilizer = FindMap(soilFert, soil);
                var water = FindMap(fertWater, fertilizer);
                var light = FindMap(waterLight, water);
                var temperature = FindMap(lightTemp, light);
                var humidity = FindMap(tempHumid, temperature);
                var location = FindMap(humidLocation, humidity);

                if (location < minLocation)
                {
                    minLocation = location;
                }
            }
        }

        Console.WriteLine(minLocation);
    }

    static long FindMap(List<long[]> map, long value)
    {
        for (var i = 0; i < map.Count; i++)
        {
            if (map[i][1] <= value && value < map[i][1] + map[i][2])
            {
                return map[i][0] + value - map[i][1];
            }
        }

        return value;
    }

    public static void Part2()
    {
        var input = File.ReadAllText("day5.txt");
        var lines = input.Split("\r\n");
        
    }
}