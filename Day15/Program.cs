#region Input parsing
string text = File.ReadAllText("input.txt");
string[] t = text.Split(',');
int[] nums = new int[t.Length];
for (int i = 0; i < t.Length; i++)
    nums[i] = int.Parse(t[i]);
#endregion

#region Part1
Solve(nums, 2020);
Solve(nums, 30000000);

static void Solve(int[] nums, int N)
{
    int last = 0;
    Dictionary<int, Stack<int>> lst = new();
    for (int i = 0; i < nums.Length; i++)
    {
        var st = new Stack<int>();
        st.Push(i + 1);
        lst[nums[i]] = st;
        last = nums[i];
        //Console.WriteLine($"Turn: {i + 1} - {last}");
    }

    for (int i = nums.Length; i < N; i++)
    {
        // If that was the first time the number has been spoken, the current player says 0.
        if (lst[last].Count == 1)
        {
            if (lst.ContainsKey(0))
                lst[0].Push(i + 1);
            else
            {
                var st = new Stack<int>();
                st.Push(i + 1);
                lst[0] = st;
            }
            last = 0;
        }
        else // Otherwise, the number had been spoken before; the current player announces how many turns apart the number is from when it was previously spoken.
        {
            var st = lst[last];
            int a = st.Pop();
            int b = st.Pop();
            st.Push(b); st.Push(a);
            int val = a - b;
            if (lst.ContainsKey(val))
                lst[val].Push(i + 1);
            else
            {
                st = new Stack<int>();
                st.Push(i + 1);
                lst[val] = st;
            }
            last = val;
        }
        //Console.WriteLine($"Turn: {i + 1} - {last}");
    }
    Console.WriteLine(last);
}
#endregion

#region Part2
#endregion