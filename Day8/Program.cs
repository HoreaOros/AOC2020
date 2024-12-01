#region Input parsing
using System.Text.RegularExpressions;

string code = File.ReadAllText("input.txt");
string[] lines = code.Split(Environment.NewLine);
List<(string instr, int arg)> program = new();
Regex r = new Regex(@"(acc|jmp|nop) ((?:\+|-)[0-9]+)");
foreach (string line in lines)
{
    Match m = r.Match(line);
    program.Add((m.Groups[1].Value, int.Parse(m.Groups[2].Value)));
}
#endregion

#region Part1
(int accumulator, bool result) = Part1(program);
Console.WriteLine(accumulator);

static (int, bool) Part1(List<(string instr, int arg)> program)
{
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
        if (PC >= program.Count)
            return (accumulator, true);
    } while (!SEEN.Contains(PC));
    return (accumulator, false);
}
#endregion


#region Part2
for(int i = 0; i < program.Count; i++)
{
    if (program[i].instr == "acc")
        continue;

    string bck = program[i].instr;
    if (program[i].instr == "nop")
        program[i] = ("jmp", program[i].arg);
    else
        program[i] = ("nop", program[i].arg);


    (int acc, bool res) = Part1(program);

    if(res == true)
    {
        Console.WriteLine(acc);
        break;
    }


    program[i] = (bck, program[i].arg);

}
#endregion
