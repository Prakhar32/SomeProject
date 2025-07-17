using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetter
{
    private List<Sprite> _sprites = new List<Sprite>();
    private CardMatcher _matcher;

    public CardSetter(List<Sprite> Sprites, CardMatcher Matcher)
    {
        if(Sprites == null)
        {
            throw new NullReferenceException("Sprites cannot be null");
        }

        if(Sprites.Count == 0)
        {
            throw new MissingReferenceException("Sprites cannot be Null or Empty");
        }

        if(Matcher == null)
        {
            throw new NullReferenceException("Matcher cannot be null");
        }

        _sprites = Sprites;
        _matcher = Matcher;
    }

    public void SetFaceUpSprites(CardView cardView)
    {
        cardView.CardMatcher = _matcher;
        cardView.FaceUpSprite = _sprites[0];
    }
}
