using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
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
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1);
        card1.CardMatcher = matcher;

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2);
        card2.CardMatcher = matcher;

        card2.FaceUpSprite = card1.FaceUpSprite;
        yield return null;

        //When
        card1.Selected();
        card2.Selected();
        yield return null;

        //Then
        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        Assert.IsTrue(card1 == null);
        Assert.IsTrue(card2 == null);
    }

    [UnityTest]
    public IEnumerator Pause_BeforeDestoying_WhenMatched()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1);
        card1.CardMatcher = matcher;

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2);
        card2.CardMatcher = matcher;

        card2.FaceUpSprite = card1.FaceUpSprite;
        yield return null;

        //When
        card1.Selected();
        card2.Selected();

        //Then
        yield return null;
        Assert.IsFalse(card1 == null);
        Assert.IsFalse(card2 == null);  

        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        Assert.IsTrue(card1 == null);
        Assert.IsTrue(card2 == null);
    }

    [UnityTest]
    public IEnumerator TwoDifferentCardsSelected()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = new GameObject();
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1);
        card1.CardMatcher = matcher;

        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2);
        card2.CardMatcher = matcher;

        yield return null;

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
        CardView card1 = HelperMethods.ConvertGameobjectIntoCard(g1);
        card1.CardMatcher = matcher;
        
        GameObject g2 = new GameObject();
        CardView card2 = HelperMethods.ConvertGameobjectIntoCard(g2);
        card2.CardMatcher = matcher;
        yield return null;

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
}
