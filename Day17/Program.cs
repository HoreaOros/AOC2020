#region Input parsing
using System.Collections.Specialized;
using System.Security.Cryptography;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
int TIMES = 6;
int offsetI = 15, offsetJ = 15, offsetK = 15, offsetT = 15;
int[,,] map = new int[30, 30, 30];
int[,,,] map4 = new int[30, 30, 30, 30];

for (int i = 0; i < lines.Length; i++)
    for (int j = 0; j < lines[i].Length; j++)
        if (lines[i][j] == '#')
        {
            map[i + offsetI, j+offsetJ, offsetK] = 1;
            map4[i + offsetI, j + offsetJ, offsetK, offsetT] = 1;
        }
#endregion

#region Part1
int[] di = {1, 1,  1,  1, 1,  1, 1,  1,  1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, 0,  0, 0,  0, 0,  0,  0,  0};
int[] dj = {1, 1, -1, -1, 0,  0, 0,  1, -1,  1,  1, -1, -1,  0,   0,  0,  1, -1, 1, -1, 0,  0, 1,  1, -1, -1};
int[] dk = {1,-1,  1, -1, 0, -1, 1,  0,  0,  1, -1,  1, -1,  0,  -1,  1,  0,  0, 0,  0, 1, -1, 1, -1,  1, -1};
for(int times = 0; times < TIMES; times++)
{
    //int[,,] newMap = DeepCopy(map);
    int[,,] newMap = new int[map.GetLength(0), map.GetLength(1), map.GetLength(2)];
    for (int i = 0; i < map.GetLength(0); i++)
        for (int j = 0; j < map.GetLength(1); j++)
            for (int k = 0; k < map.GetLength(2); k++)
            {
                int count = 0;
                for (int t = 0; t < di.Length; t++)
                {
                    int ni = i + di[t];
                    int nj = j + dj[t];
                    int nk = k + dk[t];
                    if (ni >= 0 && nj >= 0 && nk >= 0 && ni < map.GetLength(0) && nj < map.GetLength(1) && nk < map.GetLength(2))
                        if (map[ni, nj, nk] == 1)
                            count++;
                }
                if (map[i, j, k] == 1)
                    if (count == 2 || count == 3)
                        newMap[i, j, k] = 1;
                    else
                        newMap[i, j, k] = 0;
                else
                    if (count == 3)
                        newMap[i, j, k] = 1;
                    else
                        newMap[i, j, k] = 0;
            }
    map = newMap; 
}

int r1 = 0;
for (int i = 0; i < map.GetLength(0); i++)
    for (int j = 0; j < map.GetLength(1); j++)
        for (int k = 0; k < map.GetLength(2); k++)
            if (map[i, j, k] == 1)
                r1++;
Console.WriteLine(r1);


int[,,] DeepCopy(int[,,] map)
{
    int[,,] newMap = new int[map.GetLength(0), map.GetLength(1), map.GetLength(2)];
    for(int i = 0; i <map.GetLength(0); i++)
        for(int j = 0; j < map.GetLength(1); j++)
            for(int k = 0; k < map.GetLength(2); k++)
                newMap[i, j, k] = map[i, j, k];
    return newMap;
}
#endregion

#region Part2
for (int times = 0; times < TIMES; times++)
{
    //int[,,] newMap = DeepCopy(map);
    int[,,,] newMap4 = new int[map4.GetLength(0), map4.GetLength(1), map4.GetLength(2), map4.GetLength(3)];
    for (int i = 0; i < map4.GetLength(0); i++)
        for (int j = 0; j < map4.GetLength(1); j++)
            for (int k = 0; k < map4.GetLength(2); k++)
                for (int t = 0; t < map4.GetLength(3); t++)
                {
                    int[] v = { i, j, k, t };
                    int[] orig = { i, j, k, t };

                    int count = 0;
                    F(map4, v, orig, 0, ref count);
                    if (map4[i, j, k, t] == 1)
                        if (count == 2 || count == 3)
                            newMap4[i, j, k, t] = 1;
                        else
                            newMap4[i, j, k, t] = 0;
                    else
                    if (count == 3)
                        newMap4[i, j, k, t] = 1;
                    else
                        newMap4[i, j, k, t] = 0;
                }
    map4 = newMap4;
}

void F(int[,,,] map4, int[] v, int[] orig, int k, ref int count)
{
    if(k == 4)
    {
        bool same = true;
        for(int i = 0; i < v.Length; i++)
            if (v[i] != orig[i])
                same = false;
        if (!same)
        {
            if (v.All(x => x >= 0 && x < 30) && map4[v[0], v[1], v[2], v[3]] == 1)
                count++;
            //for(int i = 0; i < 4; i++)
            //    Console.Write(v[i]);
            //Console.WriteLine();
        }
    }
    else
    {
        // 0
        F(map4, v, orig, k + 1, ref count);
        
        // +1
        v[k]++;
        F(map4, v, orig, k + 1, ref count);
        v[k]--;

        // -1
        v[k]--;
        F(map4, v, orig, k + 1, ref count);
        v[k]++;
    }
}

int r2 = 0;
for (int i = 0; i < map4.GetLength(0); i++)
    for (int j = 0; j < map4.GetLength(1); j++)
        for (int k = 0; k < map4.GetLength(2); k++)
          for (int t = 0; t < map4.GetLength(3); t++)
            if (map4[i, j, k, t] == 1)
                r2++;
Console.WriteLine(r2);
#endregion