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

    [UnityTest]
    public IEnumerator ScoreIncreasesByOneonIncrease()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher(); 
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        Score score = new Score(cardMatcher);
        int initialScore = score.getScore();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        int newScore = score.getScore();
        Assert.IsTrue(initialScore + 1 == newScore);
    }

    [UnityTest]
    public IEnumerator CanSubscribeToScoreChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        Score score = new Score(cardMatcher);
        bool scoreChanged = false;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        score.SubscribeToScoreChange(() => scoreChanged = true);
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(scoreChanged);
    }

    [UnityTest]
    public IEnumerator CanUnsubscribeToScoreChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        Score score = new Score(cardMatcher);
        bool scoreChanged = false;
        UnityAction action = () => scoreChanged = true;

        score.SubscribeToScoreChange(action);
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        score.UnsubscribeToScoreChange(action);
        cardView1.Selected();
        cardView2.Selected(); 
        yield return null;

        //Then
        Assert.IsFalse(scoreChanged);
    }
}
