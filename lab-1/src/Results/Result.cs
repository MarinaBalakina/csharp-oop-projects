namespace Itmo.ObjectOrientedProgramming.Lab1.Results;

public sealed class Result
{
    public bool IsSuccess { get; }

    public string Message { get; }

    private Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result Fail(string reason)
    {
        return new Result(false, reason);
    }
}
