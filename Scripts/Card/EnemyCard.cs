using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCard : MonoBehaviour
{
    public CardSO data;

    [Header("Status Texts")]
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text health, manaCost;

    [Header("Info")]
    [SerializeField] TMP_Text cardName;
    [SerializeField] TMP_Text description, lore;

    [Header("Art")]
    [SerializeField] Image background;
    [SerializeField] Image character;

    [Header("Status")]
    public int enemyCardHealth;

    //placement
    float moveSpeed = 5f, rotationSpeed = 540f;
    Vector3 targetPosition;
    Quaternion targetRotation;

    [SerializeField] Animator anim;
    string currentState;

    void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;

        SetUpCard();
        enemyCardHealth = data.health;
    }

    // Update is called once per frame
    void Update()
    {
        health.text = enemyCardHealth.ToString();

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //Card Movement
    public void MoveCardToPosition(Vector3 position, Quaternion rotation)
    {
        targetPosition = position;
        targetRotation = rotation;
    }

    public void SetUpCard()
    {
        //Stats
        attack.text = data.attack.ToString();
        health.text = data.health.ToString();
        manaCost.text = data.manaCost.ToString();

        //Infos
        cardName.text = data.cardName;
        description.text = data.description;
        lore.text = data.lore;

        //Art
        background.sprite = data.background;
        character.sprite = data.character;
    }
    public void ChangeAnimation(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        anim.Play(newState);
        currentState = newState;
    }
}
