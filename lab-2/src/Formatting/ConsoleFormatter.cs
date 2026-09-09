using Itmo.ObjectOrientedProgramming.Lab2.Archiving;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatting;

public class ConsoleFormatter : IStorage
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}