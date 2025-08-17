using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameStateMachineTests
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
    public IEnumerator DifficultySelectionPanelNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator GamePanelNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        yield return null;
        
        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator ArrangementGeneratorNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator ScoreCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), new CardMatcher(), null, null, null, null);
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator TurnCounterCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), matcher,
            new Score(matcher), null, null, null);
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator TimerCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), matcher,
            new Score(matcher), new TurnCounter(matcher), null, null);
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator DifficultySettorCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), matcher,
            new Score(matcher), new TurnCounter(matcher), new Timer(gameManager), null);
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator AudioPlayerCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), matcher,
            new Score(matcher), new TurnCounter(matcher), 
            new Timer(gameManager), GameObject.FindObjectOfType<DifficultySettor>());
        yield return null;

        Assert.IsTrue(gameManager == null);
    }

    [UnityTest]
    public IEnumerator CardMatcherCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        CardMatcher matcher = new CardMatcher();
        GameObject g = new GameObject();
        GameStateMachine gameManager = g.AddComponent<GameStateMachine>();
        gameManager.DifficultySelectionPanel = new GameObject();
        gameManager.GamePanel = new GameObject();
        gameManager.AudioService = GameObject.FindObjectOfType<AudioPlayerService>();
        gameManager.SetDependencies(GameObject.FindObjectOfType<ArrangementGenerator>(), null,
            new Score(matcher), new TurnCounter(matcher),
            new Timer(gameManager), GameObject.FindObjectOfType<DifficultySettor>());
        yield return null;

        Assert.IsTrue(gameManager == null);
    }


    [UnityTest]
    public IEnumerator DifficultySelectionActiveInitially()
    {
        GameStateMachine stateMachine = GameObject.FindObjectOfType<GameStateMachine>();
        yield return null;
        Assert.IsTrue(stateMachine.DifficultySelectionPanel.activeSelf);
    }
}
