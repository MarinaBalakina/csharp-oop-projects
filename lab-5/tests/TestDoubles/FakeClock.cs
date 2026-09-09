using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Time;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;

public class FakeClock : IClock
{
    public DateTimeOffset FixedNow { get; set; }

    public FakeClock()
    {
        FixedNow = DateTimeOffset.Now;
    }

    public DateTimeOffset Now()
    {
        return FixedNow;
    }
}