using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public enum CardFrom
    {
        player,
        enemy,
    }
    public CardFrom cardOwner;

    [Header("Stats")]
    public int attack;
    public int health, manaCost;

    [Header("Info")]
    public string cardName;
    [TextArea]public string description, lore;

    [Header("Art")]
    public Sprite background; 
    public Sprite character;
}
