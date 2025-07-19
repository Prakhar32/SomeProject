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
        Assert.Throws(typeof(NullReferenceException), () => new CardSetter(null, null));
    }

    [Test]
    public void Sprites_Empty()
    {
        LogAssert.ignoreFailingMessages = true;
        Assert.Throws(typeof(ArgumentException), () => new CardSetter(new SpriteAtlas(), null));
    }

    [Test]
    public void CardMatcherCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        Assert.Throws(typeof(NullReferenceException), () => 
            new CardSetter(HelperMethods.GetSpriteAtlus(), null));
    }

    [Test]
    public void SpriteAssigned()
    {
        //Given
        CardSetter cardSetter = new CardSetter(HelperMethods.GetSpriteAtlus(), new CardMatcher());
        
        //When
        CardView cardView = HelperMethods.ConvertGameobjectIntoCard(new GameObject());
        cardSetter.SetupCards(new List<CardView>() { cardView, cardView});
          
        //Then
        Assert.IsTrue(cardView.FaceUpSprite != null);
    }
}
