#region Input parsing
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
Console.WriteLine();
Regex r = new Regex("(\\d+)-(\\d+) ([a-z]): ([a-z]+)");
Match m;
int n1, n2;
char letter;
string password;

List <(int, int, char, string)> pwds= new ();

foreach (string line in lines)
{
    m = r.Match(line);
    n1 = int.Parse(m.Groups[1].Value);
    n2 = int.Parse(m.Groups[2].Value);
    letter = m.Groups[3].Value[0];
    password = m.Groups[4].Value;
    pwds.Add((n1, n2, letter, password));
}
Console.WriteLine();
#endregion

#region Part1
int goodPasswords = 0;
for (int i = 0; i < pwds.Count; i++)
{
    (n1, n2, letter, password) = pwds[i];
    int count = 0;
    foreach (char c in password)
        if (c == letter)
            count++;
    if (count >= n1 && count <= n2)
        goodPasswords++;
}
Console.WriteLine(goodPasswords);
#endregion

#region Part2
goodPasswords = 0;
for (int i = 0; i < pwds.Count; i++)
{
    (n1, n2, letter, password) = pwds[i];
    int count = 0;
    //if ((password[n1 - 1] == letter && password[n2 - 1] != letter) ||
    //    (password[n1 - 1] != letter && password[n2 - 1] == letter))
    //    goodPasswords++;
    if (password[n1 - 1] == letter)
        count++;
    if (password[n2 - 1] == letter)
        count++;
    if (count == 1)
        goodPasswords++;
}
Console.WriteLine(goodPasswords);
#endregion

