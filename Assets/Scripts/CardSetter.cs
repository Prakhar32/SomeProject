using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardSetter
{
    private Sprite[] _sprites;

    public CardSetter(List<Sprite> sprites)
    {
        if(sprites == null)
        {
            throw new NullReferenceException("SpriteAtlus cannot be null");
        }

        if(sprites.Count == 0)
        {
            throw new ArgumentException("SpriteAtlus cannot be empty");
        }

        _sprites = sprites.ToArray();
    }

    private Sprite choosingRandomSprite()
    {
        return _sprites[UnityEngine.Random.Range(0, _sprites.Length)];
    }

    public void SetupCards(List<CardView> cardViews)
    {
        int cardCount = cardViews.Count;
        while(cardCount > 0)
        {
            Sprite selectedSprite = choosingRandomSprite();
            setupCard(cardViews, selectedSprite, cardCount--);
            setupCard(cardViews, selectedSprite, cardCount--);
        }
    }

    private void setupCard(List<CardView> cardViews, Sprite sprite, int cardCount)
    {
        CardView cardView = cardViews[UnityEngine.Random.Range(0, cardCount)];
        cardView.FaceUpSprite = sprite;

        cardViews.Remove(cardView);
    }
}
