using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.TreeActions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Paths;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeFormatting;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.TreeCommandStep;

public class TreeListStep : ICommandCreateStep
{
    private IPathService PathService { get; }

    private ITreeFormatter Formatter { get; }

    public TreeListStep(IPathService pathService, ITreeFormatter formatter)
    {
        PathService = pathService ?? throw new ArgumentNullException(nameof(pathService));
        Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    public bool CanHandle(ParserResult result)
    {
        if (!string.Equals("tree", result.CommandName, StringComparison.OrdinalIgnoreCase)) return false;

        if (result.Arguments.Count == 0) return false;

        string subCommandName = result.Arguments[0];
        return string.Equals("list", subCommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ICommand Create(ParserResult result)
    {
        int depth = 1;
        if (result.Flags.TryGetValue("d", out string? depthValue))
        {
            if (!int.TryParse(depthValue, out depth) || depth < 1)
                return new ErrorCommand($"Invalid depth: {depth}");
        }

        return new TreeListCommand(depth, PathService, Formatter);
    }
}