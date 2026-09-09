using Itmo.ObjectOrientedProgramming.Lab2.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab2.Archiving;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

internal static class OwnMoq
{
    internal sealed class RecordingRecipient : IRecipient
    {
        public int DeliverCallCount { get; private set; }

        public List<Message> ReceivedMessages { get; } = new();

        public void Deliver(Message message)
        {
            DeliverCallCount++;
            ReceivedMessages.Add(message);
        }
    }

    internal sealed class RecordingLogger : ILogger
    {
        public int LogCallCount { get; private set; }

        public List<string> LogEntries { get; } = new();

        public void Log(string message)
        {
            LogCallCount++;
            LogEntries.Add(message);
        }
    }

    internal sealed class RecordingStorage : IStorage
    {
        public int WriteCallCount { get; private set; }

        public List<string> Lines { get; } = new();

        public void WriteLine(string text)
        {
            WriteCallCount++;
            Lines.Add(text);
        }
    }
}
