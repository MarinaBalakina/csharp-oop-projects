using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class UserMessageRecord
{
    public ReadStatus Status { get; private set; } = ReadStatus.Unread;

    public DateTime ReceivedTime { get; }

    public DateTime ReadTime { get; private set; }

    public Guid RecordId { get; }

    public UserMessageRecord(Message currentMessage)
    {
        if (currentMessage == null)
        {
            throw new ArgumentNullException("currentMessage", "Message must be non-null");
        }

        ReceivedTime = DateTime.UtcNow;
        RecordId = Guid.NewGuid();
    }

    public void MarkRead()
    {
        if (Status == ReadStatus.Unread)
        {
            Status = ReadStatus.Read;
            ReadTime = DateTime.UtcNow;
        }
        else
        {
            throw new InvalidOperationException("Message already marked as read.");
        }
    }
}