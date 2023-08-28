using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static HandController instance;
    private void Awake()
    {
        instance = this;
    }
    //----------------------------------//

    public List<Card> heldCards = new List<Card>();
    public List<Vector3> cardPositions = new List<Vector3>();

    public Transform minPos, maxPos;
    void Start()
    {
        SetUpPositionInHand();
    }

    void Update()
    {
        
    }

    public void SetUpPositionInHand()
    {
        cardPositions.Clear();

        Vector3 distanceBetweenPoints = Vector3.zero;

        if (heldCards.Count > 1)
        {
            distanceBetweenPoints = (maxPos.position - minPos.position) / (heldCards.Count - 1);
        }

        for (int i = 0; i < heldCards.Count; i++)
        {
            cardPositions.Add(minPos.position + (distanceBetweenPoints * i));

            heldCards[i].MoveCardToPosition(cardPositions[i], minPos.rotation);
            heldCards[i].inHand = true;
            heldCards[i].positionInHand = i;
        }
    }
    public void RemoveCardFromHand(Card cardToRemove)
    {
        if (heldCards[cardToRemove.positionInHand] == cardToRemove)
        {
            heldCards.RemoveAt(cardToRemove.positionInHand);
        }
        else
        {
            Debug.Log("Card at position" + cardToRemove.positionInHand + " is not the card being removed from hand");
        }

        SetUpPositionInHand();
    }

    public void AddCardToHand(Card cardToAdd)
    {
        heldCards.Add(cardToAdd);
        SetUpPositionInHand();
    }
}
