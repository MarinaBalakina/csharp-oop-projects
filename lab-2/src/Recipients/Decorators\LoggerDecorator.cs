using Itmo.ObjectOrientedProgramming.Lab2.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Recipients.Decorators;

public class LoggerDecorator : IRecipient
{
    private IRecipient Recipient { get; }

    private ILogger Logger { get; }

    public LoggerDecorator(IRecipient recipient, ILogger logger)
    {
        Recipient = recipient ?? throw new ArgumentNullException("recipient", "Recipient cannot be null.");
        Logger = logger ?? throw new ArgumentNullException("logger", "Logger cannot be null.");
    }

    public void Deliver(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message cannot be null.");
        }

        Logger.Log($"Deliver -> {message.GetType().Name}{message.Header}");

        try
        {
            Recipient.Deliver(message);

            Logger.Log($"Deliver -> {message.GetType().Name}{message.Header}");
        }
        catch (Exception ex)
        {
            Logger.Log($"Deliver FAILED -> {message.GetType().Name}{message.Header}. Error: {ex.Message}");

            throw;
        }
    }
}
