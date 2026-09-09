using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.FileActions;

public class FileMoveCommand : ICommand
{
    private string SourcePath { get; }

    private string DestinationPath { get; }

    private IPathService PathService { get; }

    private IFileSystem FileSystem { get; }

    public FileMoveCommand(
        string sourcePath,
        string destinationPath,
        IPathService pathService,
        IFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(sourcePath);
        ArgumentNullException.ThrowIfNull(destinationPath);
        ArgumentNullException.ThrowIfNull(pathService);
        ArgumentNullException.ThrowIfNull(fileSystem);

        SourcePath = sourcePath;
        DestinationPath = destinationPath;
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

        if (rootPath is null) return CommandResult.Fail("Path is not specified");

        PathResolveResult source = PathService.GetPath(rootPath, localPath, SourcePath);
        if (!source.IsSuccess) return CommandResult.Fail(source.ErrorMessage);

        PathResolveResult destination = PathService.GetPath(rootPath, localPath, DestinationPath);
        if (!destination.IsSuccess) return CommandResult.Fail(destination.ErrorMessage);

        PathResult sourceResult = source.Result ?? throw new InvalidOperationException("Path is not set");
        if (!FileSystem.FileExists(sourceResult.AbsolutePath))
        {
            return CommandResult.Fail($"File '{sourceResult.AbsolutePath}' does not exist");
        }

        PathResult destinationResult = destination.Result ?? throw new InvalidOperationException("Path is not set");
        if (!FileSystem.DirectoryExists(destinationResult.AbsolutePath))
        {
            return CommandResult.Fail($"Directory '{destinationResult.AbsolutePath}' does not exist");
        }

        string fileName = Path.GetFileName(sourceResult.AbsolutePath);
        string targetPath = Path.Combine(destinationResult.AbsolutePath, fileName);
        if (FileSystem.FileExists(targetPath))
            return CommandResult.Fail("File alredy exists in distination directory");

        FileSystem.MoveFile(sourceResult.AbsolutePath, destinationResult.AbsolutePath);

        return CommandResult.Success("File moved");
    }
}