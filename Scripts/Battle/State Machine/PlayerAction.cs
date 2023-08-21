using UnityEngine;

public class PlayerAction : BattleAbstract
{
    public int startingHand = 5;
    public int turn;
    public override void EnterState(BattleController battle)
    {
        battle.RefillMana(0);

        if (turn == 0)
        {
            DeckController.instance.DrawMultiple(startingHand);
        }
        else
        {
            DeckController.instance.DrawCard();
            if (battle.playerCurrentMana < battle.playerMaxMana)
            {
                battle.playerCurrentMana++;
            }
        }

    }
    public override void LogicsUpdate(BattleController battle)
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            battle.SwitchState(battle.playerAttack);
        }
    }
    public override void ExitState(BattleController battle)
    {

    }
}
