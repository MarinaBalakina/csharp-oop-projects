using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class Account
{
    public AccountNumber Number { get; }

    public MoneyAmount ViewBalance => Balance;

    private MoneyAmount Balance { get; set; }

    private PinCode Pin { get; }

    public Account(AccountNumber number, PinCode pin, MoneyAmount balance)
    {
        Number = number;
        Pin = pin;
        Balance = balance;
    }

    public bool IsPinValid(PinCode pinCode)
    {
        return Pin.Matches(pinCode);
    }

    public OperationResult IncreaseBalance(TransactionAmount amount)
    {
        ArgumentNullException.ThrowIfNull(amount);

        Balance = Balance.Add(amount);

        return OperationResult.Success();
    }

    public OperationResult DecreaseBalance(TransactionAmount amount)
    {
        ArgumentNullException.ThrowIfNull(amount);

        OperationResultWithValue<MoneyAmount> newBalance = Balance.TrySubtract(amount);
        if (!newBalance.IsSuccess)
        {
            return OperationResult.Fail(newBalance.Error);
        }

        Balance = newBalance.GetValue;
        return OperationResult.Success();
    }
}
