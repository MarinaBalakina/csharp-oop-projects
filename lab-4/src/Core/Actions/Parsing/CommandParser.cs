namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

public class CommandParser : IParser
{
    public ParserResult Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input), "Command text cannot be null or whitespace.");

        IReadOnlyList<string> tokens = SplitToTokens(input);

        if (tokens.Count == 0)
            throw new ArgumentException("Command text cannot be empty.", nameof(input));

        string commandName = tokens[0];
        if (string.IsNullOrWhiteSpace(commandName))
            throw new ArgumentException("Command name cannot be null or whitespace.", nameof(input));

        ParseArgumentsAndFlags(tokens, 1, out List<string> arguments, out Dictionary<string, string?> flags);

        return new ParserResult(commandName, arguments, flags);
    }

    private IReadOnlyList<string> SplitToTokens(string input)
    {
        IReadOnlyList<string> tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return tokens;
    }

    private void ParseArgumentsAndFlags(
        IReadOnlyList<string> tokens,
        int startIndex,
        out List<string> arguments,
        out Dictionary<string, string?> flags)
    {
        if (startIndex < 0 || startIndex > tokens.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        arguments = new List<string>();
        flags = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        for (int i = startIndex; i < tokens.Count; i++)
        {
            string current = tokens[i];

            if (current.StartsWith('-') && current.Length > 1)
            {
                string flagName = current.Substring(1);
                if (string.IsNullOrWhiteSpace(flagName))
                    throw new ArgumentException("Flag name cannot be null or whitespace.", nameof(tokens));

                string? value = null;
                int nextIndex = i + 1;
                if (nextIndex < tokens.Count)
                {
                    string nextToken = tokens[nextIndex];

                    if (!nextToken.StartsWith('-'))
                    {
                        value = nextToken;
                        i = nextIndex;
                    }
                }

                flags[flagName] = value;
            }
            else
            {
                arguments.Add(current);
            }
        }
    }
}