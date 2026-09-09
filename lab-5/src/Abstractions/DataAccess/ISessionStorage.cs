using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;

public interface ISessionStorage
{
    void Add(ISession session);

    ISession? FindByKey(SessionKey sessionKey);
}