using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public class UserSession : Session, IUserSession
{
    public AccountNumber AccNumber { get; }

    public UserSession(SessionKey key, AccountNumber accNumber) : base(key)
    {
        AccNumber = accNumber ?? throw new ArgumentNullException(nameof(accNumber));
    }
}