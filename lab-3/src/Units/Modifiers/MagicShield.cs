using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

public sealed class MagicShield : IUnit
{
    public IUnit InnerUnit { get; }

    public bool IsActive { get; private set; }

    public MagicShield(IUnit innerUnit)
    {
        ArgumentNullException.ThrowIfNull(innerUnit);

        InnerUnit = innerUnit;

        IsActive = true;
    }

    public UnitType Type => InnerUnit.Type;

    public int Attack => InnerUnit.Attack;

    public int Health => InnerUnit.Health;

    public bool IsAlive => InnerUnit.IsAlive;

    public void IncreaseAttack(int value) => InnerUnit.IncreaseAttack(value);

    public void IncreaseHealth(int value) => InnerUnit.IncreaseHealth(value);

    public void SwapAttackAndHealth() => InnerUnit.SwapAttackAndHealth();

    public void AttackTarget(IUnit target)
    {
        ArgumentNullException.ThrowIfNull(target);
        InnerUnit.AttackTarget(target);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException("damage", "Damage cannot be less than 0");

        if (!IsAlive) return;

        if (IsActive)
        {
            IsActive = false;
            return;
        }

        InnerUnit.TakeDamage(damage);
    }
}