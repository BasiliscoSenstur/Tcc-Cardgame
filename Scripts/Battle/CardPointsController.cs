using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPointsController : MonoBehaviour
{
    public static CardPointsController instance;
    private void Awake()
    {
        instance = this;
    }
    // ------------------------------------ //

    public CardPlacePoint[] playerPoints, enemyPoints;
    public float timeBetweenAttacks = 0.54f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCo());
    }

    IEnumerator PlayerAttackCo()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for (int i = 0; i < playerPoints.Length; i++)
        {
            if (playerPoints[i].playerCard != null)
            {
                if (enemyPoints[i].enemyCard != null && BattleController.instance.currentState != BattleController.instance.battleEnded)
                {
                    playerPoints[i].playerCard.ChangeAnimation("Card_Attack");
                    enemyPoints[i].enemyCard.enemyCardHealth -= playerPoints[i].playerCard.data.attack;
                    enemyPoints[i].enemyCard.ChangeAnimation("Card_Hurt");
                    AudioManager.instance.PlaySoundFx(2);

                    if (enemyPoints[i].enemyCard.enemyCardHealth <= 0)
                    {
                        enemyPoints[i].enemyCard.ChangeAnimation("Card_Jump");

                        yield return new WaitForSeconds(0.12f);

                        AudioManager.instance.PlaySoundFx(1);
                        enemyPoints[i].enemyCard.MoveCardToPosition(BattleController.instance.discardPile.position, BattleController.instance.discardPile.rotation);
                        Destroy(enemyPoints[i].enemyCard.gameObject, 1.33f);
                        enemyPoints[i].enemyCard = null;
                    }
                    else
                    {
                        enemyPoints[i].enemyCard.ChangeAnimation("Card_Idle");
                    }

                    Debug.Log("Attack " + (i + 1));
                }
                else
                {
                    //Direct Attack
                    if (BattleController.instance.currentState != BattleController.instance.battleEnded)
                    {
                        Debug.Log("Direct Attack " + (i + 1));
                        playerPoints[i].playerCard.ChangeAnimation("Card_Attack");
                        AudioManager.instance.PlaySoundFx(0);
                        BattleController.instance.DemageLifePoints(1, playerPoints[i].playerCard.data.attack);
                    }
                }
                yield return new WaitForSeconds(timeBetweenAttacks);
                playerPoints[i].playerCard.ChangeAnimation("Card_Idle");
            }
        }
        if(BattleController.instance.currentState != BattleController.instance.battleEnded)
        {
            BattleController.instance.SwitchState(BattleController.instance.enemyAction);
        }
    }


    public void EnemyAttack()
    {
        StartCoroutine(EnemyAttackCo());
    }

    IEnumerator EnemyAttackCo()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for (int i = 0; i < enemyPoints.Length; i++)
        {
            if (enemyPoints[i].enemyCard != null)
            {
                if (playerPoints[i].playerCard != null)
                {
                    //Attack
                    enemyPoints[i].enemyCard.ChangeAnimation("Card_Attack");
                    AudioManager.instance.PlaySoundFx(2);
                    playerPoints[i].playerCard.playerCardHealth -= enemyPoints[i].enemyCard.data.attack;
                    playerPoints[i].playerCard.ChangeAnimation("Card_Hurt");

                    //Destruição da carta do player 
                    if (playerPoints[i].playerCard.playerCardHealth <= 0)
                    {
                        playerPoints[i].playerCard.ChangeAnimation("Card_Jump");

                        yield return new WaitForSeconds(0.12f);

                        AudioManager.instance.PlaySoundFx(1);
                        playerPoints[i].playerCard.MoveCardToPosition(BattleController.instance.discardPile.position, BattleController.instance.discardPile.rotation);
                        Destroy(playerPoints[i].playerCard.gameObject, 1.33f);
                        playerPoints[i].playerCard = null;
                    }
                    else
                    {
                        playerPoints[i].playerCard.ChangeAnimation("Card_Idle");
                    }

                    Debug.Log("Attack " + (i + 1));
                }
                else
                {
                    //Direct Attack
                    if (BattleController.instance.currentState != BattleController.instance.battleEnded)
                    {
                        Debug.Log("Direct Attack " + (i + 1));
                        enemyPoints[i].enemyCard.ChangeAnimation("Card_Attack");
                        AudioManager.instance.PlaySoundFx(0);
                        BattleController.instance.DemageLifePoints(0, enemyPoints[i].enemyCard.data.attack);
                    }
                }
                yield return new WaitForSeconds(timeBetweenAttacks);
                enemyPoints[i].enemyCard.ChangeAnimation("Card_Idle");
            }
        }
        if (BattleController.instance.currentState != BattleController.instance.battleEnded)
        {
            BattleController.instance.SwitchState(BattleController.instance.playerAction);
        }
    }
}
