using UnityEngine;

public class EnemyAttack : BattleAbstract
{
    public override void EnterState(BattleController battle)
    {
        CardPointsController.instance.EnemyAttack();
    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {
        battle.playerAction.turn++;
    }
}
