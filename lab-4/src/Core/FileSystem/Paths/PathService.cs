namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

public sealed class PathService : IPathService
{
    public PathResolveResult GetPath(string rootPath, string localPath, string inputPath)
    {
        if (string.IsNullOrWhiteSpace(rootPath))
            return PathResolveResult.Fail("Root path cannot be empty");

        if (string.IsNullOrWhiteSpace(localPath))
            return PathResolveResult.Fail("Local path cannot be empty");

        if (string.IsNullOrWhiteSpace(inputPath))
            return PathResolveResult.Fail("Input path cannot be empty");

        string normalRootPath = Path.GetFullPath(rootPath);

        string absolutePath = BuildAbsolutePath(normalRootPath, localPath, inputPath);

        absolutePath = Path.GetFullPath(absolutePath);

        string? error = CheckInsideRoot(normalRootPath, absolutePath);

        if (error is not null) return PathResolveResult.Fail(error);

        string newLocalPath = GetLocalPath(normalRootPath, absolutePath);

        return PathResolveResult.Success(new PathResult(newLocalPath, absolutePath));
    }

    private string BuildAbsolutePath(string normalRootPath, string localPath, string inputPath)
    {
        if (inputPath.StartsWith('/') || inputPath.StartsWith('\\'))
        {
            string trimmed = inputPath.TrimStart('/', '\\');

            if (string.IsNullOrWhiteSpace(trimmed)) return normalRootPath;

            return Path.Combine(normalRootPath, trimmed);
        }

        if (localPath == ".")
        {
            return Path.Combine(normalRootPath, inputPath);
        }
        else
        {
            return Path.Combine(normalRootPath, localPath, inputPath);
        }
    }

    private string? CheckInsideRoot(string rootPath, string absolutePath)
    {
        if (absolutePath.Equals(rootPath, StringComparison.OrdinalIgnoreCase)) return null;

        string rootPrefix = rootPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                            + Path.DirectorySeparatorChar;

        if (!absolutePath.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return "Resulting path is not inside root path";
        }

        return null;
    }

    private string GetLocalPath(string rootPath, string absolutePath)
    {
        if (absolutePath.Equals(rootPath, StringComparison.OrdinalIgnoreCase)) return ".";

        string rootPrefix = rootPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                            + Path.DirectorySeparatorChar;

        string localPathFromRoot = absolutePath.Substring(rootPrefix.Length);

        localPathFromRoot = localPathFromRoot.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        if (string.IsNullOrWhiteSpace(localPathFromRoot)) return ".";
        return localPathFromRoot;
    }
}