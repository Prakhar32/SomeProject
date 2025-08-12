using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultySettor : MonoBehaviour
{
    private Difficulty _difficulty;
    private UnityEvent _difficultyChangedEvent = new UnityEvent();

    public void SetDifficulty(int difficulty)
    {
        if (!Enum.IsDefined(typeof(Difficulty), 3))
            throw new InvalidOperationException("Invalid Difficulty");

        _difficulty = (Difficulty)difficulty;
        _difficultyChangedEvent.Invoke();
    }

    public Difficulty GetDifficulty()
    {
        return _difficulty;
    }

    public void SubscribeToDifficultyChange(UnityAction action)
    {
        if(action == null)
        {
            throw new System.NullReferenceException("Action cannot be null");
        }

        _difficultyChangedEvent.AddListener(action);
    }

    public void UnsubscribeFromDifficultyChange(UnityAction action)
    {
        _difficultyChangedEvent.RemoveListener(action);
    }
}
