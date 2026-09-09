using Itmo.ObjectOrientedProgramming.Lab2.Archiving;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public sealed class ArchiverRecipient : IRecipient
{
    private IArchiver Archiver { get; }

    public ArchiverRecipient(IArchiver archiver)
    {
        Archiver = archiver ?? throw new ArgumentNullException("archiver", "Archiver must be non-null");
    }

    public void Deliver(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "message must be non-null");
        }

        Archiver.Save(message);
    }
}
