namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;

public sealed class PathResolveResult
{
    public bool IsSuccess { get; }

    public PathResult? Result { get; }

    public string ErrorMessage { get; }

    private PathResolveResult(bool isSuccess, PathResult? result, string errorMessage)
    {
        IsSuccess = isSuccess;
        Result = result;
        ErrorMessage = errorMessage;
    }

    public static PathResolveResult Success(PathResult? result) => new PathResolveResult(true, result, string.Empty);

    public static PathResolveResult Fail(string error) => new PathResolveResult(false, null, error);
}