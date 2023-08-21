using UnityEngine;

public class PlayerAttack : BattleAbstract
{
    public override void EnterState(BattleController battle)
    {
        CardPointsController.instance.PlayerAttack();
    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {

    }
}
