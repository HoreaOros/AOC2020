#region Input processing
string text = File.ReadAllText("input.txt");
string[] groups = text.Split("\r\n\r\n");
Console.WriteLine();
#endregion

#region Part1
int sum = 0;
foreach (string group in groups)
{
    HashSet<char> q = new HashSet<char>();
    foreach (char c in group)
        if(char.IsLetter(c))
            q.Add(c);
 
    sum += q.Count;
}
Console.WriteLine(sum);
#endregion


#region Part2
sum = 0;
foreach(string group in groups)
{
    List<HashSet<char>> ans = new List<HashSet<char>>();
    string[] t = group.Split("\r\n");
    foreach(string s in t)
    {
        HashSet<char> q = new HashSet<char>(s);
        ans.Add(q);
    }

    HashSet<char> result = new HashSet<char>();
    result = ans[0];
    foreach(var h in ans)
        result.IntersectWith(h);
    sum += result.Count;
}
Console.WriteLine(sum);
#endregion