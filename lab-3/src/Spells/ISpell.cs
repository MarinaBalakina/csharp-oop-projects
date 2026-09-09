using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public interface ISpell
{
    IUnit ApplyTo(IUnit unit);
}
