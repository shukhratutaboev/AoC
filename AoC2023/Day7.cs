
public class Day7
{
    private static char[] _cards = new char[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'};
    public static void Part1()
    {
//         var input = @"32T3K 765
// T55J5 684
// KK677 28
// KTJJT 220
// QQQJA 483";

        var input = File.ReadAllText("day7.txt");

        var lines = input.Split("\r\n");

        var cards = lines.Select(c => {
            var card = c.Split(' ');
            return new Card {
                Cards = card[0],
                Strength = GetStrength(card[0]),
                Bid = int.Parse(card[1])
            };
        }).ToList();

        cards.Sort((a, b) => {
            if (a.Strength == b.Strength)
            {
                var i = 0;

                while (a.Cards[i] == b.Cards[i])
                {
                    i++;
                    if (i == 5)
                    {
                        return 0;
                    }
                }
                var aIndex = Array.IndexOf(_cards, a.Cards[i]);

                var bIndex = Array.IndexOf(_cards, b.Cards[i]);

                return -(aIndex - bIndex);
            }

            return a.Strength - b.Strength;
        });

        long total = 0;

        long length = 1;

        foreach (var card in cards)
        {
            total += card.Bid * length;
            length++;
        }

        Console.WriteLine(total);
    }

    private static int GetStrength(string v)
    {
        var dc = new Dictionary<char, int>();

        foreach (var c in v)
        {
            if (dc.ContainsKey(c))
            {
                dc[c]++;
            }
            else
            {
                dc[c] = 1;
            }
        }

        if (dc.Count == 1)
        {
            return 7;
        }

        if (dc.Count == 2 && dc.ContainsValue(4))
        {
            return dc.ContainsKey('J') ? 7 : 6;
        }

        if (dc.Count == 2 && dc.ContainsValue(3))
        {
            return dc.ContainsKey('J') ? 7 : 5;
        }

        if (dc.Count == 3 && dc.ContainsValue(3))
        {
            return dc.ContainsKey('J') ? 6 : 4;
        }

        if (dc.Count == 3 && dc.ContainsValue(2))
        {
            return dc.ContainsKey('J') ? dc['J'] == 2 ? 6 : 5 : 3;
        }

        if (dc.Count == 4)
        {
            return dc.ContainsKey('J') ? 4 : 2;
        }

        return 1;
    }

    class Card
    {
        public string Cards { get; set; }
        public int Strength { get; set; }
        public int Bid { get; set; }
    }
}