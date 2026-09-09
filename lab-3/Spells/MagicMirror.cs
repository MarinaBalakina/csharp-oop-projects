using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class MagicMirror : ISpell
{
    public IUnit ApplyTo(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        unit.SwapAttackAndHealth();

        return unit;
    }
}