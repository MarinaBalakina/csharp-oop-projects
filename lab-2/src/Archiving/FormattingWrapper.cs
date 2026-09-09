using Itmo.ObjectOrientedProgramming.Lab2.Formatting;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archiving;

public sealed class FormattingWrapper
{
    private IMessageFormatter HeaderFormatting { get; }

    private IMessageFormatter BodyFormatting { get; }

    private IStorage Storage { get; }

    public FormattingWrapper(IMessageFormatter header, IMessageFormatter body, IStorage storage)
    {
        HeaderFormatting = header ?? throw new ArgumentNullException("header");
        BodyFormatting = body ?? throw new ArgumentNullException("body");
        Storage = storage ?? throw new ArgumentNullException("storage");
    }

    public (string Header, string Body) Format(Message msg)
    {
        ArgumentNullException.ThrowIfNull(msg);

        string header = HeaderFormatting.WriteLine(msg.Header);

        string importanceBody = "**Importance:** " + msg.Importance + Environment.NewLine + msg.Body;

        string body = BodyFormatting.WriteLine(importanceBody);

        return (header, body);
    }

    public void Save((string Header, string Body) formatted)
    {
        Storage.WriteLine(formatted.Header);
        Storage.WriteLine(formatted.Body);
    }
}