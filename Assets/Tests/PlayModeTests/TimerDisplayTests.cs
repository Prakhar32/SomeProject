using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerDisplayTests
{
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
        display.Timer = new Timer(display);
        yield return null;

        //When
        display.Timer.SetTimer(5f);
        display.Timer.StartTimer();
        Assert.AreEqual("Time Remaining : 5", g.GetComponent<TextMeshProUGUI>().text);
        yield return new WaitForSeconds(1f);
        
        //Then
        Assert.AreEqual("Time Remaining : 4", g.GetComponent<TextMeshProUGUI>().text);
    }
}
