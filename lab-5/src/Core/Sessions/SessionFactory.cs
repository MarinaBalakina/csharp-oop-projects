using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public sealed class SessionFactory : ISessionFactory
{
    public UserSession CreateUserSession(AccountNumber accNumber)
    {
        ArgumentNullException.ThrowIfNull(accNumber);

        var key = SessionKey.CreateNew();
        return new UserSession(key, accNumber);
    }

    public AdminSession CreateAdminSession()
    {
        var key = SessionKey.CreateNew();
        return new AdminSession(key);
    }
}