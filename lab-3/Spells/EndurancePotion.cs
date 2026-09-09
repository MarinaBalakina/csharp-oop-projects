using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class EndurancePotion : ISpell
{
    public IUnit ApplyTo(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        unit.IncreaseHealth(5);

        return unit;
    }
}
