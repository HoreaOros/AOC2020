#region Input parsing
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
List<(string mask, List<(long adr, long value)> list)> data = new();
Regex r1 = new Regex(@"mask = ([10X]{36})");
Regex r2 = new Regex(@"mem\[(\d+)\] = (\d+)");
Match m;
List<(long adr, long value)> lst = new List<(long adr, long value)>();

Dictionary<int, int> dic = new Dictionary<int, int>();

foreach (var item in lines)
{
    m = r1.Match(item);
    if (m.Success)
    {
        lst = new List<(long adr, long value)>();
        data.Add((m.Groups[1].Value, lst));
    }
    else
    {
        m = r2.Match(item);
        lst.Add((long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value)));
    }
}
Console.WriteLine();

#endregion

#region Part1
Dictionary<long, long> mem = new();
foreach (var item in data)
{
    string mask = item.mask;
    foreach (var item2 in item.list)
    {
        mem[item2.adr] = Process(item2.value, mask);
    }
}


//value: 000000000000000000000000000000001011(decimal 11)
//mask: XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
//result: 000000000000000000000000000001001001(decimal 73)
long Process(long value, string mask)
{
    long result = 0;
    string val = Convert.ToString(value, 2);
    val = val.PadLeft(36, '0');
    
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < val.Length; i++)
        if (mask[i] == 'X')
            sb.Append(val[i]);
        else sb.Append(mask[i]);

    return Convert.ToInt64(sb.ToString(), 2);
}

Sum(mem);

static void Sum(Dictionary<long, long> mem)
{
    long sum = 0;
    foreach (var item in mem)
        sum += item.Value;
    Console.WriteLine(sum);
}

#endregion

#region Part2
mem = new();
foreach (var item in data)
{
    string mask = item.mask;
    foreach (var item2 in item.list)
    {
        Process2(mem, item2.adr, item2.value, mask);
    }
}
Sum(mem);



//address: 000000000000000000000000000000101010  (decimal 42)
//mask:    000000000000000000000000000000X1001X
//result:  000000000000000000000000000000X1101X
void Process2(Dictionary<long, long> mem, long adr, long value, string mask)
{
    string address = Convert.ToString(adr, 2);
    address = address.PadLeft(36, '0');

    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < address.Length; i++)
        if (mask[i] == 'X')
            sb.Append('X');
        else if(mask[i] == '0')
            sb.Append(address[i]);
        else
            sb.Append('1');
    string result = sb.ToString();

    //Console.WriteLine(address);
    //Console.WriteLine(mask);
    //Console.WriteLine(result);

    Bck(mem, result.ToCharArray(), value, 0);


}

void Bck(Dictionary<long, long> mem, char[] d, long value, int level)
{
    if(level == 36)
    {
        string num = new string(d);
        long t = Convert.ToInt64(num, 2);
        mem[t] = value;
    }
    else
    {
        if (d[level] == '0' || d[level] == '1')
            Bck(mem, d, value, level + 1);
        else
        {
            d[level] = '0';
            Bck(mem, d, value, level + 1);
            d[level] = '1';
            Bck(mem, d, value, level + 1);
            d[level] = 'X';
        }
    }
}


#endregion