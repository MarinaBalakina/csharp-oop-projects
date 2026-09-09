using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.FileActions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.FileCommandStep;

public class FileMoveStep : ICommandCreateStep
{
    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public FileMoveStep(IPathService pathService, IFileSystem fileSystem)
    {
        PathService = pathService ?? throw new ArgumentNullException(nameof(pathService));
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public bool CanHandle(ParserResult result)
    {
        if (!string.Equals("file", result.CommandName, StringComparison.OrdinalIgnoreCase)) return false;

        if (result.Arguments.Count == 0) return false;

        string subCommandName = result.Arguments[0];
        return string.Equals("move", subCommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ICommand Create(ParserResult result)
    {
        if (result.Arguments.Count < 3)
            return new ErrorCommand("Command 'move file' must have source and destination path");

        string sourcePath = result.Arguments[1];
        string destinationPath = result.Arguments[2];

        if (string.IsNullOrWhiteSpace(sourcePath)) return new ErrorCommand("Path must be non empty");

        if (string.IsNullOrWhiteSpace(destinationPath)) return new ErrorCommand("Path must be non empty");

        return new FileMoveCommand(sourcePath, destinationPath, PathService, FileSystem);
    }
}