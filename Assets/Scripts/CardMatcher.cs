using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcher 
{
    public bool Evaluate(Sprite card1, Sprite card2)
    {
        return card1 == card2;
    }
}
