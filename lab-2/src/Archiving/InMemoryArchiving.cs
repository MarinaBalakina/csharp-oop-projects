using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archiving;

public sealed class InMemoryArchiving : IArchiver
{
    private List<Message> Storage { get; } = new();

    public void Save(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "message must be non-null.");
        }

        Storage.Add(message);
    }
}