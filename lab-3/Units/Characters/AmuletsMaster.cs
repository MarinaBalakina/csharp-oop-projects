using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;

public sealed class AmuletsMaster : UnitBase
{
    public AmuletsMaster(UnitType type, int attack, int health) : base(type, attack, health) { }

    public override void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (!IsAlive || Attack <= 0) return;

        target.TakeDamage(Attack);
    }
}
