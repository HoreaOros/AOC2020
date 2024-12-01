#region Input parsing
using System.Data.Common;

string text = File.ReadAllText("input.txt");
string[] lines = text.Split(Environment.NewLine);
int LINS = lines.Length, COLS = lines[0].Length;
char[,] seats = new char[LINS, COLS];
char[,] newSeats = new char[LINS, COLS];
for (int i = 0; i < LINS; i++)
    for (int j = 0; j < COLS; j++)
    {
        newSeats[i, j] = lines[i][j];
        seats[i, j] = lines[i][j];
    }

//printSeats(seats);

void printSeats(char[,] seats)
{
    for (int i = 0; i < seats.GetLength(0); i++)
    {
        for (int j = 0; j < seats.GetLength(1); j++)
            Console.Write(seats[i, j]);
        Console.WriteLine();
    }
    Console.WriteLine();
}
#endregion
int[] di = { -1, -1, -1, 1, 1, 1,  0, 0 };
int[] dj = { -1, 0, 1,  -1, 0, 1, -1, 1 };

#region Part1

//do
//{
//    seats = newSeats;
//    newSeats = new char[LINS, COLS];
//    for (int i = 0; i < seats.GetLength(0); i++)
//        for (int j = 0; j < seats.GetLength(1); j++)
//        {
//            int count = 0;
//            for (int k = 0; k < di.Length; k++)
//            {
//                int ni = i + di[k];
//                int nj = j + dj[k];
//                if (ni >= 0 && ni < LINS && nj >= 0 && nj < COLS)
//                    if (seats[ni, nj] == '#')
//                        count++;
//            }
//            if (seats[i, j] == 'L' && count == 0)
//                newSeats[i, j] = '#';
//            else if (seats[i, j] == '#' && count >= 4)
//                newSeats[i, j] = 'L';
//            else
//                newSeats[i, j] = seats[i, j];
//        }
//    //printSeats(newSeats);

//} while (!stable(seats, newSeats));

//int result1 = CountOccupiedSeats(seats);
//Console.WriteLine(result1);

bool stable(char[,] seats, char[,] newSeats)
{
    for (int i = 0; i < seats.GetLength(0); i++)
        for (int j = 0; j < seats.GetLength(1); j++)
            if (seats[i, j] != newSeats[i, j])
                return false;
    return true;
}

static int CountOccupiedSeats(char[,] seats)
{
    int result1 = 0;
    for (int i = 0; i < seats.GetLength(0); i++)
        for (int j = 0; j < seats.GetLength(1); j++)
            if (seats[i, j] == '#')
                result1++;
    return result1;
}
#endregion

#region Part2

do
{
    seats = newSeats;
    newSeats = new char[LINS, COLS];
    for (int i = 0; i < seats.GetLength(0); i++)
        for (int j = 0; j < seats.GetLength(1); j++)
        {
            int visibleOccupiedSeats = 0;
            int visibleFreeSeats = 0;
            for (int k = 0; k < di.Length; k++)
            {
                int ni = i + di[k];
                int nj = j + dj[k];
                while (ni >= 0 && ni < LINS && nj >= 0 && nj < COLS && seats[ni, nj] == '.')
                {
                    ni = ni + di[k];
                    nj = nj + dj[k];
                }
                if (ni >= 0 && ni < LINS && nj >= 0 && nj < COLS && seats[ni, nj] == '#')
                    visibleOccupiedSeats++;
                if (ni >= 0 && ni < LINS && nj >= 0 && nj < COLS && seats[ni, nj] == 'L')
                    visibleFreeSeats++;
            }
            if (seats[i, j] == 'L' && visibleOccupiedSeats == 0)
                newSeats[i, j] = '#';
            else if (seats[i, j] == '#' && visibleOccupiedSeats >= 5)
                newSeats[i, j] = 'L';
            else
                newSeats[i, j] = seats[i, j];
        }
    //printSeats(newSeats);

} while (!stable(seats, newSeats));


int result2 = CountOccupiedSeats(seats);
Console.WriteLine(result2);
#endregion
