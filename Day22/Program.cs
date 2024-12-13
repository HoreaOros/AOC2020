#region Input parsing
string text = File.ReadAllText("input.txt");
string[] t = text.Split("\r\n\r\n");
Queue<int> q1 = new Queue<int>();
Queue<int> q2 = new Queue<int>();

string[] t2 = t[0].Split("\r\n");
for (int i = 1; i < t2.Length; i++)
    q1.Enqueue(int.Parse(t2[i]));

t2 = t[1].Split("\r\n");
for (int i = 1; i < t2.Length; i++)
    q2.Enqueue(int.Parse(t2[i]));
Console.WriteLine();
#endregion

#region Part1
while(q1.Count > 0 && q2.Count > 0)
{
    int a = q1.Dequeue();
    int b = q2.Dequeue();
    if(a > b)
    {
        q1.Enqueue(a); q1.Enqueue(b);
    }
    else
    {
        q2.Enqueue(b); q2.Enqueue(a);
    }
}

long r1 = 0;
Queue<int> q;
if (q1.Count > 0)
    q = q1;
else
    q = q2;
while(q.Count > 0)
{
    int a = q.Dequeue();
    r1 += a * (q.Count + 1);
}
Console.WriteLine(r1);
#endregion

#region Part2
#endregion