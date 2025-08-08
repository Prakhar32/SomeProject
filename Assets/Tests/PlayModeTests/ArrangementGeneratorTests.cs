using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class ArrangementGeneratorTests
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        bool sceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) => { sceneLoaded = true; };
        SceneManager.LoadScene(Constants.GammeSceneName);
        yield return new WaitUntil(() => sceneLoaded);
    }

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
        arrangementGenerator.CardSprites = HelperMethods.GetSprites();
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
        arrangementGenerator.CardSprites = HelperMethods.GetSprites();
        arrangementGenerator.FaceDownSprite = HelperMethods.GetRandomSprite();
        yield return null;

        Assert.IsTrue(arrangementGenerator == null);
    }

    [UnityTest]
    public IEnumerator EasyDifficulty_6Elements()
    {
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 6);
    }

    [UnityTest]
    public IEnumerator MediumDifficulty_12Elements()
    {
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Medium);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 12);
    }

    [UnityTest]
    public IEnumerator HardDifficulty_20Elements()
    {
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Hard);
        yield return null;

        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 20);
    }

    [UnityTest]
    public IEnumerator LevelGeneratedisSolvable()
    {
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
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

    [UnityTest]
    public IEnumerator ResetEmptiesCards()
    {
        //Given
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        yield return null;
        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;
        
        //When
        arrangementGenerator.ResetArrangement();
        yield return null;
        
        //Then
        Assert.IsTrue(arrangementGenerator.ArrangementParent.childCount == 0);
    }
}
