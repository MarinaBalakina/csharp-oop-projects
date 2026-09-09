namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

public sealed class OperationResultWithValue<T>
{
    public bool IsSuccess { get; }

    public OperationError Error { get; }

    private T? Value { get; }

    private OperationResultWithValue(bool isSuccess, T? value, OperationError error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public T GetValue
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException("Can't get value.");

            if (Value is null)
                throw new InvalidOperationException("Value is null in successful result.");

            return Value;
        }
    }

    public static OperationResultWithValue<T> Success(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new(true, value, OperationError.None);
    }

    public static OperationResultWithValue<T> Fail(OperationError error)
    {
        if (error == OperationError.None)
        {
            throw new ArgumentException("Error must not be None", nameof(error));
        }

        return new OperationResultWithValue<T>(false, value: default, error);
    }
}