using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ArrangementGeneratorTests
{
    [UnityTest]
    public IEnumerator ArrangementParentNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator CardPrefabIsNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.ArrangementParent = new GameObject().transform;
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator GridLayoutGroupNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.ArrangementParent = new GameObject().transform;
        arrangementGenerator.CardPrefab = new GameObject();
        arrangementGenerator.CardPrefab.gameObject.SetActive(false);
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator CardMatcherNotSet()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameObject arrangementParent = new GameObject();

        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.LayoutGroup = arrangementParent.AddComponent<GridLayoutGroup>();
        arrangementGenerator.ArrangementParent = arrangementParent.transform;
        arrangementGenerator.CardPrefab = new GameObject();

        yield return null;
        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator FaceDownSprite_Null()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameObject arrangementParent = new GameObject();

        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.LayoutGroup = arrangementParent.AddComponent<GridLayoutGroup>();
        arrangementGenerator.ArrangementParent = arrangementParent.transform;
        arrangementGenerator.CardMatcher = new CardMatcher();
        arrangementGenerator.CardPrefab = new GameObject();
        arrangementGenerator.CardSpriteAtlas = HelperMethods.GetSpriteAtlus();
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator CardPrefab_IsNotCard()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameObject arrangementParent = new GameObject();

        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.LayoutGroup = arrangementParent.AddComponent<GridLayoutGroup>();
        arrangementGenerator.ArrangementParent = arrangementParent.transform;
        arrangementGenerator.CardMatcher = new CardMatcher();
        arrangementGenerator.CardPrefab = new GameObject();
        arrangementGenerator.CardSpriteAtlas = HelperMethods.GetSpriteAtlus();
        arrangementGenerator.FaceDownSprite = HelperMethods.createSpriteStub();
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    private ArrangementGenerator GetArrangementGenerator()
    {
        GameObject g = new GameObject();
        GameObject arrangementParent = new GameObject();

        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.LayoutGroup = arrangementParent.AddComponent<GridLayoutGroup>();
        arrangementGenerator.ArrangementParent = arrangementParent.transform;
        arrangementGenerator.CardMatcher = new CardMatcher();
        
        arrangementGenerator.CardPrefab = new GameObject();
        HelperMethods.ConvertGameobjectIntoCard(arrangementGenerator.CardPrefab);
        arrangementGenerator.CardPrefab.SetActive(false);
        
        arrangementGenerator.CardSpriteAtlas = HelperMethods.GetSpriteAtlus();
        arrangementGenerator.FaceDownSprite = HelperMethods.createSpriteStub();
        return arrangementGenerator;
    }

    [UnityTest]
    public IEnumerator EasyDifficulty_6Elements()
    {
        ArrangementGenerator arrangementGenerator = GetArrangementGenerator();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 6);
    }

    [UnityTest]
    public IEnumerator MediumDifficulty_12Elements()
    {
        ArrangementGenerator arrangementGenerator = GetArrangementGenerator();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Medium);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 12);
    }

    [UnityTest]
    public IEnumerator HardDifficulty_20Elements()
    {
        ArrangementGenerator arrangementGenerator = GetArrangementGenerator();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Hard);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 20);
    }

    [UnityTest]
    public IEnumerator LevelGeneratedisSolvable()
    {
        ArrangementGenerator arrangementGenerator = GetArrangementGenerator();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Hard);
        yield return null;

        Dictionary<Sprite, int> map = new Dictionary<Sprite, int>();
        for(int i = 0; i < arrangementGenerator.ArrangementParent.childCount; i++)
        {
            CardView cardView = arrangementGenerator.ArrangementParent.GetChild(i).GetComponent<CardView>();
            if (map.ContainsKey(cardView.FaceUpSprite))
                map[cardView.FaceUpSprite] += 1;
            else
                map.Add(cardView.FaceUpSprite, 1);
        }

        foreach(int value in map.Values)
        {
            if(value % 2 != 0)
                Assert.Fail();
        }
    }
}
