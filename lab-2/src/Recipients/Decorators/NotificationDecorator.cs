using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Notifications;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients.Decorators;

public class NotificationDecorator : IRecipient
{
    private IRecipient Recipient { get; }

    private INotificationSystem Notificator { get; }

    private IReadOnlyList<string> KeyWords { get; }

    public NotificationDecorator(IRecipient recipient, INotificationSystem notificator, IReadOnlyList<string> keyWords)
    {
        if (recipient is null)
        {
            throw new ArgumentNullException("recipient", "Recipient cannot be null");
        }

        if (notificator is null)
        {
            throw new ArgumentNullException("notificator", "Notificator cannot be null");
        }

        if (keyWords is null)
        {
            throw new ArgumentNullException("keyWords", "KeyWords cannot be null");
        }

        Recipient = recipient;

        Notificator = notificator;

        var cleaned = new List<string>();
        foreach (string word in keyWords)
        {
            if (!string.IsNullOrWhiteSpace(word)) cleaned.Add(word);
        }

        KeyWords = cleaned.AsReadOnly();
    }

    public void Deliver(Message message)
    {
        if (message is null)
        {
            throw new ArgumentNullException("message", "Message cannot be null");
        }

        string text = $"{message.Header}\n{message.Body}".ToLowerInvariant();

        bool hasKeyWord = false;

        foreach (string key in KeyWords)
        {
            if (text.Contains(key, StringComparison.OrdinalIgnoreCase))
            {
                hasKeyWord = true;
                break;
            }
        }

        if (hasKeyWord)
        {
            Notificator.Notify($"Suspicious text. Message: {message.Header}");
        }

        Recipient.Deliver(message);
    }
}
