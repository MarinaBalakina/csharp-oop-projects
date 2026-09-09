using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class CharactersTests
{
    [Fact]
    public void CombatAnalyst_IncreasesAttackBy2()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate tmpl = catalog.CreateCombatAnalyst();

        IUnit analyst = tmpl.CreateTableUnit();

        var target = new UnitForTest("target", attack: 1, health: 5);

        analyst.AttackTarget(target);

        Assert.Equal(1, target.Health);
    }

    [Fact]
    public void EvilFighter_NonLethalDamage_DoublesAttack()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate tmpl = catalog.CreateEvilFighter();

        IUnit fighter = tmpl.CreateTableUnit();

        var attacker = new UnitForTest("attacker", attack: 1, health: 5);

        attacker.AttackTarget(fighter);
        Assert.True(fighter.IsAlive);
        Assert.Equal(2, fighter.Attack);

        attacker.AttackTarget(fighter);
        Assert.True(fighter.IsAlive);
        Assert.Equal(4, fighter.Attack);
    }

    [Fact]
    public void MimicChest_BeforeAttack_CopiesMaxOfStats()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate tmpl = catalog.CreateMimicChest();

        IUnit mimic = tmpl.CreateTableUnit();

        var target1 = new UnitForTest("target1", attack: 7, health: 6);
        mimic.AttackTarget(target1);

        Assert.False(target1.IsAlive);
        Assert.Equal(7, mimic.Attack);
        Assert.Equal(6, mimic.Health);

        var target2 = new UnitForTest("target2", attack: 4, health: 8);
        mimic.AttackTarget(target2);

        Assert.Equal(8, mimic.Health);
        Assert.Equal(7, mimic.Attack);
        Assert.Equal(1, target2.Health);
    }

    [Fact]
    public void InfiniteHorror_FirstLethalDamage_RevivesWith1Hp_SecondKills()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate tmpl = catalog.CreateInfiniteHorror();

        IUnit horror = tmpl.CreateTableUnit();
        var attacker = new UnitForTest("attacker", attack: 4, health: 7);

        attacker.AttackTarget(horror);
        Assert.True(horror.IsAlive);
        Assert.Equal(1, horror.Health);

        attacker.AttackTarget(horror);
        Assert.False(horror.IsAlive);
    }

    [Fact]
    public void AmuletsMaster()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate tmpl = catalog.CreateAmuletsMaster();

        IUnit master = tmpl.CreateTableUnit();
        var enemy = new UnitForTest("attacker", attack: 10, health: 12);

        enemy.AttackTarget(master);
        Assert.True(master.IsAlive);

        master.AttackTarget(enemy);
        Assert.Equal(2, enemy.Health);
    }
}