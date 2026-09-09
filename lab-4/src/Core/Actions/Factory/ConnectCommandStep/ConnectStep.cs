using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.ConnectionActions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.ConnectCommandStep;

public class ConnectStep : ICommandCreateStep
{
    private IFileSystem FileSystem { get; }

    public ConnectStep(IFileSystem fileSystem)
    {
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public bool CanHandle(ParserResult result)
    {
        return string.Equals("connect", result.CommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ICommand Create(ParserResult result)
    {
        if (result.Arguments.Count == 0)
            return new ErrorCommand("Command 'connect' must have an address");

        string address = result.Arguments[0];

        string? modeFlag = null;
        if (result.Flags.TryGetValue("m", out string? flagValue))
        {
            modeFlag = flagValue;
        }

        FileSystemMode mode;

        if (string.IsNullOrWhiteSpace(modeFlag))
        {
            mode = FileSystemMode.Local;
        }
        else if (modeFlag.Equals("local", StringComparison.OrdinalIgnoreCase))
        {
            mode = FileSystemMode.Local;
        }
        else
        {
            return new ErrorCommand($"Unknown mode '{modeFlag}'");
        }

        return new ConnectCommand(address, mode, FileSystem);
    }
}