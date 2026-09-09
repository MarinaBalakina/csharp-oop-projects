using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.ConnectionActions;

public class ConnectCommand : ICommand
{
    private string Address { get; }

    private FileSystemMode Mode { get; }

    private IFileSystem FileSystem { get; }

    public ConnectCommand(string address, FileSystemMode mode, IFileSystem fileSystem)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException(nameof(address));

        if (!Path.IsPathRooted(address))
            throw new ArgumentException("Address must be an absolute path", nameof(address));

        Address = address;
        Mode = mode;
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        if (!FileSystem.DirectoryExists(Address))
            return CommandResult.Fail("Root directory does not exist");

        FileSystemStatus fileSystemStatus = environment.FileSystemStatus
                                            ?? throw new InvalidOperationException(
                                                "file system status is not configured");

        fileSystemStatus.Connect(Address, Mode);

        return CommandResult.Success($"Connecting to {Address} in Mode {Mode}.");
    }
}
