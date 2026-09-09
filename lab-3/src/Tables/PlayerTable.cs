using Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tables;

public class PlayerTable : IPlayerTable
{
    private const int MaxUnitCount = 7;

    private List<IUnit> UitsList { get; }

    public PlayerTable(IEnumerable<IUnit> units)
    {
        ArgumentNullException.ThrowIfNull(units);

        UitsList = new List<IUnit>(units);

        if (UitsList.Count > MaxUnitCount)
        {
            throw new ArgumentOutOfRangeException("units", $"Cannot create a table with more than {MaxUnitCount} units.");
        }
    }

    public IReadOnlyList<IUnit> Units => UitsList;

    public void AddUnit(IUnit unit)
    {
        ArgumentNullException.ThrowIfNull(unit);

        UitsList.Add(unit);
    }

    public IUnit? PickAttacker(IRandom random)
    {
        ArgumentNullException.ThrowIfNull(random);

        var candidates = UitsList.Where(static u => u.IsAlive && u.Attack > 0).ToList();

        if (candidates.Count == 0) return null;

        int index = random.PickIndex(candidates.Count);

        return candidates[index];
    }

    public IUnit? PickAttacked(IRandom random)
    {
        ArgumentNullException.ThrowIfNull(random);

        var candidates = UitsList.Where(static u => u.IsAlive).ToList();

        if (candidates.Count == 0) return null;

        int index = random.PickIndex(candidates.Count);

        return candidates[index];
    }

    public void ReplaceAt(int unitIndex, IUnit newUnit)
    {
        ArgumentNullException.ThrowIfNull(newUnit);

        if (unitIndex >= UitsList.Count) throw new ArgumentOutOfRangeException("unitIndex", "Index is out of range");

        UitsList[unitIndex] = newUnit;
    }
}
