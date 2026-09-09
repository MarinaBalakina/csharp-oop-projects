namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

public class ParserResult
{
    public string CommandName { get; }

    public IReadOnlyList<string> Arguments { get; }

    public IReadOnlyDictionary<string, string?> Flags { get; }

    public ParserResult(
        string commandName,
        IReadOnlyList<string> arguments,
        IReadOnlyDictionary<string, string?> flags)
    {
        CommandName = commandName;
        Arguments = arguments;
        Flags = flags;
    }
}