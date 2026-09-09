using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory;

public class CommandFactory : ICommandFactory
{
    private IReadOnlyList<ICommandCreateStep> Steps { get; }

    public CommandFactory(
        IEnumerable<ICommandCreateStep> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        Steps = steps.ToList();
    }

    public ICommand Create(ParserResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        string commandName = result.CommandName.ToLowerInvariant();

        foreach (ICommandCreateStep step in Steps)
        {
            if (step.CanHandle(result))
            {
                return step.Create(result);
            }
        }

        return new ErrorCommand($"Unknown command: {commandName}");
    }
}
