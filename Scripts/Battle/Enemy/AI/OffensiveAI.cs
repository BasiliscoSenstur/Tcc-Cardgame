using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAI : MonoBehaviour
{
    public IEnumerator EnemyActionCo()
    {
        Debug.Log("Offensive AI");
        yield return new WaitForSeconds(2f);
        BattleController.instance.SwitchState(BattleController.instance.enemyAttack);
    }
}
