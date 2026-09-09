namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Actions.CommandPattern;

public interface ICommand
{
    CommandResult Execute(CommandEnvironment environment);
}