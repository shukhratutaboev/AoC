public class Day11
{
    public static void Part1()
    {
//         var input = @"...#......
// .......#..
// #.........
// ..........
// ......#...
// .#........
// .........#
// ..........
// .......#..
// #...#.....";

        var input = File.ReadAllText("day11.txt");

        var lines = input.Split("\r\n").Select(c => c.ToList()).ToList();

        var width = lines[0].Count;
        var height = lines.Count;

        var ec = new bool[width];
        var er = new bool[height];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (lines[i][j] == '#')
                {
                    ec[j] = true;
                    er[i] = true;
                }
            }
        }

        // var ra = 0;
        // for (var i = 0; i < height; i++)
        // {
        //     if (!er[i])
        //     {
        //         ra++;
        //         lines.Insert(i + ra, GetEmptyList(width));
        //     }
        // }

        // var ca = 0;

        // for (var i = 0; i < width; i++)
        // {
        //     if (!ec[i])
        //     {
        //         ca++;
        //         foreach (var line in lines)
        //         {
        //             line.Insert(i + ca, '.');
        //         }
        //     }
        // }

        var cords = new List<(int, int)>();

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (lines[i][j] == '#')
                {
                    cords.Add((i, j));
                }
            }
        }

        long sum = 0;

        for (int i = 0; i < cords.Count; i++)
        {
            for (int j = i+1; j < cords.Count; j++)
            {
                var maxI = Math.Max(cords[i].Item1, cords[j].Item1);
                var minI = Math.Min(cords[i].Item1, cords[j].Item1);

                var maxJ = Math.Max(cords[i].Item2, cords[j].Item2);
                var minJ = Math.Min(cords[i].Item2, cords[j].Item2);

                var exI = 0L;
                var exJ = 0L;

                for (var k = minI + 1; k < maxI; k++)
                {
                    if (!er[k])
                    {
                        exI++;
                    }
                }

                for (var k = minJ + 1; k < maxJ; k++)
                {
                    if (!ec[k])
                    {
                        exJ++;
                    }
                }

                sum += maxI - minI + maxJ - minJ + (exI + exJ) * 1;
            }
        }

        Console.WriteLine(sum);
    }

    static List<char> GetEmptyList(int width)
    {
        var list = new List<char>();

        for (var i = 0; i < width; i++)
        {
            list.Add('.');
        }

        return list;
    }

    static void Print(List<List<char>> lines)
    {
        foreach (var line in lines)
        {
            Console.WriteLine(string.Join("", line));
        }
    }
}