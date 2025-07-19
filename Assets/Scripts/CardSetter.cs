using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardSetter
{
    private Sprite[] _sprites;
    private CardMatcher _matcher;

    public CardSetter(SpriteAtlas atlas, CardMatcher Matcher)
    {
        if(atlas == null)
        {
            throw new NullReferenceException("SpriteAtlus cannot be null");
        }

        if(atlas.spriteCount == 0)
        {
            throw new ArgumentException("SpriteAtlus cannot be empty");
        }

        if(Matcher == null)
        {
            throw new NullReferenceException("Matcher cannot be null");
        }

        _sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(_sprites);
        _matcher = Matcher;
    }

    private Sprite chooseingRandomSprite()
    {
        return _sprites[UnityEngine.Random.Range(0, _sprites.Length)];
    }

    public void SetupCards(List<CardView> cardViews)
    {
        int cardCount = cardViews.Count;
        for(int i = 0; i < cardCount; i++)
        {
            Sprite selectedSprite = chooseingRandomSprite();
            setupCard(cardViews, selectedSprite, cardCount--);
            setupCard(cardViews, selectedSprite, cardCount--);
        }
    }

    private void setupCard(List<CardView> cardViews, Sprite sprite, int cardCount)
    {
        CardView cardView = cardViews[UnityEngine.Random.Range(0, cardCount)];
        cardView.CardMatcher = _matcher;
        cardView.FaceUpSprite = sprite;
        cardViews.Remove(cardView);
    }
}
