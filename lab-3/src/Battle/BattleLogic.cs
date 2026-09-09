using Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;
using Itmo.ObjectOrientedProgramming.Lab3.Tables;
using Itmo.ObjectOrientedProgramming.Lab3.Units.BattleCopies;
using Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public sealed class BattleLogic : IBattle
{
    private List<IUnit> FirstTable { get; set; } = new List<IUnit>();

    private List<IUnit> SecondTable { get; set; } = new List<IUnit>();

    private IRandom Random { get; }

    private int MaxRounds { get; }

    private IBattleUnitFactory UnitFactory { get; }

    public BattleLogic(IBattleUnitFactory unitFactory, IRandom random, int maxRounds)
    {
        ArgumentNullException.ThrowIfNull(unitFactory);
        ArgumentNullException.ThrowIfNull(random);

        MaxRounds = maxRounds > 0 && maxRounds <= 200 ? maxRounds : 200;

        Random = random;
        UnitFactory = unitFactory;
    }

    public BattleResult Start(IPlayerTable firstTeam, IPlayerTable secondTeam)
    {
        ArgumentNullException.ThrowIfNull(firstTeam);
        ArgumentNullException.ThrowIfNull(secondTeam);

        CreateTeamsCopy(firstTeam, secondTeam);

        bool firstTurn = true;
        if (firstTeam.Units.Count > secondTeam.Units.Count)
        {
            firstTurn = false;
        }
        else if (firstTeam.Units.Count == secondTeam.Units.Count)
        {
            firstTurn = Random.PickIndex(2) == 0;
        }

        for (int curRound = 0; curRound < MaxRounds; curRound++)
        {
            List<IUnit> attackTeam = firstTurn ? FirstTable : SecondTable;
            List<IUnit> defTeam = firstTurn ? SecondTable : FirstTable;

            if (!HasAnyAlive(FirstTable) && !HasAnyAlive(SecondTable)) return BattleResult.NobodyWon;
            if (!HasAliveAttacker(FirstTable) && !HasAliveAttacker(SecondTable)) return BattleResult.NobodyWon;

            IUnit? attacker = PickAttacker(attackTeam);
            IUnit? target = PickTarget(defTeam);

            if (attacker != null && target == null)
            {
                return firstTurn ? BattleResult.FirstPlayerWon : BattleResult.SecondPlayerWon;
            }

            if (attacker == null && target == null) return BattleResult.NobodyWon;

            if (attacker == null)
            {
                firstTurn = !firstTurn;
                continue;
            }

            if (target == null)
                return firstTurn ? BattleResult.FirstPlayerWon : BattleResult.SecondPlayerWon;

            DoAttack(attacker, target);

            if (!HasAnyAlive(defTeam))
            {
                return firstTurn ? BattleResult.FirstPlayerWon : BattleResult.SecondPlayerWon;
            }

            firstTurn = !firstTurn;
        }

        return BattleResult.NobodyWon;
    }

    private void CreateTeamsCopy(IPlayerTable first, IPlayerTable second)
    {
        FirstTable = UnitFactory.CreateTeam(first.Units).ToList();
        SecondTable = UnitFactory.CreateTeam(second.Units).ToList();
    }

    private IUnit? PickAttacker(List<IUnit> team)
    {
        var candidates = team.Where(static u => u.IsAlive && u.Attack > 0).ToList();

        if (candidates.Count == 0) return null;

        int index = Random.PickIndex(candidates.Count);

        return candidates[index];
    }

    private IUnit? PickTarget(List<IUnit> team)
    {
        var candidates = team.Where(static u => u.IsAlive).ToList();

        if (candidates.Count == 0) return null;

        int index = Random.PickIndex(candidates.Count);

        return candidates[index];
    }

    private void DoAttack(IUnit attacker, IUnit target)
    {
        attacker.AttackTarget(target);
    }

    private bool HasAnyAlive(List<IUnit> team) => team.Any(static t => t.IsAlive);

    private bool HasAliveAttacker(List<IUnit> attackers) =>
        attackers.Any(static attacker => attacker.IsAlive && attacker.Attack > 0);
}
