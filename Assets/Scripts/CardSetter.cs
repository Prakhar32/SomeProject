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
            throw new ArgumentException("Sprites cannot be empty");
        }

        if(Matcher == null)
        {
            throw new NullReferenceException("Matcher cannot be null");
        }

        _sprites = Sprites;
        _matcher = Matcher;
    }

    private Sprite chooseingRandomSprite()
    {
        return _sprites[UnityEngine.Random.Range(0, _sprites.Count)];
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
