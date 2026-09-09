using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.TreeActions;

public sealed class TreeGotoCommand : ICommand
{
    private string Path { get; }

    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public TreeGotoCommand(string path, IPathService pathService, IFileSystem fileSystem)
    {
        ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
        ArgumentNullException.ThrowIfNull(pathService, nameof(pathService));
        ArgumentNullException.ThrowIfNull(fileSystem, nameof(fileSystem));

        Path = path;
        PathService = pathService;
        FileSystem = fileSystem;
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment, nameof(environment));

        FileSystemStatus systemStatus = environment.FileSystemStatus;
        ArgumentNullException.ThrowIfNull(systemStatus, "File system status cannot be null");

        if (!systemStatus.IsConnected) return CommandResult.Fail("File system is not connected");

        string? rootPath = systemStatus.RootPath;
        string localPath = systemStatus.LocalPath;

        if (rootPath == null || localPath == null) throw new InvalidOperationException("Path is not set");

        PathResolveResult result = PathService.GetPath(rootPath, localPath, Path);

        if (!result.IsSuccess) return CommandResult.Fail(result.ErrorMessage);

        PathResult path = result.Result ?? throw new InvalidOperationException("Path is not set");

        if (!FileSystem.DirectoryExists(path.AbsolutePath))
            return CommandResult.Fail("Directory does not exist");

        systemStatus.SetLocalPath(path.LocalPath);

        return CommandResult.Success($"Current directory: {path.LocalPath}");
    }
}