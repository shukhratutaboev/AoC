public class Day10 {
    private static Dictionary<char, int[]> dict = new Dictionary<char, int[]>() {
        { '|', new int[] { 1, 0, -1, 0 } },
        { '-', new int[] { 0, 1, 0, -1 } },
        { 'L', new int[] { 0, 1, -1, 0 } },
        { 'J', new int[] { 0, -1, -1, 0 } },
        { '7', new int[] { 0, -1, 1, 0 } },
        { 'F', new int[] { 0, 1, 1, 0 } },
        { '.', new int[] { 0, 0, 0, 0 } }
    };
    public static void Part1() {
        var input = File.ReadAllText("input.txt");

//         var input = @".F----7F7F7F7F-7....
// .|F--7||||||||FJ....
// .||.FJ||||||||L7....
// FJL7L7LJLJ||LJ.L-7..
// L--J.L7...LJS7F-7L7.
// ....F-J..F7FJ|L7L7L7
// ....L7.F7||L7|.L7L7|
// .....|FJLJ|FJ|F7|.LJ
// ....FJL-7.||.||||...
// ....L---J.LJ.LJLJ...";

        var maze = input.Split("\r\n").Select(a => a.ToArray()).ToArray();

        var nodes = new Dictionary<(int, int), Node>();

        for (int i = 0; i < maze.Length; i++) {
            for (int j = 0; j < maze[i].Length; j++) {
                nodes.TryAdd((i, j), new Node(i, j, maze[i][j]));
                var node = nodes[(i, j)];

                if (node.Value == '.' || node.Value == 'S') {
                    continue;
                }

                var neighbor1 = (i + dict[node.Value][0], j + dict[node.Value][1]);
                var neighbor2 = (i + dict[node.Value][2], j + dict[node.Value][3]);

                if (!nodes.ContainsKey(neighbor1) && IsInMaze(neighbor1, maze)) {
                    nodes.TryAdd(neighbor1, new Node(neighbor1.Item1, neighbor1.Item2, maze[neighbor1.Item1][neighbor1.Item2]));
                }

                if (!nodes.ContainsKey(neighbor2) && IsInMaze(neighbor2, maze)) {
                    nodes.TryAdd(neighbor2, new Node(neighbor2.Item1, neighbor2.Item2, maze[neighbor2.Item1][neighbor2.Item2]));
                }

                if (!nodes.ContainsKey(neighbor1) || !nodes.ContainsKey(neighbor2)) {
                    continue;
                }
                node.Neighbors.Add(nodes[neighbor1]);
                node.Neighbors.Add(nodes[neighbor2]);
            }
        }

        var start = nodes.First(x => x.Value.Value == 'S').Value;

        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                var neighbor = (start.X + i, start.Y + j);

                if (!nodes.ContainsKey(neighbor) || nodes[neighbor].Value == '.')
                {
                    continue;
                }

                start.Neighbors.Add(nodes[neighbor]);
            }
        }

        foreach (var node in nodes.Values) {
            var newNeighbors = new HashSet<Node>();
            foreach (var neighbor in node.Neighbors) {
                if (neighbor.Neighbors.Contains(node)) {
                    newNeighbors.Add(neighbor);
                }
            }
            node.Neighbors = newNeighbors;
        }

        var longest = 0;

        var step = 1;

        var nn = start.Neighbors.First();
        var pp = start;

        var loop = new HashSet<(int, int)>();

        while (true) {
            var next = nn.Neighbors.FirstOrDefault(x => x != pp);

            pp = nn;
            nn = next;
            step++;

            loop.Add((nn.X, nn.Y));

            if (nn == null) {
                step = 0;
                break;
            }

            if (nn == start) {
                break;
            }
        }

        loop.Add((start.Neighbors.First().X , start.Neighbors.First().Y));

        Console.WriteLine($"Part 1: {step / 2}");

        if (step > longest) {
            longest = step;
        }
        var count = 0;
        for (var i = 1; i < maze.Length; i++)
        {
            var inLoop = false;
            var qq = 0;
            var preb = 1;
            for (var j = 0; j < maze[i].Length - 1; j++)
            {
                if (loop.Contains((i, j)))
                {
                    var node = nodes[(i, j)];

                    if (node.Value is 'L' or 'F' or 'S')
                    {
                        qq++;
                        preb = node.Value == 'L' ? 1 : -1;
                    }

                    if (node.Value is '7' or 'J')
                    {
                        preb *= node.Value == '7' ? 1 : -1;
                        qq += preb;
                    }

                    if (node.Value == '|' || qq == 2)
                    {
                        inLoop = !inLoop;
                        qq = 0;
                    }
                }
                else if (inLoop)
                {
                    count++;
                }
            }
        }
        
        Console.WriteLine($"Part 2: {count}");
    }

    static bool IsInMaze((int x, int y) xy, char[][] maze) {
        return xy.x >= 0 && xy.x < maze.Length && xy.y >= 0 && xy.y < maze[0].Length;
    }

    class Node {
        public int X { get; set; }
        public int Y { get; set; }
        public char Value { get; set; }
        public HashSet<Node> Neighbors { get; set; } = new ();
        public Node(int x, int y, char value) {
            X = x;
            Y = y;
            Value = value;
        }
    }
}