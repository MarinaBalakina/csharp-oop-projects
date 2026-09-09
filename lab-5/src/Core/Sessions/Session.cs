namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public abstract class Session : ISession
{
    public SessionKey Key { get; }

    protected Session(SessionKey key)
    {
        Key = key;
    }
}