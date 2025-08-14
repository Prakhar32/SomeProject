using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TimerTests
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
    public IEnumerator CanSetTimer()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);

        //When
        timer.SetTimer(2);
        float currentTime = timer.GetTime();
        timer.StartTimer();
        yield return new WaitForSeconds(2f);

        //Then
        Assert.AreEqual(timer.GetTime(), currentTime - 2, 0.017f);
    }

    [UnityTest]
    public IEnumerator NullCannotSubscribeToTimeOverEvent()
    {
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer); 
        yield return null;

        Assert.Throws<NullReferenceException>(() => timer.SubscibeToTimeOverEvent(null));
    }

    [UnityTest]
    public IEnumerator NullCannotUnsubscribeFromTimeOverEvent()
    {
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        Assert.Throws<NullReferenceException>(() => timer.UnsubscribeFromTimeOverEvent(null));
    }

    [UnityTest]
    public IEnumerator CanSubscribeToTimeOverEvent()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscibeToTimeOverEvent(action);
        timer.SetTimer(2);
        timer.StartTimer();

        //When
        yield return new WaitForSeconds(2f);
        
        //Then
        Assert.IsTrue(timeChanged);
    }

    [UnityTest]
    public IEnumerator NoTimeOverEventBeforeTimeis0()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscibeToTimeOverEvent(action);
        timer.SetTimer(2);
        timer.StartTimer();

        //When
        yield return new WaitForSeconds(1f);

        //Then
        Assert.IsFalse(timeChanged);
    }

    [UnityTest]
    public IEnumerator CanUnsubscribeToTimeOver() 
    {   
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscibeToTimeOverEvent(action);
        timer.SetTimer(2);

        timer.UnsubscribeFromTimeOverEvent(action);
        timer.StartTimer();
        //When
        yield return new WaitForSeconds(2f);

        //Then
        Assert.IsFalse(timeChanged);
    }

    [UnityTest]
    public IEnumerator CanSubscribeToTimerStartEvent()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscribeToTimeStartEvent(action);
        timer.SetTimer(2);
        timer.StartTimer();

        //When
        yield return new WaitForSeconds(2f);

        //Then
        Assert.IsTrue(timeChanged);
    }

    [UnityTest]
    public IEnumerator NullCannotSubscribeToTimerStartEvent()
    {
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        Assert.Throws<NullReferenceException>(() => timer.SubscribeToTimeStartEvent(null));
    }

    [UnityTest]
    public IEnumerator UnsubscribeToTimerStartEvent()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscribeToTimeStartEvent(action);
        timer.SetTimer(2);

        timer.UnscubscribeFromTimeStartEvent(action);
        timer.StartTimer();
        //When
        yield return new WaitForSeconds(2f);

        //Then
        Assert.IsFalse(timeChanged);
    }

    [UnityTest]
    public IEnumerator NullCannotUnsubscribeFromTimeStartEvent()
    {
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.SetTimer(timer);
        yield return null;

        Assert.Throws<NullReferenceException>(() => timer.UnscubscribeFromTimeStartEvent(null));
    }
}
