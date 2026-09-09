using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

public class AccountNumber
{
    public string Value { get; }

    private AccountNumber(string value)
    {
        Value = value;
    }

    public static OperationResultWithValue<AccountNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return OperationResultWithValue<AccountNumber>.Fail(OperationError.InvalidAccountNumber);
        }

        if (!value.All(char.IsDigit))
        {
            return OperationResultWithValue<AccountNumber>.Fail(OperationError.InvalidAccountNumber);
        }

        return OperationResultWithValue<AccountNumber>.Success(new AccountNumber(value));
    }
}