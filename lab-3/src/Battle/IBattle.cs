using Itmo.ObjectOrientedProgramming.Lab3.Tables;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public interface IBattle
{
    BattleResult Start(IPlayerTable firstTeam, IPlayerTable secondTeam);
}
