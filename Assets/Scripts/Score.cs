using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public CardMatcher CardMatcher;

    private int _score = 0; 
    void Start()
    {
        if(CardMatcher == null)
        {
            Destroy(this);
            throw new MissingReferenceException("CardMatcher is not assigned.");
        }

        CardMatcher.SubscribeToSuccessfulMatch(increaseScore);
    }

    private void increaseScore()
    {
        _score++;
    }

    public int getScore()
    {
        return _score;
    }
}
