using System;
using UnityEngine.Events;

public class TurnCounter
{
    private int _turn = 1;
    private UnityEvent _turnChangeEvent;
    public TurnCounter(CardMatcher cardMatcher)
    {
        if (cardMatcher == null)
            throw new NullReferenceException("CardMatcher cannot be null");

        _turnChangeEvent = new UnityEvent();
        cardMatcher.SubscribeToUnsuccessfulMatch(increaseTurn);
        cardMatcher.SubscribeToSuccessfulMatch(increaseTurn);
    }

    private void increaseTurn()
    {
        _turn++;
        _turnChangeEvent.Invoke();
    }

    internal void setTurnCounter(int turn)
    {
        _turn = turn;
        _turnChangeEvent.Invoke();
    }

    public int getTurnCounter()
    {
        return _turn;
    }

    public void SubscribeToTurnChange(UnityAction action)
    {
        if(action == null)
            throw new NullReferenceException("Action cannot be null");

        _turnChangeEvent.AddListener(action);
    }

    public void UnsubscribeToTurnChange(UnityAction action)
    {
        _turnChangeEvent.RemoveListener(action);
    }
}
