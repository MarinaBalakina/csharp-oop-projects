using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class UnitBaseTests
{
    [Fact]
    public void TakeDamade_Kill()
    {
        var unit = new UnitForTest("cookie", attack: 0, health: 6);

        unit.TakeDamage(6);

        Assert.False(unit.IsAlive);
        Assert.Equal(0, unit.Health);
    }

    [Fact]
    public void AttackTarget_AttackerNoBackDamage()
    {
        var attacker = new UnitForTest("milk", attack: 3, health: 10);
        var target = new UnitForTest("cookie", attack: 1, health: 8);

        attacker.AttackTarget(target);

        Assert.Equal(8 - 3, target.Health);
        Assert.Equal(10, attacker.Health);
    }

    [Fact]
    public void SwapAttackAndHealth()
    {
        var unit = new UnitForTest("cookie", attack: 3, health: 10);

        unit.SwapAttackAndHealth();

        Assert.Equal(10, unit.Attack);
        Assert.Equal(3, unit.Health);
    }
}