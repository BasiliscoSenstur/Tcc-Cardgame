using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Globalization;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }
    // ------------------------------------ //
    [Header("Player")]
    [SerializeField] TMP_Text playerLifePoints;
    [SerializeField] TMP_Text playerMana;

    [Header("Enemy")]
    [SerializeField] TMP_Text enemyLifePoints;
    [SerializeField] TMP_Text enemyMana;

    [Header("Battle")]
    [SerializeField] TMP_Text manaCost;
    [SerializeField] TMP_Text turn;
    [SerializeField] GameObject manaWarning;
    public GameObject manaDrawButton, endTurnButton;
    [SerializeField] UIDamageIndicator playerDamageIndicator, enemyDamageIndicator;
    float manaWarningCounter;
    public Transform canvas;

    [Header("Battle End")]
    public CanvasGroup endBattleScreen;
    public TMP_Text battleResult;

    public GameObject pauseScreen;

    void Start()
    {

    }

    void Update()
    {
        // \n quebra a linha
        manaCost.text = "Draw\n" + "(" + BattleController.instance.drawManaCost.ToString() + " Mana)";
        turn.text = "Turn: " + (BattleController.instance.playerAction.turn + 1).ToString();

        //Update LPs
        playerLifePoints.text = "Life Points: " + BattleController.instance.playerCurrentLP.ToString();
        enemyLifePoints.text = BattleController.instance.enemyCurrentLP.ToString() + " :Life Points";

        //Mana Warning
        if (manaWarningCounter > 0)
        {
            manaWarningCounter -= Time.deltaTime;
        }
        if(manaWarningCounter <= 0)
        {
            manaWarning.SetActive(false);
            manaWarningCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateManaDisplay(int key)
    {
        if (key == 0)
        {
            playerMana.text = "Mana: " + BattleController.instance.playerCurrentMana.ToString();
        }
        if (key == 1)
        {
            enemyMana.text = BattleController.instance.enemyCurrentMana.ToString() + " :Mana";
        }
    }

    public void ShowManaWarning()
    {
        manaWarning.SetActive(true);
        manaWarningCounter = 2f;
    }

    public void ManaDraw()
    {
        if (BattleController.instance.playerCurrentMana >= BattleController.instance.drawManaCost)
        {
            AudioManager.instance.PlaySoundFx(4);
            DeckController.instance.DrawCard();
            BattleController.instance.SpendMana(0, BattleController.instance.drawManaCost);
            BattleController.instance.drawManaCost++;
        }
        else
        {
            AudioManager.instance.PlaySoundFx(5);
            ShowManaWarning();
            manaDrawButton.SetActive(false);
        }
    }

    public void ShowDamageIndicator(int key, int amount)
    {
        if (key == 0)
        {
            UIDamageIndicator newIndicator = Instantiate(playerDamageIndicator);
            newIndicator.transform.SetParent(canvas.transform, false);
            newIndicator.damageText.text = amount.ToString();
            newIndicator.gameObject.SetActive(true);
        }
        if (key == 1)
        {
            UIDamageIndicator newIndicator = Instantiate(enemyDamageIndicator);
            newIndicator.transform.SetParent(canvas.transform, false);
            newIndicator.damageText.text = amount.ToString();
            newIndicator.gameObject.SetActive(true);
        }
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySoundFx(4);
        SceneManager.LoadScene(0);
        AudioManager.instance.PlayMusic(0);
        Time.timeScale = 1;

    }    
    public void PlayAgain()
    {
        AudioManager.instance.PlaySoundFx(4);
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }    
    public void SelectAnotherBattle()
    {
        AudioManager.instance.PlaySoundFx(4);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

        AudioManager.instance.PlayMusic(1);
    }

    public void PauseUnpause()
    {
        if(pauseScreen.activeSelf == true)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
