using Day20;
using System.Text.RegularExpressions;
#region Input parsing
string text = File.ReadAllText("input.txt");
string[] t = text.Split("\r\n\r\n");
Regex r = new Regex(@"\d+");
List<Tile> data = new();
int TILESIZE = 0;
foreach(var item in t)
{
    string[] t2 = item.Split(":\r\n");
    int id = int.Parse(r.Match(t2[0]).Value);
    string[] lines = t2[1].Split(Environment.NewLine);
    TILESIZE = lines.Length;
    char[,] m = new char[lines.Length, lines[0].Length];
    for (int i = 0; i < lines.Length; i++)
        for (int j = 0; j < lines[0].Length; j++)
            m[i, j] = lines[i][j];
    data.Add(new Tile() { Id = id, OrigImage = m });
}


#endregion

#region Part1
long r1 = 1;
Dictionary<int, int> p1 = new();
HashSet<int> allNums = new HashSet<int>();
foreach(Tile tile in data)
{
    int up1 = 0, down1 = 0, left1 = 0, right1 = 0;
    for (int k = 0; k < TILESIZE; k++)
    {
        up1 = up1 * 2 + (tile.OrigImage[0, k] == '.' ? 0 : 1);
        down1 = down1 * 2 + (tile.OrigImage[TILESIZE - 1, k] == '.' ? 0 : 1);
        left1 = left1 * 2 + (tile.OrigImage[k, 0] == '.' ? 0 : 1);
        right1 = right1 * 2 + (tile.OrigImage[k, TILESIZE - 1] == '.' ? 0 : 1);
    }
    int up2 = 0, down2 = 0, left2 = 0, right2 = 0;
    for (int k = TILESIZE - 1; k >= 0; k--)
    {
        up2 = up2 * 2 + (tile.OrigImage[0, k] == '.' ? 0 : 1);
        down2 = down2 * 2 + (tile.OrigImage[TILESIZE - 1, k] == '.' ? 0 : 1);
        left2 = left2 * 2 + (tile.OrigImage[k, 0] == '.' ? 0 : 1);
        right2 = right2 * 2 + (tile.OrigImage[k, TILESIZE - 1] == '.' ? 0 : 1);
    }
    if (p1.ContainsKey(up1))  p1[up1]++;
    else p1[up1] = 1;

    if (p1.ContainsKey(down1)) p1[down1]++;
    else p1[down1] = 1;

    if (p1.ContainsKey(left1)) p1[left1]++;
    else p1[left1] = 1;

    if (p1.ContainsKey(right1)) p1[right1]++;
    else p1[right1] = 1;

    if (p1.ContainsKey(up2)) p1[up2]++;
    else p1[up2] = 1;

    if (p1.ContainsKey(down2)) p1[down2]++;
    else p1[down2] = 1;

    if (p1.ContainsKey(left2)) p1[left2]++;
    else p1[left2] = 1;

    if (p1.ContainsKey(right2)) p1[right2]++;
    else p1[right2] = 1;
    allNums.Add(up1); allNums.Add(down1); allNums.Add(left1); allNums.Add(right1);
    allNums.Add(up2); allNums.Add(down2); allNums.Add(left2); allNums.Add(right2);
    tile.SideNums = new HashSet<int>(new List<int>() { up1, down1, left1, right1, up2, down2, left2, right2 });

}

foreach(Tile tile in data)
{
    foreach (var n in tile.SideNums)
        if (p1[n] == 2)
            tile.MatchingSides++;
    if(tile.MatchingSides == 4)
    {
        Console.WriteLine(tile.Id);
        r1 *= tile.Id;
    }
}
Console.WriteLine(r1);
#endregion

#region Part2
#endregion