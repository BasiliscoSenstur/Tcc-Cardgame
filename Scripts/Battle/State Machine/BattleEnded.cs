using System.Collections;
using UnityEngine;

public class BattleEnded : BattleAbstract
{
    bool showEndScreen;

    public override void EnterState(BattleController battle)
    {
        Debug.Log("Battle Ended!");

        if (HandController.instance.heldCards.Count > 0)
        {
            foreach(Card card in HandController.instance.heldCards)
            {
                card.inHand = false;
                card.MoveCardToPosition(battle.discardPile.position, battle.discardPile.rotation);
            }
        }

        ShowEndBattleScreen();
    }

    public override void LogicsUpdate(BattleController battle)
    {
        if (showEndScreen == true)
        {
            UIController.instance.endBattleScreen.alpha = Mathf.MoveTowards(UIController.instance.endBattleScreen.alpha, 1f, 2f * Time.deltaTime);
        }
    }

    public override void ExitState(BattleController battle)
    {
        
    }

    void ShowEndBattleScreen()
    {
        UIController.instance.StartCoroutine(EndBattleScreenCo());
    }

    public IEnumerator EndBattleScreenCo()
    {
        yield return new WaitForSeconds(0.54f);
        UIController.instance.endBattleScreen.gameObject.SetActive(true);

        if (BattleController.instance.playerCurrentLP <= 0)
        {
            UIController.instance.battleResult.text = "You Lost!";
        }
        else
        {
            UIController.instance.battleResult.text = "You Won!";

            if (AudioManager.instance.musicPlaying != null)
            {
                AudioManager.instance.musicPlaying.Stop();
                Debug.Log("Teste");
            }
            //AudioManager.instance.PlayMusic(3);
        }

        yield return new WaitForSeconds(0.54f);
        showEndScreen = true;

        yield return new WaitForSeconds(0.54f);
        Debug.Log("End");
    }
}
