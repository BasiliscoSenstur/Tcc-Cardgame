using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRandomAI : MonoBehaviour
{
    public IEnumerator EnemyActionCo()
    {
        Debug.Log("Hand Random AI");

        yield return new WaitForSeconds(1);

        List<CardPlacePoint> cardPoints = new List<CardPlacePoint>();
        cardPoints.AddRange(CardPointsController.instance.enemyPoints);

        int randomPoint = Random.Range(0, cardPoints.Count);
        CardPlacePoint selectedPoint = cardPoints[randomPoint];

        cardPoints.RemoveAt(randomPoint);

        int iteration = 0;
        while (selectedPoint.enemyCard != null && cardPoints.Count > 0 && iteration < 100)
        {
            randomPoint = Random.Range(0, cardPoints.Count);
            selectedPoint = cardPoints[randomPoint];
            cardPoints.RemoveAt(randomPoint);
            iteration++;
        }

        yield return new WaitForSeconds(1);

        EnemyController.instance.selectedCard = EnemyController.instance.CardToPlay();
        EnemyController.instance.PlayCard(EnemyController.instance.selectedCard, selectedPoint);

        yield return new WaitForSeconds(1f);
        BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
    }
}
