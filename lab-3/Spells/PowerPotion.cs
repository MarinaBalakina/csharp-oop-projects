using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class PowerPotion : ISpell
{
    public IUnit ApplyTo(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        unit.IncreaseAttack(5);

        return unit;
    }
}