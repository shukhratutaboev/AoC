public class Day6
{
    public static void Part1()
    {
        var input = @"Time:        53     89     76     98
Distance:   313   1090   1214   1201";

        var lines = input.Split("\n");

        var times = lines[0].Split(":")[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var distances = lines[1].Split(":")[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        long m = 1;

        for (var i = 0; i < times.Length; i++)
        {
            var result = SolveQuadratic(-1, times[i], -distances[i]);

            if (result.Item1.HasValue && result.Item2.HasValue)
            {
                // find number of integers between the two roots
                var root1 = result.Item1.Value;
                var root2 = result.Item2.Value;

                var min = Math.Min(root1, root2);

                var max = Math.Max(root1, root2);

                var cmin = Math.Ceiling(min);
                var cmax = Math.Floor(max);

                var count = (int)(cmax - cmin + 1);

                if (cmin == min) count--;
                if (cmax == max) count--;

                m *= count;
            }
        }

        Console.WriteLine(m);
    }

    public static void Part2()
    {
        var input = @"Time:        53     89     76     98
Distance:   313   1090   1214   1201";

        var lines = input.Split("\n");

        var time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
        var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

        var result = SolveQuadratic(-1, time, -distance);

        if (result.Item1.HasValue && result.Item2.HasValue)
        {
            // find number of integers between the two roots
            var root1 = result.Item1.Value;
            var root2 = result.Item2.Value;

            var min = Math.Min(root1, root2);

            var max = Math.Max(root1, root2);

            var cmin = Math.Ceiling(min);
            var cmax = Math.Floor(max);

            var count = (int)(cmax - cmin + 1);

            if (cmin == min) count--;
            if (cmax == max) count--;

            Console.WriteLine(count);
        }

        Console.WriteLine(0);
    }

    public static (double?, double?) SolveQuadratic(double a, double b, double c)
    {
        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            return (null, null);
        }

        var sqrt = Math.Sqrt(discriminant);
        var denominator = 2 * a;

        return ((-b + sqrt) / denominator, (-b - sqrt) / denominator);
    }
}