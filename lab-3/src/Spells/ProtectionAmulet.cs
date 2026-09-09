using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class ProtectionAmulet : ISpell
{
    public IUnit ApplyTo(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        return ModifierApplier.ApplyShield(unit);
    }
}
