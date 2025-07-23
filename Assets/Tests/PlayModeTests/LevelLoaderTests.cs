using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LevelLoaderTests
{
    [UnityTest]
    public IEnumerator ArrangementGeneratorNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelLoader levelLoader = g.AddComponent<LevelLoader>();
        yield return null;
        
        Assert.IsTrue(levelLoader == null);
    }

    [UnityTest]
    public IEnumerator LevelLoaded()
    {
        //Given
        ArrangementGenerator arrangementGenerator = HelperMethods.GetArrangementGenerator();

        LevelSaver levelSaver = new GameObject().AddComponent<LevelSaver>();
        levelSaver.ArrangementParent = arrangementGenerator.ArrangementParent;
        levelSaver.SetDifficulty(Difficulty.Easy);

        LevelLoader levelLoader = new GameObject().AddComponent<LevelLoader>();
        levelLoader.Generator = arrangementGenerator;
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        levelSaver.SaveLevel();
        
        List<CardView> cardViews = getCardViewsFromArrangement(arrangementGenerator.ArrangementParent);
        Dictionary<int, Sprite> idSpritePairs = getIDSpritePairs(cardViews);
        arrangementGenerator.ResetArrangement();
        
        //When
        levelLoader.LoadLevel();
        yield return null;
        yield return null;

        //Then
        spriteSameForSameID(idSpritePairs, arrangementGenerator.ArrangementParent);
    }

    private List<CardView> getCardViewsFromArrangement(Transform arrangement)
    {
        List<CardView> cardViews = new List<CardView>();
        for(int i = 0; i < arrangement.childCount; i++)
        {
            CardView cardView = arrangement.GetChild(i).GetComponent<CardView>();
            cardViews.Add(cardView);
        }

        return cardViews;
    }

    private Dictionary<int, Sprite> getIDSpritePairs(List<CardView> cardViews)
    {
        Dictionary<int, Sprite> idSpritePairs = new Dictionary<int, Sprite>();
        foreach (CardView card in cardViews)
        {
            idSpritePairs.Add(card.ID, card.FaceUpSprite);
        }
        return idSpritePairs;
    }

    private void spriteSameForSameID(Dictionary<int, Sprite> idSpritePairs, Transform parent)
    {
        Assert.IsFalse(parent.childCount == 0);
        for(int i = 0; i < parent.childCount; i++)
        {
            CardView cardView = parent.GetChild(i).GetComponent<CardView>();
            if (idSpritePairs.ContainsKey(cardView.ID))
            {
                Assert.AreEqual(idSpritePairs[cardView.ID], cardView.FaceUpSprite);
            }
            else
            {
                Assert.Fail($"Card with ID {cardView.ID} not found in idSpritePairs.");
            }
        }
    }
}
