using Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tables;

public interface IPlayerTable
{
    IReadOnlyList<IUnit> Units { get; }

    void AddUnit(IUnit unit);

    IUnit? PickAttacker(IRandom random);

    IUnit? PickAttacked(IRandom random);

    void ReplaceAt(int unitIndex, IUnit newUnit);
}
