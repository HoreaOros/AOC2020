#region Input parsing
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
string[] pass = text.Split("\r\n\r\n");


List<Dictionary<string, string>> passports = new();
char[] seps = { ' ', '\n', '\r'};
foreach(string passport in pass)
{
    string[] tokens = passport.Split(seps, StringSplitOptions.RemoveEmptyEntries);
    Dictionary<string, string> kv = new Dictionary<string, string>();
    foreach(string token in tokens)
    {
        string[] tokens2 = token.Split(':');
        kv.Add(tokens2[0], tokens2[1]);
    }
    passports.Add(kv);
}
Console.WriteLine();
#endregion

#region Part1
//List<string> fields = new List<string>() { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt", "cid" };
List<string> fields = new List<string>() { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"};
int result = 0;
foreach(var p in passports)
{
    if (fields.All(k => p.Keys.Contains(k)))
        result++;
}
Console.WriteLine(result);
#endregion


#region Part2
result = 0;
foreach (var p in passports)
{
    if (fields.All(k => p.Keys.Contains(k)) 
        && checkYear(p["byr"], 1920, 2002)
        && checkYear(p["iyr"], 2010, 2020)
        && checkYear(p["eyr"], 2020, 2030)
        && checkHeight(p["hgt"])
        && checkHcl(p["hcl"])
        && checkEcl(p["ecl"])
        && checkPid(p["pid"])
        )
    {
        result++;
        foreach(var kv in p)
            Console.WriteLine($"{kv.Key} {kv.Value}");
        Console.WriteLine();
    }
}
Console.WriteLine(result);

bool checkPid(string pid)
{
    string pattern = "^[0-9]{9}$";
    Regex r = new Regex(pattern);
    Match m = r.Match(pid);
    return m.Success;
}

bool checkEcl(string ecl)
{
    List<string> eyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
    return eyeColors.Contains(ecl);
}

bool checkHcl(string hcl)
{
    string pattern = "^#[0-9a-f]{6}$";
    Regex r = new Regex(pattern);
    Match m = r.Match(hcl);
    return m.Success;
}

bool checkHeight(string heigth)
{
    string pattern = "^(\\d+)(cm|in)$";
    Regex r = new Regex(pattern);
    Match m = r.Match(heigth);
    if (!m.Success)
        return false;
    
    int h = int.Parse(m.Groups[1].Value);
    string x = m.Groups[2].Value;

    bool result = true;
    switch (x)
    {
        case "cm":
            result = h >= 150 && h <= 193;
            break;
        case "in":
            result = h >= 59 && h <= 76;
            break;
    }   
    
    return result;
}


bool checkYear(string year, int min, int max)
{
    int intYear;
    bool r = int.TryParse(year, out intYear);
    if (!r)
        return false;
    return intYear >= min && intYear <= max;
}
#endregion