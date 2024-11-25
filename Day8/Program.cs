#region Input parsing
using System.Text.RegularExpressions;

string code = File.ReadAllText("input.txt");
string[] lines = code.Split(Environment.NewLine);
List<(string instr, int arg)> program = new();
Regex r = new Regex(@"(acc|jmp|nop) ((?:\+|-)\d+)");
foreach (string line in lines)
{
    Match m = r.Match(line);
    program.Add((m.Groups[1].Value, int.Parse(m.Groups[2].Value)));
}
#endregion

#region Part1
int PC = 0; // program counter
int accumulator = 0;
HashSet<int> SEEN = new();
do
{
    SEEN.Add(PC);
    var currentInstr = program[PC];
    switch (currentInstr.instr)
    {
        case "nop":
            PC++;
            break;
        case "acc":
            accumulator += currentInstr.arg;
            PC++;
            break;
        case "jmp":
            PC += currentInstr.arg;
            break;
    }
} while (!SEEN.Contains(PC));
Console.WriteLine(accumulator);
#endregion


#region Part2
#endregion
