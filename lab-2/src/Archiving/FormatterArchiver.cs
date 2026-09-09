using Itmo.ObjectOrientedProgramming.Lab2.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab2.Archiving;

public sealed class FormatterArchiver : IArchiver
{
    private FormattingWrapper Wrapper { get; }

    public FormatterArchiver(FormattingWrapper wrapper)
    {
        Wrapper = wrapper ?? throw new ArgumentNullException("wrapper");
    }

    public void Save(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message", "Message must be  non-null.");
        }

        (string, string) formatted = Wrapper.Format(message);
        Wrapper.Save(formatted);
    }
}
