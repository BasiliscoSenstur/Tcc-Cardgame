using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : BattleAbstract
{
    public override void EnterState(BattleController battle)
    {
        if (EnemyController.instance.activeCards.Count == 0)
        {
            EnemyController.instance.SetUpDeck();
        }

        battle.RefillMana(1);

        if (battle.enemyCurrentMana < battle.enemyMaxMana)
        {
            battle.enemyCurrentMana += 27;
        }

        EnemyController.instance.StartEnemyAction();
    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {

    }
}
