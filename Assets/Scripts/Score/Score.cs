using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score
{
    private int _score;
    private UnityEvent _scoreChangeEvent = new UnityEvent();

    public Score(CardMatcher cardMatcher)
    {
        if (cardMatcher == null)
            throw new NullReferenceException("CardMatcher cannot be null");

        cardMatcher.SubscribeToSuccessfulMatch(increaseScore);
    }

    public int getScore()
    {
        return _score;
    }

    private void increaseScore()
    {
        _score++;
        _scoreChangeEvent.Invoke();
    }

    internal void setScore(int score)
    {
        _score = score;
        _scoreChangeEvent.Invoke();
    }

    public void SubscribeToScoreChange(UnityAction action)
    {
        _scoreChangeEvent.AddListener(action);
    }

    public void UnsubscribeToScoreChange(UnityAction action)
    {
        _scoreChangeEvent.RemoveListener(action);
    }
}
