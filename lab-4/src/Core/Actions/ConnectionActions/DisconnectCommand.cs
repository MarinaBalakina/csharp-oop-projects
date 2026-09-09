using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.ConnectionActions;

public class DisconnectCommand : ICommand
{
    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        FileSystemStatus fileSystemStatus = environment.FileSystemStatus;

        if (!fileSystemStatus.IsConnected)
            return CommandResult.Fail("The file system is not connected");

        fileSystemStatus.Disconnect();

        string message = $"disconnection is successful";

        return CommandResult.Success(message);
    }
}