using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    private void Awake()
    {
        instance = this;
        drawManaCost = 2;

        playerCurrentLP = playerLifePoints;

        enemyCurrentLP = enemyLifePoints;
        enemyCurrentMana = enemyStartMana;
    }
    // ------------------------------------ //

    [Header("Player")]
    public int playerLifePoints;
    public int playerCurrentLP, playerCurrentMana;
    public int playerStartMana, playerMaxMana;

    [Header("Enemy")]
    public int enemyLifePoints;
    public int enemyCurrentLP, enemyCurrentMana;
    public int enemyStartMana, enemyMaxMana;

    //[HideInInspector] 
    public int drawManaCost;

    [Header("States")]
    public string STATE;
    public BattleAbstract currentState;
    public PlayerAction playerAction = new PlayerAction();
    public PlayerAttack playerAttack = new PlayerAttack();
    public EnemyAction enemyAction = new EnemyAction();
    public EnemyAttack enemyAttack = new EnemyAttack();
    public BattleEnded battleEnded = new BattleEnded();

    //Battle
    public Transform discardPile;
    public List<CardPlacePoint> enemyCardPoints;
    public List<CardPlacePoint> playerCardPoints;

    //public List<CardPlacePoint> availablePoints;
    //public List<CardSO> availableCards;

    //public int randomCard, randomPoint;

    //public List<CardPlacePoint> offensivePlacePoints;
    //public List<CardPlacePoint> deffensivePlacePoints;

    void Start()
    {
        //Enemy points
        enemyCardPoints = new List<CardPlacePoint>();
        enemyCardPoints.AddRange(CardPointsController.instance.enemyPoints);

        //Player points
        playerCardPoints = new List<CardPlacePoint>();
        playerCardPoints.AddRange(CardPointsController.instance.playerPoints);

        currentState = playerAction;
        currentState.EnterState(this);
    }

    void Update()
    {
        STATE = currentState.ToString();
        currentState.LogicsUpdate(this);
    }

    public void SwitchState(BattleAbstract state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void SpendMana(int key, int amount)
    {
        if (key == 0)
        {
            playerCurrentMana -= amount;
            {
                if (playerCurrentMana <= 0)
                {
                    playerCurrentMana = 0;
                }
            }
            UIController.instance.UpdateManaDisplay(key);
        }
        if (key == 1)
        {
            enemyCurrentMana -= amount;
            {
                if (enemyCurrentMana <= 0)
                {
                    enemyCurrentMana = 0;
                }
            }
            UIController.instance.UpdateManaDisplay(key);
        }
    }

    public void RefillMana(int key)
    {
        if (key == 0)
        {
            if(playerCurrentMana < playerStartMana) 
            {
                playerCurrentMana = playerStartMana;
            }
            UIController.instance.UpdateManaDisplay(key);
        }
        if (key == 1)
        {
            if (enemyCurrentMana < enemyStartMana)
            {
                enemyCurrentMana = enemyStartMana;
            }
            UIController.instance.UpdateManaDisplay(key);
        }
    }

    public void DemageLifePoints(int key, int amount)
    {
        if (key == 0)
        {
            playerCurrentLP -= amount;
            if(playerCurrentLP <= 0)
            {
                playerCurrentLP = 0;
                Debug.Log("You Lose");
                SwitchState(battleEnded);
            }
        }
        if (key == 1)
        {
            enemyCurrentLP -= amount;
            if (enemyCurrentLP <= 0)
            {
                enemyCurrentLP = 0;
                Debug.Log("You Win");
                SwitchState(battleEnded);
            }
        }
        UIController.instance.ShowDamageIndicator(key, amount);
    }

    public void EndTurn()
    {
        if (playerAction.turn == 0)
        {
            SwitchState(enemyAction);
        }
        else
        {
            SwitchState(playerAttack);
        }
    }
}
