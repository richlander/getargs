# Commandline parser

Code looks like the following.

```csharp
string value1 = string.Empty;
string value2 = string.Empty;
int value3 = 0;

GetArgs(
    new Arg("--value1", (s) => value1 = s),
    new Arg("--value2", (s) => value2 = s)
);

GetNumberArgs(
    new NumberArg("--value3", (n) => value3 = n)
);
```

Provides the following behavior.

```bash
% dotnet run
App Help:

--value1 [string]
--value2 [string]
--value3 [int]
% dotnet run --value1 val --value2 val2 --value3 8
value1 = val
value2 = val2
value3 = 8
% dotnet run --value1 val                         
value1 = val
value2 = 
value3 = 0
```
