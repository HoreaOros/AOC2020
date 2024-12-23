#region Input parsing
using System;
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
string[] t = text.Split("\r\n\r\n");
Regex r = new Regex(@"\d+");
List<Tile> data = new();
Dictionary<int, List<Tile>> tiles = new();
int TILESIZE = 0;
int SIZE = 0;
foreach (var item in t)
{
    string[] t2 = item.Split(":\r\n");
    int id = int.Parse(r.Match(t2[0]).Value);
    string[] lines = t2[1].Split(Environment.NewLine);
    TILESIZE = lines.Length;
    char[,] m = new char[lines.Length, lines[0].Length];
    for (int i = 0; i < lines.Length; i++)
        for (int j = 0; j < lines[0].Length; j++)
            m[i, j] = lines[i][j];
    Tile tile = new Tile() { Id = id, OrigImage = m };
    data.Add(tile);
    tiles[id] = new List<Tile>() { tile }; 
    for (int i = 0; i < 3; i++)
        tiles[id].Add(tiles[id][i].RotateRight());
    tiles[id].Add(tiles[id][0].FlipHorizontally());
    for (int i = 4; i < 7; i++)
        tiles[id].Add(tiles[id][i].RotateRight());
}
SIZE = (int)Math.Sqrt(tiles.Count());
#endregion

#region Part1
Dictionary<string, int> d = new();
foreach(var kv in tiles)
{
    foreach(Tile tile in kv.Value)
    {
        if (d.ContainsKey(tile.Left)) 
            d[tile.Left]++;
        else 
            d[tile.Left] = 1;
        if (d.ContainsKey(tile.Right)) 
            d[tile.Right]++;
        else 
            d[tile.Right] = 1;
        if (d.ContainsKey(tile.Up)) 
            d[tile.Up]++;
        else 
            d[tile.Up] = 1;
        if (d.ContainsKey(tile.Down))
            d[tile.Down]++;
        else 
            d[tile.Down] = 1;
    }
}

int leftCorner = 0;
HashSet<long> corners = new ();
foreach (var kv in tiles)
{
    foreach(Tile tile in kv.Value)
    {
        if (d[tile.Left] == 4 && d[tile.Up] == 4)
                corners.Add(kv.Key);
    }
}

// 20899048083289 test
Console.WriteLine(corners.Aggregate(1L, (total, next) => total * next));
#endregion

#region Part2
Tile cornerTile = null;
foreach (var kv in tiles)
{
    foreach (Tile tile in kv.Value)
    {
        if (d[tile.Left] == 4 && d[tile.Up] == 4)
            cornerTile = tile;
    }
}

Tile[,] image = new Tile[SIZE, SIZE];

//First Line
image[0, 0] = cornerTile;
for(int i = 1; i < SIZE; i++)
    foreach (var kv in tiles)
        foreach (Tile tile in kv.Value)
            if(tile.Id != image[0, i - 1].Id)
                if (tile.Left == image[0, i - 1].Right)
                    image[0, i] = tile;

// Next Lines
for (int i = 1; i < SIZE; i++)
    for(int j = 0; j < SIZE; j++)
        foreach (var kv in tiles)
            foreach (Tile tile in kv.Value)
                if (tile.Id != image[i-1, j].Id)
                    if (tile.Up == image[i-1, j].Down)
                        image[i, j] = tile;


// RemoveBorders;
for (int i = 0; i < SIZE; i++)
    for (int j = 0; j < SIZE; j++)
        image[i, j].RemovBorders();

TILESIZE -= 2;

char[,] img = new char[SIZE * TILESIZE, SIZE * TILESIZE];
for (int i = 0; i < SIZE; i++)
    for (int j = 0; j < SIZE; j++)
        for (int a = 0; a < TILESIZE; a++)
            for (int b = 0; b < TILESIZE; b++)
                img[i * TILESIZE + a, j * TILESIZE + b] = image[i, j].OrigImage[a, b];

//DisplayImage(img);


// look for sea monsters
string[] seaMonster = {
"                  # ",
"#    ##    ##    ###",
" #  #  #  #  #  #   " };

HashSet<(int r, int c)> monster = new();
for(int i = 0; i < seaMonster.Length; i++)
    for(int j = 0; j < seaMonster[0].Length; j++)
        if(seaMonster[i][j] == '#')
            monster.Add((i, j));

Tile actualImage = new Tile() { Id = 0, OrigImage = img};


List<Tile> images = new List<Tile>() { actualImage};
for (int i = 0; i < 3; i++)
    images.Add(images[i].RotateRight());
images.Add(images[0].FlipHorizontally());
for (int i = 4; i < 7; i++)
    images.Add(images[i].RotateRight());

char[,] seaMonsterImage = null;

HashSet<(int i, int j)> seaMonsterPositions = new();

for (int i = 0; i < images.Count; i++)
{
    //DisplayImage(images[i].OrigImage);
    if (ContainSeaMonster(images[i].OrigImage, seaMonster))
    {
        seaMonsterImage = images[i].OrigImage;
        //Console.WriteLine($"{i} contains sea monsters");
        break;
    }
}


HashSet<(int i, int j)> hashMonsters = new();
foreach(var item in seaMonsterPositions)
{
    for (int a = 0; a < seaMonster.Length; a++)
        for (int b = 0; b < seaMonster[0].Length; b++)
            if (seaMonster[a][b] == '#')
                hashMonsters.Add((item.i + a, item.j+b));
}
HashSet<(int i, int j)> hashAll = new();
for (int i = 0; i < seaMonsterImage.GetLength(0); i++)
    for (int j = 0; j < seaMonsterImage.GetLength(1); j++)
        if (seaMonsterImage[i, j] == '#')
            hashAll.Add((i, j));
Console.WriteLine(hashAll.Count - hashMonsters.Count);


Console.WriteLine();
bool ContainSeaMonster(char[,] img, string[] monster)
{
    bool result = false;
    for (int i = 0; i < img.GetLength(0) - monster.Length; i++)
        for (int j = 0; j < img.GetLength(1) - monster[0].Length; j++)
        {
            bool found = true;
            for (int a = 0; found && a < monster.Length; a++)
                for (int b = 0; found && b < monster[0].Length; b++)
                    if (monster[a][b] == '#' && img[i + a, j + b] != '#')
                        found = false;
            if (found)
            {
                seaMonsterPositions.Add((i, j));
                result = true;
            }
        }
    return result;
}

static void DisplayImage(char[,] img)
{
    for (int i = 0; i < img.GetLength(0); i++)
    {
        for (int j = 0; j < img.GetLength(1); j++)
            Console.Write(img[i, j]);
        Console.WriteLine();
    }

    Console.WriteLine();
}

#endregion