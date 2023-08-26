using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRandomAI : MonoBehaviour
{
    public int randomCard, randomPoint;
    CardSO selectedCard;
    CardPlacePoint selectedPoint;
    public IEnumerator EnemyActionCo()
    {
        Debug.Log("Hand Random AI");

        yield return new WaitForSeconds(1);

        if (BattleController.instance.enemyAction.availablePoints.Count > 0)
        {
            if(BattleController.instance.enemyAction.availableCards.Count > 0)
            {
                SelectCardAndPlace();

                yield return new WaitForSeconds(1);

                EnemyController.instance.PlayCard(selectedCard, selectedPoint);
                BattleController.instance.enemyAction.availableCards.RemoveAt(randomCard);
                BattleController.instance.enemyAction.availablePoints.RemoveAt(randomPoint);

                yield return new WaitForSeconds(1);

                if(BattleController.instance.enemyAction.availableCards.Count > 0)
                {
                    Debug.Log("Another One");
                    yield return new WaitForSeconds(1);

                    Debug.Log("Redefining Lists");
                    yield return new WaitForSeconds(1);

                    BattleController.instance.enemyAction.AvailablePointsAndCards();

                    yield return new WaitForSeconds(1);
                    EnemyController.instance.StartEnemyAction();
                }
                else
                {
                    Debug.Log("All Cards Placed");
                    BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
                }

                //if (BattleController.instance.enemyAction.availableCards.Count == 0)
                //{
                //    Debug.Log("All Cards Placed");
                //    BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
                //}
                //else
                //{
                //    Debug.Log("Another One");
                //    yield return new WaitForSeconds(1);

                //    Debug.Log("Redefining Lists");
                //    yield return new WaitForSeconds(1);

                //    BattleController.instance.enemyAction.AvailablePoints();
                //    BattleController.instance.enemyAction.AvailableCards();

                //    yield return new WaitForSeconds(1);
                //    EnemyController.instance.StartEnemyAction();
                //}
            }
            else
            {
                Debug.Log("No Available Cards");

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

        Debug.Log("Selectedcard " + selectedCard);

        randomPoint = Random.Range(0, BattleController.instance.enemyAction.availablePoints.Count);
        selectedPoint = BattleController.instance.enemyAction.availablePoints[randomPoint];

        Debug.Log("Selectedplace " + selectedPoint);
    }
}