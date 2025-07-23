using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.U2D;

public class CardSetterTests
{
    [Test]
    public void Sprites_Null()
    {
        LogAssert.ignoreFailingMessages = true;
        Assert.Throws(typeof(NullReferenceException), () => new CardSetter(null));
    }

    [Test]
    public void Sprites_Empty()
    {
        LogAssert.ignoreFailingMessages = true;
        Assert.Throws(typeof(ArgumentException), () => new CardSetter(new List<Sprite>()));
    }

    [Test]
    public void SpriteAssigned()
    {
        //Given
        CardMatcher matcher = new CardMatcher();
        CardSetter cardSetter = new CardSetter(HelperMethods.GetSprites());
        CardView cardView = HelperMethods.ConvertGameobjectIntoCard(new GameObject(), matcher);

        //When
        cardSetter.SetupCards(new List<CardView>() { cardView, cardView});
          
        //Then
        Assert.IsTrue(cardView.FaceUpSprite != null);
    }
}
