using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.History;

public class OperationName
{
    public string Value { get; }

    private OperationName(string value)
    {
        Value = value;
    }

    public static OperationResultWithValue<OperationName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return OperationResultWithValue<OperationName>.Fail(OperationError.InvalidOperationRecord);

        return OperationResultWithValue<OperationName>.Success(new OperationName(value));
    }

    public override string ToString()
    {
        return Value;
    }
}