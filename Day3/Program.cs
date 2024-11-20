#region Input parsing
string text = File.ReadAllText("input.txt");
string[] map = text.Split(Environment.NewLine);
#endregion

#region Part1
long trees = Part1(map, (3, 1));

Console.WriteLine(trees);

static int Part1(string[] map, (int dx, int dy) slope)
{
    int trees = 0;
    int y = 0, x = 0;
    while (y < map.Length)
    {
        x = x + slope.dx;
        y = y + slope.dy;
        // daca pe pozitia (x, y) este copac atunci creste <trees>
        if (y < map.Length && map[y][x % 31] == '#')
            trees++;
    }

    return trees;
}
#endregion


#region Part2
List<(int dx, int dy)> slopes = new() { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
long result = 1;
foreach(var slope in slopes)
    result = result * Part1(map, slope);
Console.WriteLine(result);
#endregion
