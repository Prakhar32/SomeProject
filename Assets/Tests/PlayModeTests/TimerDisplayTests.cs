using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TimerDisplayTests
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
    public IEnumerator TMP_Missing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        TimerDisplay display = g.AddComponent<TimerDisplay>();
        yield return null;

        Assert.IsTrue(display == null);
    }

    [UnityTest]
    public IEnumerator TimerCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject(); 
        g.AddComponent<TextMeshProUGUI>();
        TimerDisplay display = g.AddComponent<TimerDisplay>();
        yield return null;

        Assert.IsTrue(display == null);
    }

    [UnityTest]
    public IEnumerator TimerCountsTimeAccurately()
    {
        //Given
        GameObject g = new GameObject();
        g.AddComponent<TextMeshProUGUI>();
        TimerDisplay display = g.AddComponent<TimerDisplay>();
        Timer timer = new Timer(display);
        display.SetTimer(timer);
        yield return null;

        //When
        timer.SetTimer(2);
        timer.StartTimer();
        yield return null;
        //get function and update are out of sync by like half a frame.
        //Ignoring it for now as there is no impact on user experience.

        int currentTime = (int)timer.GetTime();
        Assert.AreEqual(string.Format("Time : {0}", currentTime), g.GetComponent<TextMeshProUGUI>().text);
        yield return new WaitForSeconds(1f);
        
        //Then
        Assert.AreEqual(string.Format("Time : {0}", currentTime - 1), g.GetComponent<TextMeshProUGUI>().text);
    }
}
