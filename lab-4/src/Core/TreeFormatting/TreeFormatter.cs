using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

public class TreeFormatter : ITreeFormatter
{
    public TreeFormatterOptions Options { get; }

    private IFileSystem FileSystem { get; }

    public TreeFormatter(TreeFormatterOptions options, IFileSystem fileSystem)
    {
        Options = options ?? TreeFormatterOptions.Default;
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public string Format(string startDirectory, int maxDepth)
    {
        ArgumentNullException.ThrowIfNull(startDirectory);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxDepth, 0);

        var builder = new StringBuilder();

        string rootName = Path.GetFileName(startDirectory);
        if (string.IsNullOrWhiteSpace(rootName)) rootName = startDirectory;

        builder.AppendLine(rootName);
        BuildTree(builder, startDirectory, curDepth: 1, maxDepth, prefix: string.Empty);

        return builder.ToString();
    }

    private void BuildTree(
        StringBuilder builder,
        string directory,
        int curDepth,
        int maxDepth,
        string prefix)
    {
        if (curDepth > maxDepth) return;

        string[] subDirectories;
        string[] files;

        try
        {
            subDirectories = FileSystem.GetDirectories(directory).ToArray();
            files = FileSystem.GetFiles(directory).ToArray();
        }
        catch (IOException ex)
        {
            builder.AppendLine(prefix + Options.Branch + $"<io error: {ex.Message}>");
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            builder.AppendLine(prefix + Options.Branch + $"<access denied: {ex.Message}>");
            return;
        }

        int totalItem = subDirectories.Length + files.Length;
        int index = 0;

        foreach (string dir in subDirectories)
        {
            index++;

            bool isLast = index == totalItem;

            string branchSymbol = isLast ? Options.LastBranch : Options.Branch;
            string nextPrefix = prefix + (isLast ? Options.Indent : Options.VerticalLine);

            string name = Path.GetFileName(dir);
            if (string.IsNullOrWhiteSpace(name)) name = dir;

            builder.AppendLine(prefix + branchSymbol + Options.DirectoryPrefix + $"<{name}>");
            BuildTree(builder, dir, curDepth + 1, maxDepth, nextPrefix);
        }

        foreach (string file in files)
        {
            index++;
            bool isLast = index == totalItem;

            string branchSymbol = isLast ? Options.LastBranch : Options.Branch;

            string name = Path.GetFileName(file);
            if (string.IsNullOrWhiteSpace(name)) name = file;

            builder.AppendLine(prefix + branchSymbol + Options.FilePrefix + $"<{name}>");
        }
    }
}