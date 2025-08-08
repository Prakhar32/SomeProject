using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LevelLoaderTests
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
    public IEnumerator ArrangementGeneratorNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelLoader levelLoader = g.AddComponent<LevelLoader>();
        yield return null;
        
        Assert.IsTrue(levelLoader == null);
    }

    [UnityTest]
    public IEnumerator ScoreCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
      
        GameObject g = new GameObject();
        LevelLoader levelLoader = g.AddComponent<LevelLoader>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        
        levelLoader.SetDependencies(arrangementGenerator, null, null, null);
        yield return null;

        Assert.IsTrue(levelLoader == null);
    }

    [UnityTest]
    public IEnumerator TurnCounterCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelLoader levelLoader = g.AddComponent<LevelLoader>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();

        levelLoader.SetDependencies(arrangementGenerator, new Score(new CardMatcher()), null, null);
        yield return null;

        Assert.IsTrue(levelLoader == null);
    }

    [UnityTest]
    public IEnumerator TimerCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g = new GameObject();
        LevelLoader levelLoader = g.AddComponent<LevelLoader>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();

        levelLoader.SetDependencies(arrangementGenerator, new Score(cardMatcher), new TurnCounter(cardMatcher), null);
       yield return null;

        Assert.IsTrue(levelLoader == null);
    }

    [UnityTest]
    public IEnumerator LevelLoaded()
    {
        //Given
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        TurnCounterDisplay turnDisplay = GameObject.FindObjectOfType<TurnCounterDisplay>();
        ScoreDisplay scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        TimerDisplay timerDisplay = GameObject.FindObjectOfType<TimerDisplay>();

        LevelSaver levelSaver = GameObject.FindAnyObjectByType<LevelSaver>();
        levelSaver.SetDifficulty(Difficulty.Easy);

        LevelLoader levelLoader = GameObject.FindAnyObjectByType<LevelLoader>();
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        string turn = turnDisplay.gameObject.GetComponent<TextMeshProUGUI>().text;
        string score = scoreDisplay.gameObject.GetComponent<TextMeshProUGUI>().text;
        string timeRemaining = timerDisplay.gameObject.GetComponent<TextMeshProUGUI>().text;
        levelSaver.SaveLevel();
        
        List<CardView> cardViews = getCardViewsFromArrangement(arrangementGenerator.ArrangementParent);
        Dictionary<int, Sprite> idSpritePairs = getIDSpritePairs(cardViews);
        cardViews[0].Selected();
        cardViews[1].Selected();
        yield return null;
        arrangementGenerator.ResetArrangement();
        
        //When
        levelLoader.LoadLevel();
        yield return null;
        yield return null;

        //Then
        spriteSameForSameID(idSpritePairs, arrangementGenerator.ArrangementParent);
        Assert.AreEqual(turn, turnDisplay.gameObject.GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual(score, scoreDisplay.gameObject.GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual(timeRemaining, timerDisplay.gameObject.GetComponent<TextMeshProUGUI>().text);
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
