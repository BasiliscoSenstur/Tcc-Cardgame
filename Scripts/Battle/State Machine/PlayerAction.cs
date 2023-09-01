using UnityEngine;

public class PlayerAction : BattleAbstract
{
    public int startingHand = 5;
    public int turn;
    public override void EnterState(BattleController battle)
    {
        if(DeckController.instance.activeCards.Count == 0)
        {
            DeckController.instance.SetUpDeck();
        }

        battle.RefillMana(0);

        BattleController.instance.drawManaCost = 2;
        UIController.instance.manaDrawButton.SetActive(true);
        UIController.instance.endTurnButton.SetActive(true);

        if (turn == 0)
        {
            DeckController.instance.DrawMultiple(startingHand);
            AudioManager.instance.PlaySoundFx(3);
        }
        else
        {
            DeckController.instance.DrawCard();
            AudioManager.instance.PlaySoundFx(3);
            if (battle.playerCurrentMana < battle.playerMaxMana)
            {
                battle.playerCurrentMana++;
            }
        }

    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {
        UIController.instance.manaDrawButton.SetActive(false);
        UIController.instance.endTurnButton.SetActive(false);
    }
}
