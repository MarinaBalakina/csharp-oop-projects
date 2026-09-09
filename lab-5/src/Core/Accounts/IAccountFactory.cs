using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public interface IAccountFactory
{
    OperationResultWithValue<Account> Create(string accountNumber, string pinCode, decimal initialBalance);

    OperationResultWithValue<Account> Create(AccountNumber accountNumber, PinCode pinCode, MoneyAmount amount);
}