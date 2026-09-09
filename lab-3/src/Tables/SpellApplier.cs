using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tables;

public class SpellApplier
{
    public void ApplyToTableUnit(IPlayerTable table, int unitIndex, ISpell spell)
    {
        ArgumentNullException.ThrowIfNull(table);
        ArgumentNullException.ThrowIfNull(spell);

        if (unitIndex >= 0 && unitIndex < table.Units.Count)
        {
            IUnit current = table.Units[unitIndex];

            IUnit newUnit = spell.ApplyTo(current);

            table.ReplaceAt(unitIndex, newUnit);
        }
    }
}
