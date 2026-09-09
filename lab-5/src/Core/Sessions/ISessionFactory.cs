using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public interface ISessionFactory
{
    UserSession CreateUserSession(AccountNumber accNumber);

    AdminSession CreateAdminSession();
}