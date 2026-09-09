using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.BattleCopies;

public interface IBattleUnitFactory
{
    IUnit CreateCopyFromTable(IUnit tableUnit);

    IReadOnlyCollection<IUnit> CreateTeam(IEnumerable<IUnit> team);
}