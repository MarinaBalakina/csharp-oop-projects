using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Itmo.ObjectOrientedProgramming.Lab3.Tables;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public class GameFacade
{
    private IPlayerTable FirstTable { get; }

    private IPlayerTable SecondTable { get; }

    private IBattle Battle { get; }

    private SpellApplier Spells { get; }

    public GameFacade(
        IPlayerTable firstTable,
        IPlayerTable secondTable,
        IBattle battle)
    {
        ArgumentNullException.ThrowIfNull(firstTable);
        ArgumentNullException.ThrowIfNull(secondTable);
        ArgumentNullException.ThrowIfNull(battle);

        FirstTable = firstTable;
        SecondTable = secondTable;
        Battle = battle;
        Spells = new SpellApplier();
    }

    public void AddToFirst(IUnit unit) => FirstTable.AddUnit(unit);

    public void AddToSecond(IUnit unit) => SecondTable.AddUnit(unit);

    public void ApplySpellToFirst(int index, ISpell spell) => Spells.ApplyToTableUnit(FirstTable, index, spell);

    public void ApplySpellToSecond(int index, ISpell spell) => Spells.ApplyToTableUnit(SecondTable, index, spell);

    public BattleResult StartBattle() => Battle.Start(FirstTable, SecondTable);
}