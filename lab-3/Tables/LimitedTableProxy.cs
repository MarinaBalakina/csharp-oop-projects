using Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tables;

public sealed class LimitedTableProxy : IPlayerTable
{
    private IPlayerTable Inner { get; }

    private const int MaxUnitCount = 7;

    public IReadOnlyList<IUnit> Units => Inner.Units;

    public LimitedTableProxy(IPlayerTable inner)
    {
        Inner = inner;
    }

    public void AddUnit(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);
        if (Inner.Units.Count >= MaxUnitCount) return;

        Inner.AddUnit(unit);
    }

    public IUnit? PickAttacked(IRandom random) => Inner.PickAttacked(random);

    public IUnit? PickAttacker(IRandom random) => Inner.PickAttacker(random);

    public void ReplaceAt(int unitIndex, IUnit newUnit) => Inner.ReplaceAt(unitIndex, newUnit);
}