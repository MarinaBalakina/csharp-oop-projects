using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Time;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Providers;

public class SystemTimeProvider : IClock
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.Now;
    }
}