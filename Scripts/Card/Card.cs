using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public CardSO data;

    [Header("Info")]
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text health, manaCost;
    [SerializeField] TMP_Text cardName;
    [SerializeField] TMP_Text description, lore;

    [Header("Art")]
    [SerializeField] Image background;
    [SerializeField] Image character;

    [Header("Status")]
    public int playerCardHealth;

    [Header("Adjustments")]
    public int positionInHand;
    public bool isSelected, inHand;
    [SerializeField] LayerMask whatIsDesktop, whatIsPlacement;
    float moveSpeed = 5f, rotationSpeed = 540f;
    Vector3 targetPosition;
    Quaternion targetRotation;
    Collider coll;
    bool clicked;

    [SerializeField] Animator anim;
    string currentState;

    void Start()
    {
        SetUpCard();
        playerCardHealth = data.health;
        coll = GetComponent<Collider>();
    }
    void Update()
    {
        //Health
        health.text = playerCardHealth.ToString();

        //Moviment
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Placement
        if (isSelected && !clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, whatIsDesktop))
            {
                MoveCardToPosition(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement) && BattleController.instance.playerCurrentMana >= data.manaCost)
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if (selectedPoint.playerCard == null && selectedPoint.isPlayerCard)
                    {
                        selectedPoint.playerCard = this;
                        BattleController.instance.SpendMana(0, data.manaCost);
                        AudioManager.instance.PlaySoundFx(3);
                        MoveCardToPosition(selectedPoint.transform.position, HandController.instance.maxPos.rotation);

                        isSelected = false;
                        inHand = false;

                        HandController.instance.RemoveCardFromHand(this);
                    }
                }
                else
                {
                    if (BattleController.instance.playerCurrentMana < data.manaCost)
                    {
                        AudioManager.instance.PlaySoundFx(5);
                        UIController.instance.ShowManaWarning();
                    }
                    ReturnToHand();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }
        }
        clicked = false;
    }

    //Card Movement
    public void MoveCardToPosition(Vector3 position, Quaternion rotation)
    {
        targetPosition = position;
        targetRotation = rotation;
    }
    public void ReturnToHand()
    {
        isSelected = false;
        coll.enabled = true;
        MoveCardToPosition(HandController.instance.cardPositions[positionInHand], HandController.instance.minPos.rotation);
    }

    //Card Stats and Infos
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

    //Mouse actions
    private void OnMouseOver()
    {
        if (BattleController.instance.currentState == BattleController.instance.playerAction)
        {
            if (inHand)
            {
                MoveCardToPosition(HandController.instance.cardPositions[positionInHand] + new Vector3(0f, 2f, 0f), HandController.instance.minPos.rotation);
            }
        }
    }
    private void OnMouseExit()
    {
        if (inHand)
        {
            MoveCardToPosition(HandController.instance.cardPositions[positionInHand], HandController.instance.minPos.rotation);
        }
    }
    private void OnMouseDown()
    {
        if(BattleController.instance.currentState == BattleController.instance.playerAction && Time.timeScale == 1)
        {
            if (inHand)
            {
                clicked = true;
                isSelected = true;
                coll.enabled = false;
            }
        }
    }

    public void ChangeAnimation(string newState)
    {
        if(currentState == newState)
        {
            return;
        }
        anim.Play(newState);
        currentState = newState;
    }
}
