using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.ConnectionActions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory.ConnectCommandStep;

public class DisconnectStep : ICommandCreateStep
{
    public bool CanHandle(ParserResult result)
    {
        return string.Equals("disconnect", result.CommandName, StringComparison.OrdinalIgnoreCase);
    }

    public ICommand Create(ParserResult result)
    {
        return new DisconnectCommand();
    }
}