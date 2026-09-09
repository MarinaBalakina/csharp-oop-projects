using Itmo.ObjectOrientedProgramming.Lab3.Tables;
using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class TableTests
{
    [Fact]
    public void PickAttacker_ChoosesOnlyAliveWithPositiveAttack()
    {
        var unit1 = new UnitForTest("unit1", attack: 0, health: 5);
        var unit2 = new UnitForTest("unit2", attack: 3, health: 0);
        var unit3 = new UnitForTest("unit3", attack: 3, health: 6);

        var table = new PlayerTable(new[] { unit1, unit2, unit3 });

        var random = new FakeRandom(0);

        IUnit? attacker = table.PickAttacker(random);
        Assert.Same(unit3, attacker);
    }

    [Fact]
    public void LimitedProxy_AddUnit_BlocksEighth()
    {
        var table = new PlayerTable(new IUnit[]
        {
            new UnitForTest("u1", attack: 1, health: 1), new UnitForTest("u2", attack: 1, health: 1),
            new UnitForTest("u1", attack: 1, health: 1), new UnitForTest("u2", attack: 1, health: 1),
            new UnitForTest("u1", attack: 1, health: 1), new UnitForTest("u2", attack: 1, health: 1),
            new UnitForTest("u1", attack: 1, health: 1),
        });

        var proxy = new LimitedTableProxy(table);

        proxy.AddUnit(new UnitForTest("u1", attack: 1, health: 1));

        Assert.True(proxy.Units.Count == 7);
    }
}