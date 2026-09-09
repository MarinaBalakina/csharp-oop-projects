using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.FileActions;

public class FileRenameCommand : ICommand
{
    private string CurrentPath { get; }

    private string Name { get; }

    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public FileRenameCommand(string path, string name, IPathService pathService, IFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(pathService);
        ArgumentNullException.ThrowIfNull(fileSystem);

        CurrentPath = path;
        Name = name;
        PathService = pathService;
        FileSystem = fileSystem;
    }

    public CommandResult Execute(CommandEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        FileSystemStatus systemStatus = environment.FileSystemStatus;
        ArgumentNullException.ThrowIfNull(systemStatus);

        if (!systemStatus.IsConnected)
        {
            return CommandResult.Fail("File system is not connected");
        }

        string? rootPath = systemStatus.RootPath;
        string localPath = systemStatus.LocalPath;

        if (rootPath is null) return CommandResult.Fail("Root path or local path is null");

        if (Name.Contains(Path.DirectorySeparatorChar, StringComparison.Ordinal)
            || Name.Contains(Path.AltDirectorySeparatorChar, StringComparison.Ordinal))
        {
            return CommandResult.Fail("New name must be not contain path separators");
        }

        PathResolveResult result = PathService.GetPath(rootPath, localPath, CurrentPath);
        if (!result.IsSuccess) return CommandResult.Fail(result.ErrorMessage);

        PathResult path = result.Result ?? throw new InvalidOperationException("Path is not set");
        if (!FileSystem.FileExists(path.AbsolutePath)) return CommandResult.Fail("Path does not exist");

        string? directoryName = Path.GetDirectoryName(path.AbsolutePath);
        if (directoryName is null)
        {
            return CommandResult.Fail("Directory path is not set");
        }

        string newPath = Path.Combine(directoryName, Name);
        if (FileSystem.FileExists(newPath))
        {
            return CommandResult.Fail("File with the same name already exists");
        }

        FileSystem.RenameFile(path.AbsolutePath, Name);

        return CommandResult.Success("File renamed");
    }
}