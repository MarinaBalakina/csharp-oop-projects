using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Security;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Providers;

public class SystemPasswordProvider : ISystemPassword
{
    private string Password { get; }

    public SystemPasswordProvider(string password)
    {
        Password = password;
    }

    public string GetPassword()
    {
        return Password;
    }
}