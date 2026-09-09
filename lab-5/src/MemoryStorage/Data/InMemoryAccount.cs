using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Data;

public class InMemoryAccount : IAccountStorage
{
    private Dictionary<string, Account> Accounts { get; }

    public InMemoryAccount()
    {
        Accounts = new Dictionary<string, Account>();
    }

    public Account? FindByNumber(AccountNumber accountNumber)
    {
        string key = accountNumber.Value;

        return Accounts.TryGetValue(key, out Account? account) ? account : null;
    }

    public void Add(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        string key = account.Number.Value;

        if (Accounts.ContainsKey(key)) return;

        Accounts.Add(key, account);
    }

    public void Update(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        string key = account.Number.Value;

        Accounts[key] = account;
    }
}