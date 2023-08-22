using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveAI : MonoBehaviour
{
    public IEnumerator EnemyActionCo()
    {
        Debug.Log("Defensive AI");
        yield return new WaitForSeconds(2f);
        BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
    }
}
