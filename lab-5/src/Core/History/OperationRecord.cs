using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.History;

public class OperationRecord
{
    public Guid Id { get; }

    public AccountNumber Number { get; }

    public OperationName Name { get; }

    public TransactionAmount? Amount { get; }

    public MoneyAmount BalanceAfter { get; }

    public DateTimeOffset OccurredAt { get; }

    public OperationRecord(
        Guid id,
        AccountNumber number,
        OperationName name,
        TransactionAmount? amount,
        MoneyAmount balanceAfter,
        DateTimeOffset occurredAt)
    {
        Id = id;
        Number = number;
        Name = name;
        Amount = amount;
        BalanceAfter = balanceAfter;
        OccurredAt = occurredAt;
    }

    public static OperationResultWithValue<OperationRecord> Create(
        AccountNumber accountNumber,
        OperationName name,
        TransactionAmount? amount,
        MoneyAmount balanceAfter,
        DateTimeOffset occurredAt)
    {
        if (accountNumber is null)
            return OperationResultWithValue<OperationRecord>.Fail(OperationError.InvalidOperationRecord);

        if (balanceAfter is null)
            return OperationResultWithValue<OperationRecord>.Fail(OperationError.InvalidOperationRecord);

        if (occurredAt == default)
            return OperationResultWithValue<OperationRecord>.Fail(OperationError.InvalidOperationRecord);

        return OperationResultWithValue<OperationRecord>.Success(new OperationRecord(
            Guid.NewGuid(),
            accountNumber,
            name,
            amount,
            balanceAfter,
            occurredAt));
    }
}