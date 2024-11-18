#region Input parsing
string text = File.ReadAllText("input.txt");
string[] map = text.Split(Environment.NewLine);

#endregion

#region Part1
long trees = Part1(map, 3, 1);

Console.WriteLine(trees);

static int Part1(string[] map, int dx, int dy)
{
    int trees = 0;
    int y = 0, x = 0;
    while (y < map.Length - 1)
    {
        x = x + dx;
        y = y + dy;
        // daca pe pozitia (x, y) este copac atunci creste <trees>
        if (map[y][x % map[y].Length] == '#')
            trees++;
    }

    return trees;
}
#endregion


#region Part2
List<(int dx, int dy)> slopes = new() { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
trees = 1;
foreach(var slope in slopes)
    trees = trees * Part1(map, slope.dx, slope.dy);
Console.WriteLine(trees);
#endregion
