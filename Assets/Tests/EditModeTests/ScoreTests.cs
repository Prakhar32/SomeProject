using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

public class ScoreTests
{
    [Test]
    public void CardMatcherIsNull()
    {
        Assert.Throws<NullReferenceException>(() => new Score(null));
    }

    [Test]
    public void ScoreInitiallyZero()
    {
        CardMatcher cardMatcher = new CardMatcher();
        Score score = new Score(cardMatcher);
        Assert.IsTrue(score.getScore() == 0);
    }

    [Test]
    public void ScoreIncreasesByOneonIncrease()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        Score score = new Score(cardMatcher);
        int initialScore = score.getScore();

        //When
        score.IncreaseScore();

        //Then
        int newScore = score.getScore();
        Assert.IsTrue(initialScore + 1 == newScore);
    }

    [Test]
    public void CanSubscribeToScoreChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        Score score = new Score(cardMatcher);
        bool scoreChanged = false;
        
        //When
        score.SubscribeToScoreChange(() => scoreChanged = true);
        score.IncreaseScore();

        //Then
        Assert.IsTrue(scoreChanged);
    }

    [Test]
    public void CanUnsubscribeToScoreChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        Score score = new Score(cardMatcher);
        bool scoreChanged = false;
        UnityAction action = () => scoreChanged = true;
        score.SubscribeToScoreChange(action);

        //When
        score.UnsubscribeToScoreChange(action);
        score.IncreaseScore();

        //Then
        Assert.IsFalse(scoreChanged);
    }
}
