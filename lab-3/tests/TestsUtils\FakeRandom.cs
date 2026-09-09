using Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;

public sealed class FakeRandom : IRandom
{
    public Queue<int> Queue { get; }

    public FakeRandom(params int[] numbers)
    {
        Queue = new Queue<int>(numbers ?? Array.Empty<int>());
    }

    public int PickIndex(int maxValue)
    {
        if (maxValue <= 0) return 0;
        int chosenIndex = Queue.Count == 0 ? 0 : Queue.Dequeue();

        int clampedIndex = chosenIndex;

        if (clampedIndex < 0) clampedIndex = 0;
        if (clampedIndex >= maxValue) clampedIndex = maxValue - 1;

        return clampedIndex;
    }
}
