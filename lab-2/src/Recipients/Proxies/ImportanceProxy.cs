using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients.Proxies;

public sealed class ImportanceProxy : IRecipient
{
    private Importance Threshold { get; }

    private IRecipient Recipient { get; }

    public ImportanceProxy(IRecipient recipient, Importance threshold)
    {
        Recipient = recipient ?? throw new ArgumentNullException("recipient", "Recipient cannot be null");
        Threshold = threshold;
    }

    public void Deliver(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message cannot be null");
        }

        if (message.Importance < Threshold) return;

        Recipient.Deliver(message);
    }
}
