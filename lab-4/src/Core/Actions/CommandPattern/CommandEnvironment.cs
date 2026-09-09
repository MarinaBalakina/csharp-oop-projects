using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;

public class CommandEnvironment
{
    public FileSystemStatus FileSystemStatus { get; }

    public CommandEnvironment(FileSystemStatus fileSystemStatus)
    {
        ArgumentNullException.ThrowIfNull(fileSystemStatus);
        FileSystemStatus = fileSystemStatus;
    }
}
