public static class Day4
{
    public static void Part1()
    {
//         var input = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

        var input = File.ReadAllText("day4.txt");

        var texts = input.Split("\r\n").Select(t => t.Split(':')[1].Split('|').Select(s => s.Split(' ' ,StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray()).ToArray();
        var dp = new int[texts.Length];
        var sum = 0;
        for (var i = 0; i < texts.Length; i++)
        {
            var cardwins = texts[i][0].Intersect(texts[i][1]).ToArray().Length;
            dp[i]++;
            for (var j = i + 1; j < i + 1 + cardwins; j++)
            {
                dp[j] += dp[i];
            }

            sum += dp[i];
        }

        // sum += texts.Length;

        Console.WriteLine(sum);
    }

    public static int Part2()
    {
        return 0;
    }
}