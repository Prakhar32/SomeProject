using NUnit.Framework;
using System.Collections;
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
        arrangementGenerator.CardPrefab = new GameObject();
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
}
