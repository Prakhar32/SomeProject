using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTests
{
    [Test]
    public void NullCannotSubscribed()
    {
        Score score = new Score(new CardMatcher());
        Assert.Throws<NullReferenceException>(() => score.SubscribeToScoreChange(null));
    }

    [Test]
    public void NullCannotBeUnsubscribed()
    {
        Score score = new Score(new CardMatcher());
        Assert.Throws<NullReferenceException>(() => score.UnsubscribeToScoreChange(null));
    }
}
