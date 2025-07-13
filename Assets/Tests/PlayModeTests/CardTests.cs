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

        Card card = g.AddComponent<Card>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        yield return null;
        
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator FaceUpSprite_Missing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        Card card = g.AddComponent<Card>();
        g.AddComponent<Image>();

        yield return null;
        Assert.IsTrue(card == null);
    }

    [UnityTest]
    public IEnumerator FaceDownSprite_Missing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        Card card = g.AddComponent<Card>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        g.AddComponent<Image>();

        yield return null;
        Assert.IsTrue(card == null);
    }

    private Card ConvertGameobjectIntoCard(GameObject g)
    {
        Card card = g.AddComponent<Card>();
        card.FaceUpSprite = HelperMethods.createSpriteStub();
        card.FaceDownSprite = HelperMethods.createSpriteStub();

        g.AddComponent<Image>();
        return card;
    }

    [UnityTest]
    public IEnumerator Card_Initially_FaceDown()
    {
        //Given
        GameObject g = new GameObject();
        Card card = ConvertGameobjectIntoCard(g);
        yield return null;

        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == card.FaceDownSprite);
    }

    [UnityTest]
    public IEnumerator Card_FaceUp_WhenClickingOn_FaceDownCard()
    {
        //Given
        GameObject g = new GameObject();
        Card card = ConvertGameobjectIntoCard(g);
        yield return null;

        //When
        card.Selected();

        //Then
        Assert.IsTrue(g.GetComponent<Image>().sprite == g.GetComponent<Card>().FaceUpSprite);
    }
}
