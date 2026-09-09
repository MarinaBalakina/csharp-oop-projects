using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Parsing;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.Factory;

public interface ICommandCreateStep
{
    bool CanHandle(ParserResult result);

    ICommand Create(ParserResult result);
}