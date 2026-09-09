namespace Itmo.ObjectOrientedProgramming.Lab2.Messages;

public class Message
{
    public string Header { get; }

    public string Body { get; }

    public Importance Importance { get; }

    public DateTime CreatedTime { get; }

    public Message(string header, string body, Importance importance)
    {
        if (string.IsNullOrWhiteSpace(header))
        {
            throw new ArgumentNullException("header", "header must be non-empty");
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentNullException("body", "body must be non-empty");
        }

        Header = header;
        Body = body;
        Importance = importance;
        CreatedTime = DateTime.UtcNow;
    }
}