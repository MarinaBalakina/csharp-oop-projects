using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;

public sealed class EvilFighter : UnitBase
{
    public EvilFighter(UnitType type, int attack, int health) : base(type, attack, health) { }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsAlive) IncreaseAttack(Attack);
    }

    public override void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (!IsAlive || Attack <= 0) return;
        target.TakeDamage(Attack);
    }
}