using Itmo.ObjectOrientedProgramming.Lab2.Archiving;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formatting;

public sealed class FileFormatter : IStorage
{
    private string FilePath { get; }

    public FileFormatter(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException("path", "Path must be non-empty");
        }

        string correctPath = Path.GetFullPath(path);

        string dir = Path.GetDirectoryName(correctPath) ?? ".";

        Directory.CreateDirectory(dir);

        FilePath = correctPath;
    }

    public void WriteLine(string text)
    {
        File.AppendAllText(FilePath, text + Environment.NewLine);
    }
}