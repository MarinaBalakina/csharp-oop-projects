using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;

public sealed class MimicChest : UnitBase
{
    public MimicChest(UnitType type, int attack, int health) : base(type, attack, health) { }

    public override void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        if (!IsAlive || Attack <= 0 || !target.IsAlive) return;

        if (target.Attack > Attack) Attack = target.Attack;
        if (target.Health > Health) Health = target.Health;

        target.TakeDamage(Attack);
    }
}