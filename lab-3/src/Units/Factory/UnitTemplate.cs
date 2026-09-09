using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;

public sealed class UnitTemplate
{
    public Func<IUnit> CreateBaseUnit { get; }

    public bool WithShield { get; }

    public bool WithSkills { get; }

    public UnitTemplate(Func<IUnit> baseUnit, bool withShield = false, bool withSkills = false)
    {
        CreateBaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
        WithShield = withShield;
        WithSkills = withSkills;
    }

    public IUnit CreateTableUnit()
    {
        IUnit unit = CreateBaseUnit();

        if (WithShield) unit = ModifierApplier.ApplyShield(unit);
        if (WithSkills) unit = ModifierApplier.ApplyAttackSkills(unit);

        return unit;
    }

    public UnitTemplate WithShieldMod() => new(CreateBaseUnit, withShield: true, WithSkills);

    public UnitTemplate WithSkillsMod() => new(CreateBaseUnit, WithShield, withSkills: true);
}