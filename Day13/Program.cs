#region Input parsing
string text = File.ReadAllText("input.txt");
string[] t = text.Split(Environment.NewLine);
int time = int.Parse(t[0]);
t = t[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
List<int> busIDs = new List<int>();
int busid;
foreach (var item in t)
{
    bool r = int.TryParse(item, out busid);
    if(r)
        busIDs.Add(busid);
}
#endregion

#region Part1
List<int> lst = new List<int>();
foreach(var item in busIDs)
{
    int q = time / item * item;
    while (q < time)
        q += item;
    lst.Add(q);
}
int r1 = lst.Min(x => x - time);
for(int i = 0; i < lst.Count; i++)
{
    if (lst[i] - time == r1)
    {
        r1 = r1 * busIDs[i];
        break;
    }
}
Console.WriteLine(r1);
#endregion

#region Part2
List<(long value, long mod)> crt = new();
for(int i = 0; i < t.Length;i++)
{
    bool r = int.TryParse(t[i], out busid);
    if (r)
    {
        int q = busid - i;
        while (q < 0)
            q += busid;
        crt.Add((q%busid, busid));
    }
}
foreach(var item in crt)
    Console.WriteLine($"x = {item.value} mod {item.mod}");
Console.WriteLine();
#endregion