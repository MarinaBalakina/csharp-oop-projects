using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;

public class FakeAccountStorage : IAccountStorage
{
    private Dictionary<string, Account> Accounts { get; }

    public FakeAccountStorage()
    {
        Accounts = new Dictionary<string, Account>();
    }

    public void Add(Account account)
    {
        string key = account.Number.Value;
        Accounts[key] = account;
    }

    public void Update(Account account)
    {
        string key = account.Number.Value;
        Accounts[key] = account;
    }

    public Account? FindByNumber(AccountNumber accountNumber)
    {
        string key = accountNumber.Value;
        return Accounts.TryGetValue(key, out Account? account) ? account : null;
    }
}