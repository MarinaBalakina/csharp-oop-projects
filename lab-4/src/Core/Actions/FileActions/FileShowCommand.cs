using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.FileActions;

public class FileShowCommand : ICommand
{
    private string Path { get; }

    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public FileShowCommand(string path, IPathService pathService, IFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(fileSystem);
        ArgumentNullException.ThrowIfNull(pathService);

        Path = path;
        PathService = pathService;
        FileSystem = fileSystem;
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        FileSystemStatus systemStatus = environment.FileSystemStatus;
        ArgumentNullException.ThrowIfNull(systemStatus);

        if (!systemStatus.IsConnected) return CommandResult.Fail("File system is not connected");

        string? rootPath = systemStatus.RootPath;
        string localPath = systemStatus.LocalPath;

        if (rootPath is null) return CommandResult.Fail("path is not set");

        PathResolveResult result = PathService.GetPath(rootPath, localPath, Path);
        if (!result.IsSuccess) return CommandResult.Fail(result.ErrorMessage);

        PathResult path = result.Result ?? throw new InvalidOperationException("Path is not set");
        if (!FileSystem.FileExists(path.AbsolutePath)) return CommandResult.Fail("File is not found");

        string content = FileSystem.ReadFile(path.AbsolutePath);

        return CommandResult.Success(content);
    }
}