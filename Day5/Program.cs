#region Input processing
string lines = File.ReadAllText("input.txt");
string[] seats = lines.Split(Environment.NewLine);
#endregion


#region Part1
int maxim = 0;
// BBFBFBFLRL
// 01100
List<int> lst = new();
foreach (string seat in seats)
{
    int row = 0, column = 0, bit;
    for (int i = 0; i < 7; i++)
    {
        if (seat[i] == 'F')
            bit = 0;
        else
            bit = 1;
        row = row * 2 + bit;
    }
    for(int i = 7; i < 10; i++)
    {
        if (seat[i] == 'L')
            bit = 0;
        else
            bit = 1;
        column = column * 2 + bit;
    }
    int seatID = row * 8 + column;
    lst.Add(seatID);
    maxim = Math.Max(maxim, seatID);
         
}
Console.WriteLine(maxim);
#endregion


#region Part2
lst.Sort();
for(int i = 0; i < lst.Count - 1; i++)
    if (lst[i + 1] - lst[i] == 2)
        Console.WriteLine(lst[i] + 1);
#endregion


