using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

public class TurnCounterTests
{
    [Test]
    public void CardMatcherIsNull()
    {
        Assert.Throws<NullReferenceException>(() => new TurnCounter(null));
    }

    [Test]
    public void TurnInitiallyOne()
    {
        CardMatcher cardMatcher = new CardMatcher();
        TurnCounter score = new TurnCounter(cardMatcher);
        Assert.IsTrue(score.getTurnCounter() == 1);
    }

    [UnityTest]
    public IEnumerator TurnIncreasesByOneonSuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        TurnCounter turn = new TurnCounter(cardMatcher);
        int initialTurn = turn.getTurnCounter();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        int newScore = turn.getTurnCounter();
        Assert.IsTrue(initialTurn + 1 == newScore);
    }

    [UnityTest]
    public IEnumerator TurnIncreasesByOneonUnsuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();

        TurnCounter turn = new TurnCounter(cardMatcher);
        int initialTurn = turn.getTurnCounter();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        int newScore = turn.getTurnCounter();
        Assert.IsTrue(initialTurn + 1 == newScore);
    }

    [UnityTest]
    public IEnumerator CanSubscribeToTurnChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        TurnCounter turn = new TurnCounter(cardMatcher);
        bool turnChanged = false;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        turn.SubscribeToTurnChange(() => turnChanged = true);
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(turnChanged);
    }

    [UnityTest]
    public IEnumerator CanUnsubscribeToTurnChange()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        TurnCounter turn = new TurnCounter(cardMatcher);
        bool turnChanged = false;
        UnityAction action = () => turnChanged = true;

        turn.SubscribeToTurnChange(action);
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        turn.UnsubscribeToTurnChange(action);
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsFalse(turnChanged);
    }
}
