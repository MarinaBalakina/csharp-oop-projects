using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients;

public sealed class UserRecipient : IRecipient
{
    private User User { get; }

    public UserRecipient(User user)
    {
        User = user ?? throw new ArgumentNullException("user", "User must be non-null.");
    }

    public void Deliver(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message must be non-null.");
        }

        User.Receive(message);
    }
}