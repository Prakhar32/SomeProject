using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

public class TimerTests
{
    [UnityTest]
    public IEnumerator CanSetTimer()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();

        Timer timer = new Timer(timerDisplay);
        timerDisplay.Timer = timer;

        //When
        timer.SetDifficulty(Difficulty.Easy);
        float currentTime = timer.GetTime();
        timer.StartTimer();
        yield return new WaitForSeconds(2f);

        //Then
        Assert.AreEqual(timer.GetTime(), currentTime - 2, 0.001f);
    }

    [UnityTest]
    public IEnumerator NullCannotSubscribeToTimeChange()
    {
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        
        Timer timer = new Timer(timerDisplay);
        timerDisplay.Timer = timer; 
        yield return null;

        Assert.Throws<NullReferenceException>(() => timer.SubscibeToTimeChange(null));
    }

    [UnityTest]
    public IEnumerator CanSubscribeToTimeChange()
    {
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay); 
        timerDisplay.Timer = timer;
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscibeToTimeChange(action);
        timer.SetDifficulty(Difficulty.Easy);
        timer.StartTimer();

        //When
        yield return new WaitForSeconds(1f);
        
        //Then
        Assert.IsTrue(timeChanged);
    }

    [UnityTest]
    public IEnumerator CanUnsubscribeToTimeChange() 
    {   
        //Given
        GameObject gameObject = new GameObject();
        TimerDisplay timerDisplay = gameObject.AddComponent<TimerDisplay>();
        gameObject.AddComponent<TextMeshProUGUI>();
        Timer timer = new Timer(timerDisplay);
        timerDisplay.Timer = timer;
        yield return null;

        bool timeChanged = false;
        UnityAction action = () => timeChanged = true;
        timer.SubscibeToTimeChange(action);
        timer.SetDifficulty(Difficulty.Easy);

        timer.UnsubscribeFromTimeChange(action);
        timer.StartTimer();
        //When
        yield return new WaitForSeconds(1f);

        //Then
        Assert.IsFalse(timeChanged);
    }
}
