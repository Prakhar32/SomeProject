using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CardMatcher 
{
    private CardView _previousCard;
    private UnityEvent _successfulMatchEvent;
    private UnityEvent _unsuccessfulMatchEvent;

    public CardMatcher()
    {
        _successfulMatchEvent = new UnityEvent();
        _unsuccessfulMatchEvent = new UnityEvent();
    }

    public void SubscribeToSuccessfulMatch(UnityAction action)
    {
        if (action == null)
            throw new NullReferenceException("Null cannot subscribe to successful match event");

        _successfulMatchEvent.AddListener(action);
    }

    public void UnsubscribefromSuccessfulMatch(UnityAction action)
    {
        _successfulMatchEvent.RemoveListener(action);
    }

    public void SubscribeToUnsuccessfulMatch(UnityAction action)
    {
        if (action == null)
            throw new NullReferenceException("Null cannot subscribe to unsuccessful match event");

        _unsuccessfulMatchEvent.AddListener(action);
    }

    public void UnsubscribefromUnsuccessfulMatch(UnityAction action)
    {
        _unsuccessfulMatchEvent.RemoveListener(action);
    }

    private bool Evaluate(Sprite card1, Sprite card2)
    {
        return card1 == card2;
    }

    internal void CardSelected(CardView card)
    {
        if(_previousCard == null)
            _previousCard = card;
        else
             evaluateSelection(card);
    }

    private void evaluateSelection(CardView card)
    {
        bool result = Evaluate(_previousCard.FaceUpSprite, card.FaceUpSprite);
        if (result)
            _successfulMatchEvent.Invoke();
        else
            _unsuccessfulMatchEvent.Invoke();

        _previousCard = null;
    }

    internal void ResetCardSelected()
    {
        _previousCard = null;
    }
}
