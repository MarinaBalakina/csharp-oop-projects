using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;

public sealed class CombatAnalyst : UnitBase
{
    public CombatAnalyst(UnitType type, int attack, int health) : base(type, attack, health) { }

    public override void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (!IsAlive || Attack <= 0) return;

        IncreaseAttack(2);

        target.TakeDamage(Attack);
    }
}