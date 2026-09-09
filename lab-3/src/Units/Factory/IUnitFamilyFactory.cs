namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Factory;

public interface IUnitFamilyFactory
{
    UnitTemplate CreateCombatAnalyst();

    UnitTemplate CreateEvilFighter();

    UnitTemplate CreateMimicChest();

    UnitTemplate CreateInfiniteHorror();

    UnitTemplate CreateAmuletsMaster();
}