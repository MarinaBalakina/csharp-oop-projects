using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab2.Topics;

public sealed class Topic
{
    public string Name { get; }

    private List<IRecipient> Recipients { get; } = new();

    public Topic(string name, IEnumerable<IRecipient>? initial = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException("name", "Name must be non-empty");
        }

        if (initial != null)
        {
            Recipients.AddRange(initial);
        }

        Name = name.Trim();
    }

    public void AddRecipient(IRecipient recipient)
    {
        if (recipient == null)
        {
            throw new ArgumentNullException("recipient", "Recipient must not be null");
        }

        Recipients.Add(recipient);
    }

    public void RemoveRecipient(IRecipient recipient)
    {
        if (recipient == null)
        {
            throw new ArgumentNullException("recipient", "Recipient must not be null");
        }

        Recipients.Remove(recipient);
    }

    public void Send(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message must not be null");
        }

        foreach (IRecipient recipient in Recipients)
        {
            recipient.Deliver(message);
        }
    }
}
