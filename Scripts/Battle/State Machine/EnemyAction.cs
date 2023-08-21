using UnityEngine;

public class EnemyAction : BattleAbstract
{
    public override void EnterState(BattleController battle)
    {

        battle.RefillMana(1);

        //if (battle.playerAction.turn == 0)
        //{
        //    DeckController.instance.DrawMultiple(battle.playerAction.startingHand);
        //}
        //else
        //{
        //    DeckController.instance.DrawCard();
        if (battle.enemyCurrentMana < battle.enemyMaxMana)
        {
            battle.enemyCurrentMana++;
        }
        //}

        Debug.Log("Skip Enemy Action");
        battle.SwitchState(battle.enemyAttack);
    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {

    }
}
