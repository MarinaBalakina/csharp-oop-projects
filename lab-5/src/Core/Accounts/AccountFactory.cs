using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class AccountFactory : IAccountFactory
{
    public OperationResultWithValue<Account> Create(string accountNumber, string pinCode, decimal initialBalance)
    {
        OperationResultWithValue<AccountNumber> numberResult = AccountNumber.Create(accountNumber);
        if (!numberResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(numberResult.Error);

        OperationResultWithValue<PinCode> pinResult = PinCode.Create(pinCode);
        if (!pinResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(pinResult.Error);

        OperationResultWithValue<MoneyAmount> amountResult = MoneyAmount.Create(initialBalance);
        if (!amountResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(amountResult.Error);

        return Create(numberResult.GetValue, pinResult.GetValue, amountResult.GetValue);
    }

    public OperationResultWithValue<Account> Create(
        AccountNumber accountNumber,
        PinCode pinCode,
        MoneyAmount amount)
    {
        ArgumentNullException.ThrowIfNull(accountNumber);
        ArgumentNullException.ThrowIfNull(pinCode);
        ArgumentNullException.ThrowIfNull(amount);

        var account = new Account(accountNumber, pinCode, amount);
        return OperationResultWithValue<Account>.Success(account);
    }
}