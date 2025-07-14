using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardMatchTests
{
    [UnityTest]
    public IEnumerator TwoCardsofSameTypeSelected()
    {
        //Given
        CardMatcher matcher = new CardMatcher();

        GameObject g1 = new GameObject();
        Card card1 = HelperMethods.ConvertGameobjectIntoCard(g1);
        card1.CardMatcher = matcher;

        GameObject g2 = new GameObject();
        Card card2 = HelperMethods.ConvertGameobjectIntoCard(g2);
        card2.CardMatcher = matcher;

        card2.FaceUpSprite = card1.FaceUpSprite;
        yield return null;

        //When
        card1.Selected();
        card2.Selected();

        //Then
        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        Assert.IsTrue(card1 == null);
        Assert.IsTrue(card2 == null);
    }
}
