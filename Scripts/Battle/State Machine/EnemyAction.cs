using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAction : BattleAbstract
{
    public List<CardPlacePoint> availablePoints;
    public List<CardSO> availableCards;
    public override void EnterState(BattleController battle)
    {
        if (EnemyController.instance.activeCards.Count == 0)
        {
            EnemyController.instance.SetUpDeck();
        }

        if (EnemyController.instance.cardsInEnemyHand.Count == 0)
        {
            EnemyController.instance.SetUpHand();
        }

        battle.RefillMana(1);

        if (battle.enemyCurrentMana < battle.enemyMaxMana)
        {
            battle.enemyCurrentMana++;
        }

        AvailablePointsAndCards();

        ///////

        EnemyController.instance.StartEnemyAction();
    }
    public override void LogicsUpdate(BattleController battle)
    {
        BattleController.instance.availablePoints = availablePoints;
        BattleController.instance.availableCards = availableCards;

        BattleController.instance.randomPoint = EnemyController.instance.handRandomAI.randomPoint;
        BattleController.instance.randomCard = EnemyController.instance.handRandomAI.randomCard;

    }
    public override void ExitState(BattleController battle)
    {

    }

    public void AvailablePointsAndCards()
    {
        availablePoints = new List<CardPlacePoint>();
        availablePoints.Clear();

        foreach (CardPlacePoint point in BattleController.instance.enemyCardPoints)
        {
            if (point.enemyCard == null)
            {
                availablePoints.Add(point);
            }
        }

        availableCards = new List<CardSO>();
        availableCards.Clear();

        foreach (CardSO enemyCard in EnemyController.instance.cardsInEnemyHand)
        {
            if (enemyCard.manaCost <= BattleController.instance.enemyCurrentMana)
            {
                availableCards.Add(enemyCard);
            }
        }
    }
}
