using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SystemTests
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
    public IEnumerator StartLevelOnDifficultySelection()
    {
        //Given
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();

        //When
        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;

        //Then
        Assert.IsTrue(arrangementGenerator.transform.childCount > 0);
    }
}
