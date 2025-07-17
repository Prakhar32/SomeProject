using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
        Assert.Throws(typeof(MissingReferenceException), () => new CardSetter(new List<Sprite>(), null));
    }

    [Test]
    public void CardMatcherCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        Assert.Throws(typeof(NullReferenceException), () => 
            new CardSetter(new List<Sprite>() { HelperMethods.createSpriteStub()}, null));
    }

    [Test]
    public void SpriteAssigned()
    {
        Sprite sprite = HelperMethods.createSpriteStub();
        CardSetter cardSetter = new CardSetter(new List<Sprite>() { sprite }, new CardMatcher());
        CardView cardView = HelperMethods.ConvertGameobjectIntoCard(new GameObject());
        cardSetter.SetFaceUpSprites(cardView);
        Assert.IsTrue(cardView.FaceUpSprite == sprite);
    }
}
