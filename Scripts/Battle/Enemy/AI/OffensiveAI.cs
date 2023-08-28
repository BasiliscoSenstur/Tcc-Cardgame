using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAI : MonoBehaviour
{
    public int randomCard, randomPoint;
    CardSO selectedCard;
    CardPlacePoint selectedPoint;

    public IEnumerator EnemyActionCo()
    {
        Debug.Log("Offensive AI");
        yield return new WaitForSeconds(1f);

        BattleController.instance.enemyAction.OffensivePlacePoints();

        if (BattleController.instance.enemyAction.offensivePlacePoints.Count > 0)
        {
            if (BattleController.instance.enemyAction.availableCards.Count > 0)
            {
                SelectCardAndPlace();

                EnemyController.instance.PlayCard(selectedCard, selectedPoint);

                BattleController.instance.enemyAction.availableCards.RemoveAt(randomCard);
                BattleController.instance.enemyAction.offensivePlacePoints.RemoveAt(randomPoint);

                if (BattleController.instance.enemyAction.availableCards.Count > 0)
                {
                    Debug.Log("Another One");
                    yield return new WaitForSeconds(1);
                    EnemyController.instance.StartEnemyAction();
                }
                else
                {
                    Debug.Log("All Cards Placed");
                    BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
                }
            }
            else
            {
                Debug.Log("No Cards Available");
                BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
            }
        }
        else
        {
            Debug.Log("No Available Points");
            BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
        }
    }

    public void SelectCardAndPlace()
    {
        randomCard = Random.Range(0, BattleController.instance.enemyAction.availableCards.Count);
        selectedCard = BattleController.instance.enemyAction.availableCards[randomCard];

        Debug.Log("Selected Card " + selectedCard);

        randomPoint = Random.Range(0, BattleController.instance.enemyAction.offensivePlacePoints.Count);
        selectedPoint = BattleController.instance.enemyAction.offensivePlacePoints[randomPoint];

        Debug.Log("Selected Place " + selectedPoint);
    }
}
