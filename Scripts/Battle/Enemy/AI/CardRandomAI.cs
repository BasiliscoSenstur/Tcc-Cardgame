using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomAI : MonoBehaviour
{
    public IEnumerator EnemyActionCo()
    {
        yield return new WaitForSeconds(1);

        List<CardPlacePoint> cardPoints = new List<CardPlacePoint>();
        cardPoints.AddRange(CardPointsController.instance.enemyPoints);

        int randomPoint = Random.Range(0, cardPoints.Count);
        CardPlacePoint selectedPoint = cardPoints[randomPoint];

        while (selectedPoint.enemyCard != null && cardPoints.Count > 0)
        {
            randomPoint = Random.Range(0, cardPoints.Count);
            selectedPoint = cardPoints[randomPoint];
            cardPoints.RemoveAt(randomPoint);
        }

        if (selectedPoint.enemyCard == null)
        {
            EnemyCard newEnemyCard = Instantiate (EnemyController.instance.enemyCardToSpawn, 
                EnemyController.instance.spawnPoint.position, EnemyController.instance.spawnPoint.transform.rotation);

            newEnemyCard.data = EnemyController.instance.activeCards[0];
            EnemyController.instance.activeCards.RemoveAt(0);
            newEnemyCard.SetUpCard();

            BattleController.instance.SpendMana(1, newEnemyCard.data.manaCost);

            //Bandaid Bug (the card doest not move to position without the wait)
            yield return new WaitForSeconds(0.2f);
            newEnemyCard.MoveCardToPosition(selectedPoint.transform.position, EnemyController.instance.placeRotation.transform.rotation);

            selectedPoint.enemyCard = newEnemyCard;
        }
        yield return new WaitForSeconds(1f);
        BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
    }
}
