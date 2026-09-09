using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;

public sealed class InfiniteHorror : UnitBase
{
    private bool Resurrected { get; set; }

    public InfiniteHorror(UnitType type, int attack, int health) : base(type, attack, health) { Resurrected = false; }

    public override void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        base.TakeDamage(damage);

        if (!IsAlive && !Resurrected)
        {
            Health = 1;
            Resurrected = true;
        }
    }

    public override void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (!IsAlive || Attack <= 0) return;
        target.TakeDamage(Attack);
    }
}