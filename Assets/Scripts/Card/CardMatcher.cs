using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        _successfulMatchEvent.AddListener(action);
    }

    public void UnsubscribeToSuccessfulMatch(UnityAction action)
    {
        _successfulMatchEvent.RemoveListener(action);
    }

    public void SubscribeToUnsuccessfulMatch(UnityAction action)
    {
        _unsuccessfulMatchEvent.AddListener(action);
    }

    public void UnsubscribeToUnsuccessfulMatch(UnityAction action)
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

    internal void ResetMatcher()
    {
        _previousCard = null;
        _successfulMatchEvent.RemoveAllListeners();
        _unsuccessfulMatchEvent.RemoveAllListeners();
    }
}
