#region Input parsing
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
//pale turquoise bags contain 3 muted cyan bags, 5 striped teal bags.
//striped maroon bags contain no other bags.
//string pattern = @"([ a-z]+) bags contain ((\d)+ ([a-z ]+)(?:, |\.)+)";
//Regex regex = new Regex(pattern);

string[] lines = text.Split(Environment.NewLine);
Dictionary<string, List<(string, int)>> map = new();
Regex r = new Regex(@"(\d+) ([a-z ]+) bags*");
foreach (string line in lines)
{
    string[] t1 = line.Split(" bags contain ");
    string key = t1[0];
    //Console.WriteLine(t1[0]);
    MatchCollection mc = r.Matches(t1[1]);
    List<(string bag, int count)> lst = new();
    foreach (Match m in mc)
    {
        //Console.WriteLine($"\t{m.Groups[1].Value} {m.Groups[2].Value}");
        lst.Add((m.Groups[2].Value, int.Parse(m.Groups[1].Value)));
    }
    //Console.WriteLine();

    map.Add(key, lst);
}

Dictionary<string, List<(string, int)>> inverseMap = new();
foreach(string key in map.Keys)
{
    foreach(var v in map[key]) // lista de tuples (nod, count)
    {
        string nod = v.Item1;
        if (!inverseMap.Keys.Contains(nod))
            inverseMap.Add(nod, new List<(string, int)>() { (key, v.Item2) });
        else
            inverseMap[nod].Add((key, v.Item2));
    }
}

Console.WriteLine();
#endregion


#region Part1
string start = "shiny gold";
HashSet<string> SEEN = new();
SEEN.Add(start);
Part1(start);
void Part1(string start)
{
    if(inverseMap.ContainsKey(start))
        foreach (var item in inverseMap[start])
            if (!SEEN.Contains(item.Item1))
            {
                SEEN.Add(item.Item1);
                Part1(item.Item1);
            }
}
Console.WriteLine(SEEN.Count - 1);
#endregion


#region Part2
Console.WriteLine(Part2(start));

long Part2(string start)
{

    if (map[start].Count == 0)
        return 0;
    long result = 0;
    foreach (var item in map[start])
    {
        result = result + item.Item2 + item.Item2 * Part2(item.Item1);
    }
    return result;
}

#endregion
