namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;

public class ErrorCommand : ICommand
{
    private string Message { get; }

    public ErrorCommand(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        Message = message;
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        return CommandResult.Fail(Message);
    }
}