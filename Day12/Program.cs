#region Input parsing
string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
List<(char command, int value)> lst = new();
foreach (string line in lines)
    lst.Add((line[0], int.Parse(line.Substring(1))));
Console.WriteLine();
#endregion

#region Part1
char facing = 'E';
int x = 0, y = 0;
int angle;
foreach(var item in lst)
{
    switch (item.command)
    {
        case 'N':
            y += item.value; break;
        case 'S':
            y -= item.value; break;
        case 'E':
            x += item.value; break;
        case 'W':
            x -= item.value; break;
        case 'F':
            switch (facing)
            {
                case 'N':
                    y += item.value; break;
                case 'S':
                    y -= item.value; break;
                case 'E':
                    x += item.value; break;
                case 'W':
                    x -= item.value; break;
            }
            break;
        case 'L':
            angle = item.value;
            while (angle > 0)
            {
                switch (facing)
                {
                    case 'E':
                        facing = 'N'; break;
                    case 'S':
                        facing = 'E'; break;
                    case 'W':
                        facing = 'S'; break;
                    case 'N':
                        facing = 'W'; break;
                }
                angle -= 90;
            }
            break;
        case 'R':
            angle = item.value;
            while (angle > 0)
            { 
                switch (facing)
                {
                    case 'E':
                        facing = 'S'; break;
                    case 'S':
                        facing = 'W'; break;
                    case 'W':
                        facing = 'N'; break;
                    case 'N':
                        facing = 'E'; break;
                }
                angle -= 90;
            }
            break;
    }
}


Console.WriteLine(Math.Abs(x) + Math.Abs(y));
#endregion


#region Part1
#endregion
