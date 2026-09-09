using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

public sealed class AttackSkill : IUnit
{
    public IUnit InnerUnit { get; }

    public AttackSkill(IUnit innerUnit)
    {
        ArgumentNullException.ThrowIfNull(innerUnit);

        InnerUnit = innerUnit;
    }

    public UnitType Type => InnerUnit.Type;

    public int Attack => InnerUnit.Attack;

    public int Health => InnerUnit.Health;

    public bool IsAlive => InnerUnit.IsAlive;

    public void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);

        InnerUnit.AttackTarget(target);

        if (IsAlive && target.IsAlive)
        {
            InnerUnit.AttackTarget(target);
        }
    }

    public void TakeDamage(int damage) => InnerUnit.TakeDamage(damage);

    public void IncreaseAttack(int value) => InnerUnit.IncreaseAttack(value);

    public void IncreaseHealth(int value) => InnerUnit.IncreaseHealth(value);

    public void SwapAttackAndHealth() => InnerUnit.SwapAttackAndHealth();
}
