#region Input parsing
string text = File.ReadAllText("input.txt");
string[] lines = text.Split('\n');
int[] nums = new int[lines.Length];
for(int i = 0; i < nums.Length; i++)
    nums[i] = int.Parse(lines[i]);
Array.Sort(nums);
Console.WriteLine();
#endregion

#region Part1
int diff1 = 0, diff3 = 0;

if (nums[0] == 1)
    diff1++;
else
    diff3++;
for(int i = 1; i < nums.Length; i++)
    if (nums[i] - nums[i - 1] == 1)
        diff1++;
    else
        diff3++;

int result1 = diff1 * (diff3 + 1);
Console.WriteLine(result1);
#endregion

#region Part2
int[] diffs = new int[nums.Length];
diffs[0] =  nums[0];
for(int i = 1; i < nums.Length; i++)
    diffs[i] = nums[i] - nums[i - 1];
int count1 = 0;
List<int> ones = new List<int>();
for(int i = 0; i < diffs.Length; i++)
{
    if (diffs[i] == 1) count1++;
    else
    {
        if(count1 != 0) ones.Add(count1);
        count1 = 0;
    }
}
if (count1 != 0) ones.Add(count1);
Dictionary<int, int> S = new();
S.Add(1, 1);
S.Add(2, 2);
S.Add(3, 4);
for (int k = 4; k <= ones.Max(); k++)
    S.Add(k, S[k - 1] + S[k - 2] + S[k - 3]);



long result2 = 1;
for(int i = 0; i < ones.Count; i++)
    result2 *= S[ones[i]];

Console.WriteLine(result2);
#endregion