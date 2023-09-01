using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAction : BattleAbstract
{
    //Hand Random
    public List<CardPlacePoint> availablePoints;
    public List<CardSO> availableCards;

    public EnemyCard enemyCard;

    //Offensive AI
    public List<CardPlacePoint> offensivePlacePoints;

    //Deffensive AI
    public List<CardPlacePoint> deffensivePlacePoints;

    public override void EnterState(BattleController battle)
    {
        if (EnemyController.instance.activeCards.Count == 0)
        {
            EnemyController.instance.SetUpDeck();
        }

        if (EnemyController.instance.cardsInEnemyHand.Count == 0)
        {
            if(battle.playerAction.turn == 0)
            {
                EnemyController.instance.EnemyDraw(1);
            }
        }

        if (battle.playerAction.turn > 0)
        {
            EnemyController.instance.EnemyDraw(0);
            EnemyController.instance.EnemyDraw(0);
        }

        battle.RefillMana(1);

        if (battle.enemyCurrentMana < battle.enemyMaxMana)
        {
            battle.enemyCurrentMana++;
        }

        //AI Places
        if (EnemyController.instance.enemyAI == EnemyController.EnemyAI.handRandom)
        {
            HandRandomPlacePoints();
        }

        if (EnemyController.instance.enemyAI == EnemyController.EnemyAI.offensiveAI)
        {
            OffensivePlacePoints();
        }

        if (EnemyController.instance.enemyAI == EnemyController.EnemyAI.deffensiveAI)
        {
            DeffensivePlacePoints();
        }
        ///////

        EnemyController.instance.StartEnemyAction();
    }
    public override void LogicsUpdate(BattleController battle)
    {

    }
    public override void ExitState(BattleController battle)
    {

    }

    public void SelectCard()
    {

        ////Lista de possiveis cartas para colocação baseada no custo de mana
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
    public void HandRandomPlacePoints()
    {
        //Lista de places disponiveis para colocação de carta
        availablePoints = new List<CardPlacePoint>();
        availablePoints.Clear();

        foreach (CardPlacePoint point in BattleController.instance.enemyCardPoints)
        {
            if (point.enemyCard == null)
            {
                availablePoints.Add(point);
            }
        }

        SelectCard();
    }
    public void OffensivePlacePoints()
    {
        offensivePlacePoints = new List<CardPlacePoint>();
        offensivePlacePoints.Clear();

        for (int i = 0; i < BattleController.instance.playerCardPoints.Count; i++)
        {
            CardPlacePoint playerPoint = BattleController.instance.playerCardPoints[i];
            CardPlacePoint enemyPoint = BattleController.instance.enemyCardPoints[i];

            if (playerPoint.playerCard == null && enemyPoint.enemyCard == null)
            {
                offensivePlacePoints.Add(enemyPoint);
            }
        }

        if (offensivePlacePoints.Count == 0)
        {
            offensivePlacePoints = new List<CardPlacePoint>();
            offensivePlacePoints.Clear();

            foreach (CardPlacePoint point in BattleController.instance.enemyCardPoints)
            {
                if (point.enemyCard == null)
                {
                    offensivePlacePoints.Add(point);
                }
            }
        }

        SelectCard();
    }
    public void DeffensivePlacePoints()
    {
        deffensivePlacePoints = new List<CardPlacePoint>();
        deffensivePlacePoints.Clear();


        for (int i = 0; i < BattleController.instance.playerCardPoints.Count; i++)
        {
            CardPlacePoint playerPoint = BattleController.instance.playerCardPoints[i];
            CardPlacePoint enemyPoint = BattleController.instance.enemyCardPoints[i];

            if (playerPoint.playerCard != null && enemyPoint.enemyCard == null)
            {
                deffensivePlacePoints.Add(enemyPoint);
            }
        }

        if (deffensivePlacePoints.Count == 0)
        {
            deffensivePlacePoints = new List<CardPlacePoint>();
            deffensivePlacePoints.Clear();

            foreach (CardPlacePoint point in BattleController.instance.enemyCardPoints)
            {
                if (point.enemyCard == null)
                {
                    deffensivePlacePoints.Add(point);
                }
            }
        }

        SelectCard();
    }
}
