#region Input parsing
using System.Text.RegularExpressions;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
Regex r = new Regex(@"(?:\d)|(?:\+)|(?:\*)|(?:\()|(?:\))");
List<List<string>> data = new();
foreach (string line in lines)
{
    MatchCollection matches = r.Matches("("+line+")");
    List<string> lst = new();
    foreach (Match match in matches)
        lst.Add(match.Value);
    data.Add(lst);
}
Console.WriteLine();
#endregion

#region Part1
long r1 = 0;
foreach (var item in data)
    r1 += part1(item);
Console.WriteLine(r1);

long part1(List<string> item)
{
    Stack<string> ops = new();
    List<string> pn = new();

    // generate polish notation
    foreach (string t in item)
    {
        switch (t)
        {
            case "+":
            case "*":
                while (ops.Count > 0 && ops.Peek() != "(")
                    pn.Add(ops.Pop());
                ops.Push(t);
                break;
            case "(":
                ops.Push(t);
                break;
            case ")":
                while (ops.Count > 0 && ops.Peek() != "(")
                {
                    pn.Add(ops.Pop());
                }
                if (ops.Count > 0)
                    ops.Pop();
                break;
            default:
                pn.Add(t);
                break;
        }
    }
    // evaluate polish notation
    return EvaluatePolishNotation(pn);
}


static long EvaluatePolishNotation(List<string> pn)
{
    Stack<long> st = new();
    foreach (string it in pn)
    {
        if (it == "+")
        {
            long b = st.Pop();
            long a = st.Pop();
            st.Push(a + b);
        }
        else if (it == "*")
        {
            long b = st.Pop();
            long a = st.Pop();
            st.Push(a * b);
        }
        else
            st.Push(long.Parse(it));
    }
    return st.Pop();
}
#endregion

#region Part2
long r2 = 0;
foreach (var item in data)
    r2 += part2(item);
Console.WriteLine(r2);

long part2(List<string> item)
{
    Stack<string> ops = new();
    List<string> pn = new();

    // generate polish notation
    foreach (string t in item)
    {
        switch (t)
        {
            case "+":
                while (ops.Count > 0 && ops.Peek() != "(" && ops.Peek() == "+")
                    pn.Add(ops.Pop());
                ops.Push(t);
                break;
            case "*":
                while (ops.Count > 0 && ops.Peek() != "(")
                    pn.Add(ops.Pop());
                ops.Push(t);
                break;
            case "(":
                ops.Push(t);
                break;
            case ")":
                while (ops.Count > 0 && ops.Peek() != "(")
                {
                    pn.Add(ops.Pop());
                }
                if (ops.Count > 0)
                    ops.Pop();
                break;
            default:
                pn.Add(t);
                break;
        }
    }
    // evaluate polish notation
    return EvaluatePolishNotation(pn);
}
#endregion