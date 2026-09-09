using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class CommandRunner
{
    private IParser Parser { get; }

    private ICommandFactory CommandFactory { get; }

    private CommandEnvironment Environment { get; }

    public CommandRunner(IParser parser, ICommandFactory commandFactory, CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(commandFactory);
        ArgumentNullException.ThrowIfNull(environment);

        Parser = parser;
        CommandFactory = commandFactory;
        Environment = environment;
    }

    public CommandResult Run(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return CommandResult.Fail("Input is empty");
        }

        try
        {
            ParserResult result = Parser.Parse(input);
            ICommand command = CommandFactory.Create(result);

            return command.Execute(Environment);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);

            return CommandResult.Fail("Internal error: See log for details.");
        }
    }
}
