using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CardTests
{
    [UnityTest]
    public IEnumerator ImageComponentMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();

        CardView card = g.AddComponent<CardView>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        yield return null;
        
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator FaceUpSprite_Missing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        CardView card = g.AddComponent<CardView>();
        g.AddComponent<Image>();

        yield return null;
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator FaceDownSprite_Missing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        CardView card = g.AddComponent<CardView>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        g.AddComponent<Image>();

        yield return null;
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator CardMatcher_NotSet()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        CardView card = g.AddComponent<CardView>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        card.FaceDownSprite = HelperMethods.createSpriteStub();
        g.AddComponent<Image>();

        yield return null;
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator ButtonNotSet()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        CardView card = g.AddComponent<CardView>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        card.FaceDownSprite = HelperMethods.createSpriteStub();
        g.AddComponent<Image>();
        card.CardMatcher = new CardMatcher();

        yield return null;
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator Card_Initially_FaceUp()
    {
        //Given
        GameObject g = new GameObject();
        CardView card = HelperMethods.ConvertGameobjectIntoCard(g);
        card.CardMatcher = new CardMatcher();
        yield return null;

        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == card.FaceUpSprite);
    }

    [UnityTest]
    public IEnumerator CardFacedown_AfterSomeTime()
    {
        //Given
        GameObject g = new GameObject();
        CardView card = HelperMethods.ConvertGameobjectIntoCard(g);
        card.CardMatcher = new CardMatcher();
        yield return null;

        //When
        yield return new WaitForSeconds(Constants.ViewTime);
        
        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == card.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator Card_FaceUp_WhenClickingOn_FaceDownCard()
    {
        //Given
        GameObject g = new GameObject();
        CardView card = HelperMethods.ConvertGameobjectIntoCard(g);
        card.CardMatcher = new CardMatcher();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        
        //When
        card.Selected();

        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == g.GetComponent<CardView>().FaceUpSprite);
    }

    [UnityTest]
    public IEnumerator StateRestoredOnLoadingMemento()
    {
        //Given
        GameObject g = new GameObject();
        CardView card = HelperMethods.ConvertGameobjectIntoCard(g);
        card.CardMatcher = new CardMatcher();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        
        //When
        CardMemeto memento = card.SaveState();
        card.Selected();
        card.LoadState(memento);

        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == card.FaceDownSprite);
    }
}
