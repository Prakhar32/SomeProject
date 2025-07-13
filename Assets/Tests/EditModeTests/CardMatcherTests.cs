using NUnit.Framework;
using UnityEngine;

public class CardMatcherTests
{
    [Test]
    public void CardsMatch()
    {
        Sprite sprite = HelperMethods.createSpriteStub();
        CardMatcher matcher = new CardMatcher();
        bool result = matcher.Evaluate(sprite, sprite);
        Assert.IsTrue(result);
    }

    [Test]
    public void CardsDoNotMatch()
    {
        Sprite sprite1 = HelperMethods.createSpriteStub();
        Sprite sprite2 = HelperMethods.createSpriteStub();
        CardMatcher matcher = new CardMatcher();
        bool result = matcher.Evaluate(sprite1, sprite2);
        Assert.IsFalse(result);
    }
}
