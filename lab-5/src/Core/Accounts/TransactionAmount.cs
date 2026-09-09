using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class TransactionAmount
{
    public decimal Value { get; }

    private TransactionAmount(decimal value)
    {
        Value = value;
    }

    public static OperationResultWithValue<TransactionAmount> Create(decimal value)
    {
        if (value <= 0)
            return OperationResultWithValue<TransactionAmount>.Fail(OperationError.AmountMustBePositive);

        return OperationResultWithValue<TransactionAmount>.Success(new TransactionAmount(value));
    }
}