using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    public CardMatcher Matcher;

    private int _turnCount;
    void Start()
    {
        if(Matcher == null)
        {
            Destroy(this);
            throw new MissingReferenceException("CardMatcher is not assigned.");
        }

        Matcher.SubscribeToUnsuccessfulMatch(increaseTurnCounter);
        Matcher.SubscribeToSuccessfulMatch(increaseTurnCounter);
    }

    private void increaseTurnCounter()
    {
        _turnCount++;
    }

    public int getTurnCount()
    {
        return _turnCount;
    }
}
