using Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.BattleCopies;

public sealed class BattleUnitFactory : IBattleUnitFactory
{
    public IUnit CreateCopyFromTable(IUnit tableUnit)
    {
        ArgumentNullException.ThrowIfNull(tableUnit);

        bool hasMagicShield = false;
        bool hasAttackSkill = false;

        IUnit inner = tableUnit;

        while (true)
        {
            if (inner is AttackSkill attackSkill)
            {
                hasAttackSkill = true;
                inner = attackSkill.InnerUnit;
                continue;
            }

            if (inner is MagicShield magicShield)
            {
                hasMagicShield = true;
                inner = magicShield.InnerUnit;
                continue;
            }

            break;
        }

        IUnit battleCopy = inner switch
        {
            CombatAnalyst u => new(u.Type, u.Attack, u.Health),
            EvilFighter u => new(u.Type, u.Attack, u.Health),
            MimicChest u => new(u.Type, u.Attack, u.Health),
            InfiniteHorror u => new(u.Type, u.Attack, u.Health),
            AmuletsMaster u => new AmuletsMaster(u.Type, u.Attack, u.Health),
            _ => throw new NotSupportedException($"Unknown battle unit type: {inner.GetType().Name}"),
        };

        if (hasAttackSkill) battleCopy = ModifierApplier.ApplyAttackSkills(battleCopy);
        if (hasMagicShield) battleCopy = ModifierApplier.ApplyShield(battleCopy);

        return battleCopy;
    }

    public IReadOnlyCollection<IUnit> CreateTeam(IEnumerable<IUnit> team)
    {
        ArgumentNullException.ThrowIfNull(team);

        var result = new List<IUnit>();
        foreach (IUnit unit in team)
        {
            result.Add(CreateCopyFromTable(unit));
        }

        return result;
    }
}
