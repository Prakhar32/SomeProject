using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcher 
{
    private Card _previousCard;
    public bool Evaluate(Sprite card1, Sprite card2)
    {
        return card1 == card2;
    }

    internal void CardSelected(Card card)
    {
        if(_previousCard == null)
        {
            _previousCard = card;
        }
        else
        {
            bool result = Evaluate(_previousCard.FaceUpSprite, card.FaceUpSprite);
            _previousCard.Evaluation(result);
            card.Evaluation(result);
            _previousCard = null; 
        }
    }
}
