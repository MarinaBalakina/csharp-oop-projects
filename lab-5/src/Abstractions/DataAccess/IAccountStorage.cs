using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;

public interface IAccountStorage
{
    Account? FindByNumber(AccountNumber accountNumber);

    void Add(Account account);

    void Update(Account account);
}
