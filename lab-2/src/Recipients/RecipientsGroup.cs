using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public sealed class RecipientsGroup : IRecipient
{
    public IReadOnlyList<IRecipient> Recipients => RecipientsInternal;

    private List<IRecipient> RecipientsInternal { get; } = new();

    public RecipientsGroup(IEnumerable<IRecipient> adressees)
    {
        if (adressees == null)
        {
            throw new ArgumentNullException("adressees", "adressees must be non-null");
        }

        RecipientsInternal.AddRange(adressees);
    }

    public void AddRecipients(IRecipient recipient)
    {
        if (recipient == null)
        {
            throw new ArgumentNullException("recipient", "recipient must be non-null");
        }

        RecipientsInternal.Add(recipient);
    }

    public void AddRecipients(IEnumerable<IRecipient> recipients)
    {
        if (recipients == null)
        {
            throw new ArgumentNullException("recipients", "recipients must be non-null");
        }

        RecipientsInternal.AddRange(recipients);
    }

    public void Deliver(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "message must be non-null");
        }

        foreach (IRecipient recipient in RecipientsInternal.ToArray())
        {
            recipient.Deliver(message);
        }
    }
}