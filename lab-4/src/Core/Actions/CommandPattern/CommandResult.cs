namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;

public class CommandResult
{
    public bool IsSuccess { get; private set; }

    public string Message { get; private set; }

    public CommandResult(bool isSuccess, string message)
    {
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        IsSuccess = isSuccess;
        Message = message;
    }

    public static CommandResult Success(string message)
    {
        return new CommandResult(true, message);
    }

    public static CommandResult Fail(string message)
    {
        return new CommandResult(false, message);
    }
}