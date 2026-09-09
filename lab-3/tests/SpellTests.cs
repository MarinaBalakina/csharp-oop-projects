using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Itmo.ObjectOrientedProgramming.Lab3.Tables;
using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class SpellTests
{
    [Fact]
    public void PowerPotion_Adds5Attack_PersistsOnTable()
    {
        var unit = new UnitForTest("unit", attack: 5, health: 5);
        var table = new PlayerTable(new[] { unit });
        var applier = new SpellApplier();

        applier.ApplyToTableUnit(table, 0, spell: new PowerPotion());

        Assert.Equal(unit.Attack, table.Units[0].Attack);
        Assert.Equal(5 + 5, table.Units[0].Attack);
    }

    [Fact]
    public void EndurancePotion_Adds5Health_PersistsOnTable()
    {
        var unit = new UnitForTest("unit", attack: 5, health: 6);
        var table = new PlayerTable(new[] { unit });
        var applier = new SpellApplier();

        applier.ApplyToTableUnit(table, 0, spell: new EndurancePotion());

        Assert.Equal(unit.Health, table.Units[0].Health);
        Assert.Equal(6 + 5, table.Units[0].Health);
    }

    [Fact]
    public void ProtectionAmulet_AddMagicShield()
    {
        var unit = new UnitForTest("unit", attack: 5, health: 5);
        var table = new PlayerTable(new[] { unit });
        var applier = new SpellApplier();

        applier.ApplyToTableUnit(table, 0, spell: new ProtectionAmulet());

        var attacker = new UnitForTest("attacker", attack: 7, health: 5);

        attacker.AttackTarget(table.Units[0]);
        Assert.True(unit.IsAlive);
    }

    [Fact]
    public void MagicMirror_SwapAttackAndHealth()
    {
        var unit = new UnitForTest("unit", attack: 3, health: 7);
        var table = new PlayerTable(new[] { unit });
        var applier = new SpellApplier();

        applier.ApplyToTableUnit(table, 0, spell: new MagicMirror());

        Assert.Equal(3, table.Units[0].Health);
        Assert.Equal(7, table.Units[0].Attack);
    }
}