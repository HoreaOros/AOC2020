#region Input parsing
string text = File.ReadAllText("input.txt");
int range = 25;
string[] lines = text.Split(Environment.NewLine);
ulong[] nums = lines.Select(x => ulong.Parse(x)).ToArray();
#endregion



#region Part1
List<ulong> lst = new List<ulong>();
for (int i = 0; i < range; i++)
    lst.Add(nums[i]);
ulong p1Result = 0;
for(int i = range; i < nums.Length; i++)
{
    if (TwoSum(lst, nums[i]))
    {
        lst.RemoveAt(0);
        lst.Add(nums[i]);
        continue;
    }
    else
    {
        p1Result = nums[i];
        Console.WriteLine(nums[i]);
        break;
    }
}

bool TwoSum(List<ulong> lst, ulong v)
{
    for(int i = 0; i < lst.Count; i++)
        for(int j = i + 1; j < lst.Count; j++)
            if (lst[i] + lst[j] == v)
                return true;
    return false;
}
#endregion

#region Part2
ulong[] sums = new ulong[nums.Length + 1];
sums[0] = 0;
ulong p2Result = 0;
for (int i = 1; i < sums.Length; i++)
{
    sums[i] = sums[i - 1] + nums[i - 1];
}
bool found = false;
for(int i = 1; !found && i < sums.Length; i++)
    for(int j = i + 1; !found && j < sums.Length; j++)
    {
        if (j > i && sums[j] - sums[i - 1] == p1Result)
        {
            p2Result = nums[(i-1)..(j-1)].Min() + nums[(i - 1)..(j-1)].Max();
            found = true;
        }
    }
Console.WriteLine(p2Result);
#endregion