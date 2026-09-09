using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;

public class FakeAccountFactory : IAccountFactory
{
    public OperationResultWithValue<Account> Create(string accountNumber, string pinCode, decimal initialBalance)
    {
        OperationResultWithValue<AccountNumber> numberResult = AccountNumber.Create(accountNumber);
        if (!numberResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(numberResult.Error);

        OperationResultWithValue<PinCode> pinResult = PinCode.Create(pinCode);
        if (!pinResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(pinResult.Error);

        OperationResultWithValue<MoneyAmount> balanceResult = MoneyAmount.Create(initialBalance);
        if (!balanceResult.IsSuccess)
            return OperationResultWithValue<Account>.Fail(balanceResult.Error);

        var account = new Account(
            numberResult.GetValue,
            pinResult.GetValue,
            balanceResult.GetValue);

        return OperationResultWithValue<Account>.Success(account);
    }

    public OperationResultWithValue<Account> Create(
        AccountNumber accountNumber,
        PinCode pinCode,
        MoneyAmount amount)
    {
        var account = new Account(accountNumber, pinCode, amount);

        return OperationResultWithValue<Account>.Success(account);
    }
}