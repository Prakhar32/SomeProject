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

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1, matcher);

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2, matcher);
        card2.CardMatcher = matcher;

        card2.FaceUpSprite = card1.FaceUpSprite;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        card1.Selected();
        card2.Selected();
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

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1, matcher);

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2, matcher);

        card2.FaceUpSprite = card1.FaceUpSprite;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        card1.Selected();
        card2.Selected();

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

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1, matcher);

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2, matcher);

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        card1.Selected();
        card2.Selected();

        //Then
        yield return new WaitForSeconds (Constants.ResetTime);
        Assert.IsTrue(g1.GetComponent<Image>().sprite = card1.FaceDownSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite = card2.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator Pause_BeforeReseting_WhenDifferent()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1, matcher);
        
        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2, matcher);
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        card1.Selected();
        card2.Selected();
        
        //Then
        yield return null;
        Assert.IsTrue(g1.GetComponent<Image>().sprite == card1.FaceUpSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite == card2.FaceUpSprite);
        
        yield return new WaitForSeconds(Constants.ResetTime);
        Assert.IsTrue(g1.GetComponent<Image>().sprite == card1.FaceDownSprite);
        Assert.IsTrue(g2.GetComponent<Image>().sprite == card2.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator SubscribedToSuccessfulSelection()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        bool matchSuccessful = false;
        cardMatcher.SubscribeToSuccessfulMatch(() => matchSuccessful = true);
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
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
        
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;
        
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        
        //When
        cardMatcher.UnsubscribeToSuccessfulMatch(action);
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
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
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

        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), cardMatcher);

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardMatcher.UnsubscribeToUnsuccessfulMatch(action);
        cardView1.Selected();
        cardView2.Selected();

        //Then
        Assert.IsFalse(matchFailed);
    }
}
