using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.TreeActions;

public class TreeListCommand : ICommand
{
    private int Depth { get; }

    private IPathService PathService { get; }

    private ITreeFormatter Formatter { get; }

    public TreeListCommand(int depth, IPathService pathService, ITreeFormatter formatter)
    {
        if (depth < 1) throw new ArgumentOutOfRangeException(nameof(depth), "Deep must be greater than or equal to 1");

        ArgumentNullException.ThrowIfNull(pathService);
        ArgumentNullException.ThrowIfNull(formatter);

        Depth = depth;
        PathService = pathService;
        Formatter = formatter;
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        FileSystemStatus systemStatus = environment.FileSystemStatus
                                        ?? throw new InvalidOperationException("File system status is not configured");

        if (!systemStatus.IsConnected) return CommandResult.Fail("File system is not connected");

        string rootPath = systemStatus.RootPath ?? throw new InvalidOperationException("Root path is not set");
        string localPath = systemStatus.LocalPath ?? ".";

        PathResolveResult result = PathService.GetPath(rootPath, localPath, ".");
        if (!result.IsSuccess) return CommandResult.Fail(result.ErrorMessage);

        PathResult path = result.Result ?? throw new InvalidOperationException("Path is not set");
        string currentDirectory = path.AbsolutePath;

        string treeText = Formatter.Format(currentDirectory, Depth);
        return CommandResult.Success(treeText);
    }
}