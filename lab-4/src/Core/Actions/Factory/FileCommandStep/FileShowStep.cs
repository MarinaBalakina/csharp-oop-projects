using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.FileActions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.FileCommandStep;

public class FileShowStep : ICommandCreateStep
{
    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public FileShowStep(IPathService pathService, IFileSystem fileSystem)
    {
        PathService = pathService ?? throw new ArgumentNullException(nameof(pathService));
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public bool CanHandle(ParserResult result)
    {
        if (!string.Equals("file", result.CommandName, StringComparison.OrdinalIgnoreCase)) return false;

        if (result.Arguments.Count == 0) return false;

        string subCommandName = result.Arguments[0];
        return string.Equals("show", subCommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ICommand Create(ParserResult result)
    {
        if (result.Arguments.Count < 2) return new ErrorCommand("Command 'file show' must have path");

        string path = result.Arguments[1];
        if (string.IsNullOrWhiteSpace(path)) return new ErrorCommand("Path must be non empty");

        string mode = "console";
        if (result.Flags.TryGetValue("m", out string? flagValue) && !string.IsNullOrWhiteSpace(flagValue))
        {
            mode = flagValue;
        }

        if (!string.Equals(mode, "console", StringComparison.OrdinalIgnoreCase))
        {
            return new ErrorCommand($"Unsupported mode: {mode}");
        }

        return new FileShowCommand(path, PathService, FileSystem);
    }
}