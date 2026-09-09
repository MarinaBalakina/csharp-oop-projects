namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

public interface IPathService
{
    PathResolveResult GetPath(string rootPath, string localPath, string inputPath);
}