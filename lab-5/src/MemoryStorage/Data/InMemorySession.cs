using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Data;

public class InMemorySession : ISessionStorage
{
    private Dictionary<Guid, ISession> Sessions { get; }

    public InMemorySession()
    {
        Sessions = new Dictionary<Guid, ISession>();
    }

    public void Add(ISession session)
    {
        ArgumentNullException.ThrowIfNull(session);

        Guid key = session.Key.Value;

        Sessions.Add(key, session);
    }

    public ISession? FindByKey(SessionKey sessionKey)
    {
        ArgumentNullException.ThrowIfNull(sessionKey);

        Guid key = sessionKey.Value;

        return Sessions.TryGetValue(key, out ISession? session) ? session : null;
    }
}