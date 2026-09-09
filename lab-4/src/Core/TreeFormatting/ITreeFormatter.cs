namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

public interface ITreeFormatter
{
    string Format(string startDirectory, int maxDepth);
}