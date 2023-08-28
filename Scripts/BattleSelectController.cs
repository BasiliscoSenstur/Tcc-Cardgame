using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSelectController : MonoBehaviour
{
    public void RandomAI()
    {
        SceneManager.LoadScene(2);
    }
    public void DeffensiveAI()
    {
        SceneManager.LoadScene(3);
    }
    public void OffensiveAI()
    {
        SceneManager.LoadScene(4);
    }

}
