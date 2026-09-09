using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public sealed class User
{
    public string Name { get; }

    public Guid UserId { get; } = Guid.NewGuid();

    public IReadOnlyList<UserMessageRecord> AllMessage => AllMessagesInternal.AsReadOnly();

    private List<UserMessageRecord> AllMessagesInternal { get; } = new();

    public User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException("name", "Name must be non-empty");
        }

        Name = name;
    }

    public void Receive(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message must be non-null");
        }

        AllMessagesInternal.Add(new UserMessageRecord(message));
    }

    public void MarkAsRead(Guid recordId)
    {
        UserMessageRecord record = AllMessagesInternal.FirstOrDefault(r => r.RecordId == recordId)
                                   ?? throw new KeyNotFoundException("Record not found");

        record.MarkRead();
    }
}