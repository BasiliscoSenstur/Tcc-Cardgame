using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    private void Awake()
    {
        instance = this;
    }
    // ------------------------------------ //

    [Header("Deck & Card")]
    [SerializeField] List<CardSO> deck = new List<CardSO>();
    public List<CardSO> activeCards = new List<CardSO>();
    public EnemyCard enemyCardToSpawn;
    public Transform spawnPoint, placeRotation;

    [Header("AI")]
    [HideInInspector] public CardRandomAI cardRandomAI;
    [HideInInspector] public HandRandomAI handRandomAI;
    [HideInInspector] public OffensiveAI offensiveAI;
    [HideInInspector] public DefensiveAI defensiveAI;
    public enum EnemyAI { cardRandom, handRandom, offensiveAI, defensiveAI }
    public EnemyAI enemyAI;

    public List<CardSO> cardsInEnemyHand = new List<CardSO>();
    public int startHandSize;
    public CardSO selectedCard = null;

    void Start()
    {
        SetUpDeck();
        UIController.instance.UpdateManaDisplay(1);

        if (enemyAI != EnemyAI.cardRandom)
        {
            SetUpHand();
        }

        cardRandomAI = GetComponent<CardRandomAI>();
        handRandomAI = GetComponent<HandRandomAI>();
        offensiveAI = GetComponent<OffensiveAI>();
        defensiveAI = GetComponent<DefensiveAI>();
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

    public void StartEnemyAction()
    {
        if (enemyAI == EnemyAI.cardRandom) 
        {
            StartCoroutine(cardRandomAI.EnemyActionCo());
        }

        if (enemyAI == EnemyAI.handRandom)
        {
            StartCoroutine(handRandomAI.EnemyActionCo());
        }

        if (enemyAI == EnemyAI.offensiveAI)
        {
            StartCoroutine(offensiveAI.EnemyActionCo());
        }

        if (enemyAI == EnemyAI.defensiveAI)
        {
            StartCoroutine(defensiveAI.EnemyActionCo());
        }
    }

    public void SetUpHand()
    {
        for (int i = 0; i < startHandSize; i++)
        {
            if(activeCards.Count == 0)
            {
                SetUpDeck();
            }
            cardsInEnemyHand.Add(activeCards[0]);
            activeCards.RemoveAt(0);
        }
    }

    public void PlayCard(CardSO card, CardPlacePoint selectedPlace)
    {
        StartCoroutine(PlayCardCo(card, selectedPlace));
    }

    IEnumerator PlayCardCo(CardSO card, CardPlacePoint selectedPlace)
    {
        EnemyCard newEnemyCard = Instantiate(enemyCardToSpawn, spawnPoint.position, spawnPoint.transform.rotation);

        newEnemyCard.data = card;

        newEnemyCard.SetUpCard();

        yield return new WaitForSeconds(0.2f);

        newEnemyCard.MoveCardToPosition(selectedPlace.transform.position, placeRotation.transform.rotation);

        selectedPlace.enemyCard = newEnemyCard;

        BattleController.instance.SpendMana(1, newEnemyCard.data.manaCost);

        if (cardsInEnemyHand.Count > 0)
        {
            cardsInEnemyHand.RemoveAt(0);
        }
    }

    //Retorna um item do tipo CardSO
    public CardSO CardToPlay()
    {
        //Valor a ser retornado definido como null
        CardSO cardToPlay = null;
        List<CardSO> cardsToPlay = new List<CardSO>();

        //lista de possiveis valores
        foreach (CardSO card in cardsInEnemyHand)
        {
            //Custo de mana
            if (card.manaCost <= BattleController.instance.enemyCurrentMana)
            {
                cardsToPlay.Add(card);
            }
        }

        //Definição real do valor
        if (cardsToPlay.Count > 0)
        {
            int selectedCard = Random.Range(0, cardsToPlay.Count);

            cardToPlay = cardsToPlay[selectedCard];
        }
        else
        {
            cardToPlay = null;
        }

        //retorno de item CardSO
        return cardToPlay;
    }
}
