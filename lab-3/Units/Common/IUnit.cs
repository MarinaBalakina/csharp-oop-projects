namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

public interface IUnit
{
    UnitType Type { get; }

    int Attack { get; }

    int Health { get; }

    bool IsAlive { get; }

    void AttackTarget(IUnit target);

    void TakeDamage(int damage);

    void IncreaseAttack(int value);

    void IncreaseHealth(int value);

    void SwapAttackAndHealth();
}
