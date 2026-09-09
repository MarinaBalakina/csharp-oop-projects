namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

public sealed class OperationResult
{
    public bool IsSuccess { get; }

    public OperationError Error { get; }

    private OperationResult(bool isSuccess, OperationError error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static OperationResult Success() => new(true, OperationError.None);

    public static OperationResult Fail(OperationError error)
    {
        if (error == OperationError.None)
        {
            throw new ArgumentException("Error must not be none", nameof(error));
        }

        return new(false, error);
    }
}