using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CardMatchTests
{
    [UnityTest]
    public IEnumerator TwoCardsofSameTypeSelected()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = HelperMethods.GetCard(matcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(matcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView2.CardMatcher = matcher;

        cardView2.FaceUpSprite = cardView1.FaceUpSprite;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        Assert.IsFalse(g1.activeSelf);
        Assert.IsFalse(g2.activeSelf);
    }

    [UnityTest]
    public IEnumerator Pause_BeforeDestoying_WhenMatched()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = HelperMethods.GetCard(matcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(matcher);
        CardView cardView2 = g2.GetComponent<CardView>();

        cardView2.FaceUpSprite = cardView1.FaceUpSprite;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();

        //Then
        yield return null;
        Assert.IsTrue(g1.activeSelf);
        Assert.IsTrue(g2.activeSelf);  

        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        Assert.IsFalse(g1.activeSelf);
        Assert.IsFalse(g2.activeSelf);
    }

    [UnityTest]
    public IEnumerator TwoDifferentCardsSelected()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = HelperMethods.GetCard(matcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(matcher);
        CardView cardView2 = g2.GetComponent<CardView>();

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();

        //Then
        yield return new WaitForSeconds (Constants.ResetTime);
        Assert.IsTrue(g1.GetComponent<Image>().sprite = cardView1.FaceDownSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite = cardView2.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator Pause_BeforeReseting_WhenDifferent()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = HelperMethods.GetCard(matcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(matcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        
        //Then
        yield return null;
        Assert.IsTrue(g1.GetComponent<Image>().sprite == cardView1.FaceUpSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite == cardView2.FaceUpSprite);
        
        yield return new WaitForSeconds(Constants.ResetTime);
        Assert.IsTrue(g1.GetComponent<Image>().sprite == cardView1.FaceDownSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite == cardView2.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator SubscribedToSuccessfulSelection()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        bool matchSuccessful = false;
        cardMatcher.SubscribeToSuccessfulMatch(() => matchSuccessful = true);
        CardView cardView1 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        CardView cardView2 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();

        //Then
        Assert.IsTrue(matchSuccessful);
    }

    [UnityTest]
    public IEnumerator UnsubscribedToSuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        bool matchSuccessful = false;
        UnityAction action = () => matchSuccessful = true;
        cardMatcher.SubscribeToSuccessfulMatch(action);
        
        CardView cardView1 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        CardView cardView2 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;
        
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        
        //When
        cardMatcher.UnsubscribefromSuccessfulMatch(action);
        cardView1.Selected();
        cardView2.Selected();
        
        //Then
        Assert.IsFalse(matchSuccessful);
    }


    [UnityTest]
    public IEnumerator SubscribedToUnsuccessfulSelection()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        bool matchFailed = false;
        cardMatcher.SubscribeToUnsuccessfulMatch(() => matchFailed = true);
        CardView cardView1 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        CardView cardView2 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();

        //Then
        Assert.IsTrue(matchFailed);
    }

    [UnityTest]
    public IEnumerator UnsubscribedToUnsuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        bool matchFailed = false;
        UnityAction action = () => matchFailed = true;
        cardMatcher.SubscribeToUnsuccessfulMatch(action);

        CardView cardView1 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();
        CardView cardView2 = HelperMethods.GetCard(cardMatcher).GetComponent<CardView>();

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardMatcher.UnsubscribefromUnsuccessfulMatch(action);
        cardView1.Selected();
        cardView2.Selected();

        //Then
        Assert.IsFalse(matchFailed);
    }
}
