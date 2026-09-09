using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class PinCode
{
    public string Value { get; }

    private const int PinLength = 4;

    private PinCode(string value)
    {
        Value = value;
    }

    public static OperationResultWithValue<PinCode> Create(string cur)
    {
        if (string.IsNullOrEmpty(cur))
        {
            return OperationResultWithValue<PinCode>.Fail(OperationError.InvalidPinCode);
        }

        if (!cur.All(char.IsDigit) || cur.Length != PinLength)
        {
            return OperationResultWithValue<PinCode>.Fail(OperationError.InvalidPinCode);
        }

        return OperationResultWithValue<PinCode>.Success(new PinCode(cur));
    }

    public bool Matches(PinCode curPin)
    {
        return Value == curPin.Value;
    }
}