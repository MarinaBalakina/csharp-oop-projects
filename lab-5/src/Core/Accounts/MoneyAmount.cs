using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class MoneyAmount
{
    public decimal Value { get; }

    private MoneyAmount(decimal value)
    {
        Value = value;
    }

    public static OperationResultWithValue<MoneyAmount> Create(decimal cur)
    {
        if (cur < 0)
        {
            return OperationResultWithValue<MoneyAmount>.Fail(OperationError.AmountMustBePositive);
        }

        return OperationResultWithValue<MoneyAmount>.Success(new MoneyAmount(cur));
    }

    public MoneyAmount Add(TransactionAmount other)
    {
        decimal newAmount = Value + other.Value;

        return new MoneyAmount(newAmount);
    }

    public OperationResultWithValue<MoneyAmount> TrySubtract(TransactionAmount other)
    {
        if (Value < other.Value)
        {
            return OperationResultWithValue<MoneyAmount>.Fail(OperationError.InsufficientFunds);
        }

        decimal newAmount = Value - other.Value;

        return OperationResultWithValue<MoneyAmount>.Success(new MoneyAmount(newAmount));
    }
}