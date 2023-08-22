using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;
    private void Awake()
    {
        instance = this;
    }
    // ------------------------------------ //
    [SerializeField] List<CardSO> deck = new List<CardSO>();
    public List<CardSO> activeCards = new List<CardSO>();
    [SerializeField] Card card;

    public Transform field;

    void Start()
    {
        SetUpDeck();
    }

    void Update()
    {
        
    }

    public void SetUpDeck()
    {
        activeCards.Clear();

        List<CardSO> tempDeck = new List<CardSO>();
        tempDeck.AddRange(deck);

        int iterations = 0;
        while (tempDeck.Count > 0 && iterations < 250) 
        {
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);

            iterations++;
        }
    }
    public void DrawCard()
    {
        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        Card newCard = Instantiate(card, transform.position, transform.rotation);
        newCard.data = activeCards[0];
        newCard.SetUpCard();    
        activeCards.RemoveAt(0);
        
        HandController.instance.AddCardToHand(newCard);
    }

    public void DrawMultiple(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            DrawCard();
        }
    }

    public void ManaDraw()
    {
        if (BattleController.instance.playerCurrentMana >= BattleController.instance.drawManaCost)
        {
            DrawCard();
            BattleController.instance.SpendMana(0, BattleController.instance.drawManaCost);
            BattleController.instance.drawManaCost++;
        }
        else
        {
            UIController.instance.ShowManaWarning();
        }
    }
}
