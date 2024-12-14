#region Input parsing
using System.ComponentModel.Design;

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
//while (q1.Count > 0 && q2.Count > 0)
//{
//    int a = q1.Dequeue();
//    int b = q2.Dequeue();
//    if (a > b)
//    {
//        q1.Enqueue(a); q1.Enqueue(b);
//    }
//    else
//    {
//        q2.Enqueue(b); q2.Enqueue(a);
//    }
//}

//long r1 = ComputeScore(q1, q2);
//Console.WriteLine(r1);
static long ComputeScore(Queue<int> q1, Queue<int> q2)
{
    long r = 0;
    Queue<int> q;
    if (q1.Count > 0)
        q = q1;
    else
        q = q2;
    while (q.Count > 0)
    {
        int a = q.Dequeue();
        r += a * (q.Count + 1);
    }
    return r;
}
#endregion

#region Part2
Play(q1, q2);
Console.WriteLine(ComputeScore(q1, q2));

int Play(Queue<int> q1, Queue<int> q2)
{
    

    HashSet<string> history1 = new HashSet<string>();
    HashSet<string> history2 = new HashSet<string>();

    while(true)
    {
        string h1 = string.Concat(q1);
        string h2 = string.Concat(q2);
        if (history1.Contains(h1) || history2.Contains(h2))
            return 1;

        history1.Add(h1);
        history2.Add(h2);

        int a = q1.Dequeue();
        int b = q2.Dequeue();
        if (q1.Count >= a && q2.Count >= b)
        { 
            Queue<int> n1 = new Queue<int>(q1);
            Queue<int> n2 = new Queue<int>(q2);

            Queue<int> nq1 = new Queue<int>();
            Queue<int> nq2 = new Queue<int>();
            for(int i = 0; i < a; i++)
                nq1.Enqueue(n1.Dequeue());
            for (int i = 0; i < b; i++)
                nq2.Enqueue(n2.Dequeue());
            if (Play(nq1, nq2) == 1)
            {
                q1.Enqueue(a); q1.Enqueue(b);
            }
            else
            {
                q2.Enqueue(b); q2.Enqueue(a);
            }

        }
        else
        {
            if (a > b)
            {
                q1.Enqueue(a); q1.Enqueue(b);
            }
            else
            {
                q2.Enqueue(b); q2.Enqueue(a);
            }
        }
        if (q1.Count == 0)
            return 2;
        else
            if (q2.Count == 0)
                return 1;
    }

}
#endregion