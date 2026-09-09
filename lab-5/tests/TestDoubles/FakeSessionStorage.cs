using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;

public class FakeSessionStorage : ISessionStorage
{
    private Dictionary<Guid, ISession> Sessions { get; }

    public FakeSessionStorage()
    {
        Sessions = new Dictionary<Guid, ISession>();
    }

    public void Add(ISession session)
    {
        Guid key = session.Key.Value;
        Sessions[key] = session;
    }

    public ISession? FindByKey(SessionKey sessionKey)
    {
        Guid key = sessionKey.Value;
        return Sessions.TryGetValue(key, out ISession? session) ? session : null;
    }
}