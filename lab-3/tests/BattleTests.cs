using Itmo.ObjectOrientedProgramming.Lab3.Battle;
using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Itmo.ObjectOrientedProgramming.Lab3.Tables;
using Itmo.ObjectOrientedProgramming.Lab3.Tests.TestsUtils;
using Itmo.ObjectOrientedProgramming.Lab3.Units.BattleCopies;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class BattleTests
{
    [Fact]
    public void AttackerHasUnit_DefenderHasNoTargets_FirstPlayerWinsImmediately()
    {
        var player1 = new PlayerTable(Array.Empty<IUnit>());
        var player2 = new PlayerTable(Array.Empty<IUnit>());

        var random = new FakeRandom(0);
        var unitFactory = new BattleUnitFactory();

        var battle = new BattleLogic(unitFactory, random, maxRounds: 100);
        var game = new GameFacade(player1, player2, battle);

        var catalog = new BaseSetFactory();
        game.AddToFirst(catalog.CreateCombatAnalyst().CreateTableUnit());

        BattleResult battleResult = game.StartBattle();

        Assert.Equal(BattleResult.FirstPlayerWon, battleResult);
    }

    [Fact]
    public void StartBattle_TurnAlternates_FirstEventuallyWins()
    {
        var player1 = new PlayerTable(Array.Empty<IUnit>());
        var player2 = new PlayerTable(Array.Empty<IUnit>());

        var random = new FakeRandom(0);
        var unitFactory = new BattleUnitFactory();

        var battle = new BattleLogic(unitFactory, random, maxRounds: 100);
        var game = new GameFacade(player1, player2, battle);

        var catalog = new BaseSetFactory();
        game.AddToSecond(catalog.CreateCombatAnalyst().CreateTableUnit());
        game.AddToFirst(catalog.CreateAmuletsMaster().CreateTableUnit());

        BattleResult battleResult = game.StartBattle();

        Assert.Equal(BattleResult.FirstPlayerWon, battleResult);
    }

    [Fact]
    public void ApplySpell_ProtectionAmulet_WrapsUnitAndAbsorbsFirstHit()
    {
        var player1 = new PlayerTable(Array.Empty<IUnit>());
        var player2 = new PlayerTable(Array.Empty<IUnit>());

        var random = new FakeRandom(0);
        var unitFactory = new BattleUnitFactory();

        var battle = new BattleLogic(unitFactory, random, maxRounds: 100);
        var game = new GameFacade(player1, player2, battle);

        var catalog = new BaseSetFactory();
        game.AddToFirst(catalog.CreateMimicChest().CreateTableUnit());
        game.AddToSecond(catalog.CreateInfiniteHorror().CreateTableUnit());

        game.ApplySpellToSecond(0, new ProtectionAmulet());

        IUnit target = player2.Units[0];
        MagicShield shield = Assert.IsType<MagicShield>(target);
        Assert.IsType<InfiniteHorror>(shield.InnerUnit);

        IUnit attacker = player1.Units[0];
        int hpBefore = target.Health;

        attacker.AttackTarget(target);
        Assert.Equal(hpBefore, target.Health);

        attacker.AttackTarget(target);
        Assert.True(target.IsAlive);
        Assert.Equal(1, target.Health);
    }
}
