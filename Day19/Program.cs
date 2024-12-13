#region Input parsing
string text = File.ReadAllText("input.txt");
string[] t = text.Split("\r\n\r\n");
string[] rules = t[0].Split(Environment.NewLine);
string[] mesages = t[1].Split(Environment.NewLine);

int a = 0, b = 0;
Dictionary<int, List<List<int>>> data = new();
List<(int, string)> dataList = new();






foreach (string rule in rules )
{
    string[] t2 = rule.Split(": ", StringSplitOptions.RemoveEmptyEntries);
    dataList.Add((int.Parse(t2[0]), t2[1]));
    if (t2[1].Contains('a'))
        a = int.Parse(t2[0]);
    else if (t2[1].Contains('b'))
        b = int.Parse(t2[0]);
    else
    {
        string[] t3 = t2[1].Split("|", StringSplitOptions.RemoveEmptyEntries);
        List<List<int>> lst = new();
        foreach(var item in t3)
        {
            List<int> list = new List<int>();
            string[] t4 = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var it in t4)
            {
                list.Add(int.Parse(it));
            }
            lst.Add(list);
        }
        data[int.Parse(t2[0])] = lst;
    }
}

//part2
data[8].Add(new List<int> { 42, 8 });
data[11].Add(new List<int> { 42, 11, 31 });

dataList.Sort((x,y) => x.Item1 - y.Item1);
foreach(var item in dataList)
    Console.WriteLine(item);
Console.WriteLine();
#endregion

#region Part1
int r1 = 0;
for(int i = 0; i < mesages.Length; i++)
{
    Console.Write($"{mesages[i]} ");
    List<int> lst = data[0][0];
    bool ok = true;
    for (int j = 0; j < lst.Count; j++)
        if (Valid(ref mesages[i], lst[j], a, b))
            continue;
        else
        {
            ok = false;
            break;
        }

    Console.WriteLine(ok);
    if (ok && mesages[i] == "")
        r1++;
}
Console.WriteLine(r1);

bool Valid(ref string msg, int rule, int a, int b)
{
    if (msg == "")
        return false;
    if (rule == a)
        if (msg[0] != 'a')
            return false;
        else
        {
            msg = msg.Substring(1);
            return true;
        }
    if(rule == b)
        if (msg[0] != 'b')
            return false;
        else
        {
            msg = msg.Substring(1);
            return true;
        }

    var lsts = data[rule];
    if(lsts.Count == 1)
    {
        return ValidList(ref msg, lsts[0], a, b);
    }
    else 
    {
        string msg1 = new string(msg);
        bool b1 = ValidList(ref msg1, lsts[0], a, b);
        if (b1)
        {
            msg = msg1;
            return true;
        }
        else
        {
            string msg2 = new string(msg);
            bool b2 = ValidList(ref msg2, lsts[1], a, b);
            if (b2)
            {
                msg = msg2;
                return true;
            }
            else
                return false;
        }
    }
}

bool ValidList(ref string msg, List<int> list, int a, int b)
{
    if(msg == "")
        return false;
    for(int i = 0; i < list.Count; i++)
    {
        if (Valid(ref msg, list[i], a, b))
            continue;
        else
        {
            return false;
        }
    }
    return true;
}

#endregion

#region Part2
#endregion