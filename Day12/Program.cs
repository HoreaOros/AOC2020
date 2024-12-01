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


#region Part2
long shipX = 0, shipY = 0;
long waypointX = 10, waypointY = 1, dx, dy;
foreach(var item in lst)
{
    switch(item.command)
    {
        case 'N':
            waypointY += item.value; break;
        case 'S':
            waypointY -= item.value; break;
        case 'E':
            waypointX += item.value; break;
        case 'W':
            waypointX -= item.value; break;
        case 'L':
            angle = item.value;
            while (angle > 0)
            {
                (waypointX, waypointY) = (-waypointY, waypointX);
                angle -= 90;
            }
            break;
        case 'R':
            angle = item.value;
            while (angle > 0)
            {
                (waypointX, waypointY) = (waypointY, -waypointX);
                angle -= 90;
            }
            break;
        case 'F':
            shipX += item.value * waypointX;
            shipY += item.value * waypointY;
            break;

    }
    Console.WriteLine($"{item.command}{item.value} Ship: X{shipX} Y{shipY} / Waypoint: X{waypointX} Y{waypointY}");
}
Console.WriteLine(Math.Abs(shipX) + Math.Abs(shipY));
#endregion
