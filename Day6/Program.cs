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
