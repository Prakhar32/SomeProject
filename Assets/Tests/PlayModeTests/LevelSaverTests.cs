using NUnit.Framework;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LevelSaverTests
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
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        yield return null;
        
        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator ScoreNotNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        levelSaver.ArrangementParent = new GameObject().transform;
        yield return null;

        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator TurnCounterCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        levelSaver.ArrangementParent = new GameObject().transform;
        levelSaver.Score = new Score(new CardMatcher());
        yield return null;

        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator TimerCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        levelSaver.ArrangementParent = new GameObject().transform;
        levelSaver.Score = new Score(matcher);
        levelSaver.TurnCounter = new TurnCounter(matcher);

        yield return null;

        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator SaveSuccessful()
    {
        //Given
        File.Delete(Constants.FilePath);
        
        LevelSaver levelSaver = GameObject.FindObjectOfType<LevelSaver>();
        levelSaver.SetDifficulty(Difficulty.Easy);

        ArrangementGenerator arrangementGenerator =
            GameObject.FindGameObjectWithTag(Constants.ArrangementGeneratorTag).GetComponent<ArrangementGenerator>();
        levelSaver.ArrangementParent = arrangementGenerator.ArrangementParent;
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        //When
        levelSaver.SaveLevel();
        yield return null;

        //Then
       Assert.IsTrue(File.Exists(Constants.FilePath));
    }
}
