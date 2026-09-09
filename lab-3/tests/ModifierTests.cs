using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ModifierTests
{
    [Fact]
    public void MagicShield_FirstHitAbsorbed_SecondHitPasses()
    {
        var target = new MagicShield(new UnitForTest("target", attack: 1, health: 10));
        var attacker = new UnitForTest("attacker", attack: 6, health: 10);

        attacker.AttackTarget(target);

        Assert.True(target.IsAlive);
        Assert.Equal(10, target.Health);

        attacker.AttackTarget(target);
        Assert.Equal(4, target.Health);
    }

    [Fact]
    public void MagicShield_TwoShields_AbsorbTwoHits()
    {
        var target = new MagicShield(new MagicShield(new UnitForTest("target", attack: 0, health: 10)));
        var attacker = new UnitForTest("attacker", attack: 6, health: 10);

        attacker.AttackTarget(target);
        attacker.AttackTarget(target);
        attacker.AttackTarget(target);

        Assert.Equal(4, target.Health);
    }

    [Fact]
    public void AttackSkill_SecondHitOnlyIfTargetAliveAfterFirst()
    {
        var target1 = new UnitForTest("target1", attack: 1, health: 12);
        var attacker = new AttackSkill(new UnitForTest("attacker", attack: 4, health: 10));

        attacker.AttackTarget(target1);
        Assert.Equal(4, target1.Health);

        var target2 = new UnitForTest("target2", attack: 2, health: 4);

        attacker.AttackTarget(target2);
        Assert.False(target2.IsAlive);
        Assert.Equal(0, target2.Health);
    }
}