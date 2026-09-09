namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

public class AdminSession : Session, IAdminSession
{
    public AdminSession(SessionKey key) : base(key) { }
}