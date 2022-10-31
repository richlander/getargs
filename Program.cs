using System.Diagnostics.CodeAnalysis;
using static System.Console;

string value1 = string.Empty;
string value2 = string.Empty;
int value3 = 0;
int argCount = 0;
List<string> help = new();

GetArgs(
    new Arg("--value1", (s) => value1 = s),
    new Arg("--value2", (s) => value2 = s)
);

GetNumberArgs(
    new NumberArg("--value3", (n) => value3 = n)
);

if (argCount is 0)
{
    PrintHelp();
    return;
}

WriteLine($"{nameof(value1)} = {value1}");
WriteLine($"{nameof(value2)} = {value2}");
WriteLine($"{nameof(value3)} = {value3}");

bool GetArg(string arg, [NotNullWhen(true)] out string? value)
{
    value = null;
    int index = 0;
    int count = args.Length - 1;
    while(index < count )
    {
        if (arg == args[index])
        {
            value = args[index + 1];
            return true;
        }

        index += 2;
    }

    return false;
}

int GetNumberArg(string arg)
{
    if (GetArg(arg, out string? value))
    {
        return int.TryParse(value, out int number) ? number : 0;
    }

    return 0;
}

void GetArgs(params Arg[] argList)
{
    foreach(Arg arg in argList)
    {
        help.Add($"{arg.Name} [string]");
        
        if (args.Length > 0 &&
            GetArg(arg.Name, out string? value))
        {
            arg.AssignValue(value);
            argCount++;
        }

    }
}

void GetNumberArgs(params NumberArg[] argList)
{
    foreach(NumberArg arg in argList)
    {
        help.Add($"{arg.Name} [int]");

        if (args.Length is 0)
        {
            continue;
        }

        int value = GetNumberArg(arg.Name);
        if (value > 0)
        {
            arg.AssignValue(value);
            argCount++;
        }

    }
}

void PrintHelp()
{
    WriteLine("App Help:");
    WriteLine();
    foreach(string arg in help)
    {
        WriteLine(arg);
    }
}

public record Arg(string Name, Func<string, string> AssignValue);
public record NumberArg(string Name, Func<int, int> AssignValue);
