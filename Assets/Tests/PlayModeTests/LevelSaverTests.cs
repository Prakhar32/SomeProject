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
        levelSaver.SetDependencies(new GameObject().transform, null, null, null);
        yield return null;

        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator TurnCounterCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        levelSaver.SetDependencies(new GameObject().transform, new Score(new CardMatcher()), null, null);
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
        levelSaver.SetDependencies(new GameObject().transform, new Score(matcher), new TurnCounter(matcher), null);

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
