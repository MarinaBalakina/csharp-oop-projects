using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

public static class ModifierApplier
{
    public static IUnit ApplyShield(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);
        return new MagicShield(unit);
    }

    public static IUnit ApplyAttackSkills(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);
        return new AttackSkill(unit);
    }
}