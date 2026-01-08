/*
[System.Flags]
public enum AIFlags
{
    None                = 0,
    CheckBadMove        = 1 << 0,
    TryToFaint          = 1 << 1,
    CheckViability      = 1 << 2,
    SetupFirstTurn      = 1 << 3,
    Risky               = 1 << 4,
    PreferStrongestMove = 1 << 5,
    PreferBatonPass    = 1 << 6,
    DoubleBattle        = 1 << 7,
    HpAware             = 1 << 8,
    Roaming             = 1 << 29,
    Safari              = 1 << 30,
    FirstBattle         = 1 << 31
}

public class AILogicContext
{
    public AIFlags Flags;

    public int[] MoveScores = new int[4];
    public int ChosenTarget;
    public bool ShouldSwitch;
    public int SwitchToPartyIndex;

    public Pokemon Battler;
    public List<Pokemon> Opponents;
    public BattleState BattleState;
}

public static class AIDamageSimulator
{
    public static int SimulateDamage(
        Pokemon attacker,
        Pokemon defender,
        Move move,
        BattleState state)
    {
        if (move.Power == 0)
            return 0;

        float stab = attacker.Types.Contains(move.Type) ? 1.5f : 1f;
        float effectiveness = TypeChart.GetEffectiveness(move.Type, defender.Types);

        return Mathf.FloorToInt(
            move.Power *
            attacker.Attack / (float)defender.Defense *
            stab *
            effectiveness
        );
    }
}

public interface IAIMoveScorer
{
    void ScoreMove(AILogicContext ctx, Move move, int index);
}

public class PreferStrongestMoveScorer : IAIMoveScorer
{
    public void ScoreMove(AILogicContext ctx, Move move, int index)
    {
        int damage = AIDamageSimulator.SimulateDamage(
            ctx.Battler,
            ctx.Opponents[ctx.ChosenTarget],
            move,
            ctx.BattleState
        );

        ctx.MoveScores[index] += damage / 5;
    }
}

public static class AISwitchEvaluator
{
    public static bool ShouldSwitch(AILogicContext ctx)
    {
        if (ctx.Battler.HPPercent < 0.5f)
            return false;

        int bestScore = ctx.MoveScores.Max();
        return bestScore < 20;
    }
}

public class BattleAIController
{
    private readonly List<IAIMoveScorer> scorers;

    public BattleAIController(AIFlags flags)
    {
        scorers = AIFactory.CreateScorers(flags);
    }

    public BattleAction Decide(AILogicContext ctx)
    {
        for (int i = 0; i < ctx.Battler.Moves.Count; i++)
        {
            foreach (var scorer in scorers)
                scorer.ScoreMove(ctx, ctx.Battler.Moves[i], i);
        }

        if (AISwitchEvaluator.ShouldSwitch(ctx))
            return BattleAction.Switch(ctx.SwitchToPartyIndex);

        int bestMove = Array.IndexOf(ctx.MoveScores, ctx.MoveScores.Max());
        return BattleAction.UseMove(bestMove, ctx.ChosenTarget);
    }
}
*/