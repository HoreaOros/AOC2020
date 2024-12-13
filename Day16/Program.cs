#region Input parsing
using System.Text.RegularExpressions;


int FIELDS = 20;
string text = File.ReadAllText("input.txt");
string[] t = text.Split("\r\n\r\n");
string[] s = t[0].Split(Environment.NewLine);
Regex r = new Regex(@"([a-z ]+): (\d+)-(\d+) or (\d+)-(\d+)");
Dictionary<string, ((int l, int r), (int l, int r))> dict = new ();
foreach (string item in s)
{
    Match m = r.Match(item);
    dict[m.Groups[1].Value] =
        ((int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value)),
        (int.Parse(m.Groups[4].Value), int.Parse(m.Groups[5].Value)));
}
#endregion
//foreach(var item in dict.Keys)
//{
//    Console.WriteLine($"{item}: ({dict[item].Item1.l},{dict[item].Item1.r}) or ({dict[item].Item2.l},{dict[item].Item2.r})");
//}
s = t[1].Split(Environment.NewLine);
s = s[1].Split(",");
List<int> myTicket = new List<int>();
foreach (string num in s)
    myTicket.Add(int.Parse(num));

List<List<int>> nearbyTickets = new();
s = t[2].Split(Environment.NewLine);
for(int i = 1; i < s.Length; i++)
{
    string[] tok = s[i].Split(",");
    List<int> ticks = new List<int>(tok.Select(x => int.Parse(x)));
    nearbyTickets.Add(ticks);
}

#region Part1
long sum = 0;
List<List<int>> goodTickets = new();
for (int i = 0; i < nearbyTickets.Count; i++)
{
    bool valid = true;
    foreach(var item in nearbyTickets[i])
    {
        if (!validValue(item, dict))
        {
            sum += item;
            valid = false;
        }
    }
    if(valid)
        goodTickets.Add(nearbyTickets[i]);
}

bool validValue(int item, Dictionary<string, ((int l, int r), (int l, int r))> dict)
{
    foreach(var k in dict)
    {
        if ((item >= k.Value.Item1.l && item <= k.Value.Item1.r) ||
            (item >= k.Value.Item2.l && item <= k.Value.Item2.r)
            )
            return true;
    }
    return false;
}

Console.WriteLine(sum); 
#endregion

#region Part2
List<(int,string)> map = new List<(int,string)>();
Dictionary<string, List<int>> map2 = new();
for(int i = 0; i < FIELDS; i++)
{
    List<int> values = new List<int>();
    foreach(var item in goodTickets)
    {
        values.Add(item[i]);
    }
    foreach(var k in dict.Keys)
    {
        if (values.All(x => (x >= dict[k].Item1.l && x <= dict[k].Item1.r) ||
                            (x >= dict[k].Item2.l && x <= dict[k].Item2.r)
        ))
        {
            map.Add((i, k));
            if (map2.ContainsKey(k))
                map2[k].Add(i);
            else
            {
                List<int> list = new List<int>();
                list.Add(i);
                map2[k] = list;
            }
        }
    }
}
foreach (var item in map2)
{
    Console.WriteLine(item.Key);
    foreach (var item2 in item.Value)
        Console.Write($"{item2} ");
    Console.WriteLine();
}

Dictionary<string, int> result = new();
int res = 0;
string key = "";
while(map2.Count > 0)
{
    foreach(var item in map2.Keys)
    {
        if (map2[item].Count == 1)
        {
            result[item] = map2[item][0];
            res = map2[item][0]; 
            key = item;
            break;
        }
    }
    map2.Remove(key);
    foreach (var item in map2.Keys)
    {
        if (map2[item].Contains(res))
            map2[item].Remove(res);
    }
}
long prod = 1;
foreach(var item in result)
{
    if (item.Key.StartsWith("departure"))
        prod = prod * myTicket[item.Value];

}
Console.WriteLine(prod);

#endregion