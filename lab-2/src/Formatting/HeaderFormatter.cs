namespace Itmo.ObjectOrientedProgramming.Lab2.Formatting;

public sealed class HeaderFormatter : IMessageFormatter
{
    public string WriteLine(string text) => "# " + text;
}