namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

public abstract class UnitBase : IUnit
{
    public UnitType Type { get; }

    public int Attack { get; protected set; }

    public int Health { get; protected set; }

    public bool IsAlive => Health > 0;

    protected UnitBase(UnitType type, int attack, int health)
    {
        if (attack < 0) throw new ArgumentOutOfRangeException("attack", "attack cannot be negative");
        if (health < 0) throw new ArgumentOutOfRangeException("health", "health cannot be negative");

        Type = type ?? throw new ArgumentNullException("type");
        Attack = attack;
        Health = health;
    }

    public abstract void AttackTarget(IUnit target);

    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive) return;
        if (damage < 0) throw new ArgumentOutOfRangeException("damage", "damage cannot be negative");

        if (damage >= Health) Health = 0;
        else Health -= damage;
    }

    public void IncreaseAttack(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException("value", "value cannot be negative");

        Attack += value;
    }

    public void IncreaseHealth(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException("value", "value cannot be negative");

        Health += value;
    }

    public void SwapAttackAndHealth()
    {
        (Attack, Health) = (Health, Attack);
    }
}