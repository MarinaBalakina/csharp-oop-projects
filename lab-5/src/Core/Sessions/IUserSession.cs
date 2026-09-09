using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public interface IUserSession : ISession
{
    AccountNumber AccNumber { get; }
}