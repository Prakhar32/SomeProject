using UnityEngine;

public class CardMemeto
{
    CardState cardState;
    Sprite cardSprite;
    public CardMemeto(CardState cardState, Sprite cardSprite)
    {
        this.cardState = cardState;
        this.cardSprite = cardSprite;
    }

    public CardState GetState()
    {
        return cardState;
    }

    public Sprite GetCardSprite() 
    {
        return cardSprite; 
    }
}
