using Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class FactoryTests
{
    [Fact]
    public void CreateTableUnit_ReturnsIndependentInstances()
    {
        var catalog = new BaseSetFactory();
        UnitTemplate template = catalog.CreateCombatAnalyst();

        IUnit u1 = template.CreateTableUnit();
        IUnit u2 = template.CreateTableUnit();

        Assert.NotSame(u1, u2);

        u1.IncreaseAttack(5);
        Assert.Equal(2 + 5, u1.Attack);
        Assert.Equal(2, u2.Attack);
    }

    [Fact]
    public void AmuletMaster_FromFactory_HasShieldAndAttackSkillOnTable()
    {
        var catalog = new BaseSetFactory();
        IUnit onTable = catalog.CreateAmuletsMaster().CreateTableUnit();

        AttackSkill skill = Assert.IsType<AttackSkill>(onTable);
        MagicShield shield = Assert.IsType<MagicShield>(skill.InnerUnit);

        Assert.IsType<AmuletsMaster>(shield.InnerUnit);
    }
}