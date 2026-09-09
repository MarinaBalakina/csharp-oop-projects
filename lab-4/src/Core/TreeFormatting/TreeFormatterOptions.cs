namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

public class TreeFormatterOptions
{
    public string Indent { get; }

    public string DirectoryPrefix { get; }

    public string FilePrefix { get; }

    public string Branch { get; }

    public string LastBranch { get; }

    public string VerticalLine { get; }

    public TreeFormatterOptions(
        string indent,
        string directoryPrefix,
        string filePrefix,
        string branch,
        string lastBranch,
        string verticalLine)
    {
        Indent = indent;
        DirectoryPrefix = directoryPrefix;
        FilePrefix = filePrefix;
        Branch = branch;
        LastBranch = lastBranch;
        VerticalLine = verticalLine;
    }

    public static TreeFormatterOptions Default { get; } =
        new TreeFormatterOptions(
            indent: "    ",
            directoryPrefix: "[D] ",
            filePrefix: "[F] ",
            branch: "├── ",
            lastBranch: "└── ",
            verticalLine: "│   ");
}