using UnityEngine;

[System.Serializable]
public class CardMemeto
{
    string cardState;
    string cardSpriteName;
    public CardMemeto(string cardState, string cardSprite)
    {
        this.cardState = cardState;
        this.cardSpriteName = cardSprite;
    }

    public string GetState()
    {
        return cardState;
    }

    public string GetCardSpriteName() 
    {
        return cardSpriteName; 
    }
}
