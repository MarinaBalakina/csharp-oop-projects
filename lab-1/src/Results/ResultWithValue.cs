namespace Itmo.ObjectOrientedProgramming.Lab1.Results;

public sealed class ResultWithValue<T> where T : struct
{
    public bool IsSuccess { get; }

    public string Message { get; }

    public T Value { get; }

    private ResultWithValue(bool result, string message, T value)
    {
        IsSuccess = result;
        Message = message;
        Value = value;
    }

    public static ResultWithValue<T> Success(T value)
    {
        return new ResultWithValue<T>(true, string.Empty, value);
    }

    public static ResultWithValue<T> Fail(string reason)
    {
        return new ResultWithValue<T>(false, reason, default);
    }
}