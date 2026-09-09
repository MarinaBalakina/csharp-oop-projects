using Itmo.ObjectOrientedProgramming.Lab3.Units.Characters;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;

public sealed class BaseSetFactory : IUnitFamilyFactory
{
    private readonly UnitType _combatAnalyst = new UnitType("Combat Analyst", "+2 к атаке до конца боя", 2, 4);

    private readonly UnitType _evilFighter =
        new UnitType("Evil fighter", "при не смертельном уроне атака удваивается", 1, 6);

    private readonly UnitType _mimicChest =
        new UnitType("Mimic chest", "атака и здоровье становятся как у противника если они больше чем у него", 1, 1);

    private readonly UnitType _infiniteHorror =
        new UnitType("Infinite horror", "перерождается, сохраняя показатель атаки, здоровье = 1", 4, 4);

    private readonly UnitType _amuletsMaster =
        new UnitType("Amulets master", "изначально обладает Магическим щитом и Мастерством атаки", 5, 2);

    public UnitTemplate CreateCombatAnalyst() => new UnitTemplate(() =>
        new CombatAnalyst(_combatAnalyst, _combatAnalyst.BaseAttack, _combatAnalyst.BaseHealth));

    public UnitTemplate CreateEvilFighter() => new UnitTemplate(() =>
        new EvilFighter(_evilFighter, _evilFighter.BaseAttack, _evilFighter.BaseHealth));

    public UnitTemplate CreateMimicChest() =>
        new UnitTemplate(() => new MimicChest(_mimicChest, _mimicChest.BaseAttack, _mimicChest.BaseHealth));

    public UnitTemplate CreateInfiniteHorror() => new UnitTemplate(() =>
        new InfiniteHorror(_infiniteHorror, _infiniteHorror.BaseAttack, _infiniteHorror.BaseHealth));

    public UnitTemplate CreateAmuletsMaster() => new UnitTemplate(() =>
        new AmuletsMaster(_amuletsMaster, _amuletsMaster.BaseAttack, _amuletsMaster.BaseHealth)).WithSkillsMod().WithShieldMod();
}
