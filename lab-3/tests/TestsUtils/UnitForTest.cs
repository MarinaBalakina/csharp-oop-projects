using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;

public sealed class UnitForTest : UnitBase
{
    public UnitForTest(string name, int attack, int health) : base(
        new UnitType(name, description: string.Empty, baseAttack: attack, baseHealth: health),
        attack,
        health)
    { }

    public override void AttackTarget(IUnit target)
    {
        if (target == null || !IsAlive || Attack <= 0) return;

        target.TakeDamage(Attack);
    }
}