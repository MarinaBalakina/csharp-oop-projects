namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

public sealed class PathResult
{
    public string LocalPath { get; private set; }

    public string AbsolutePath { get; private set; }

    public PathResult(string localPath, string absolutePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(localPath);

        ArgumentException.ThrowIfNullOrWhiteSpace(absolutePath);

        LocalPath = localPath;
        AbsolutePath = absolutePath;
    }
}