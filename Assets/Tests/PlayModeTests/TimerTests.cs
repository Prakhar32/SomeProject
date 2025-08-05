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
        timer.SetTimer(2f);
        timer.StartTimer();
        yield return new WaitForSeconds(2f);

        //Then
        Assert.IsTrue(timer.GetTime() == 0);
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
        timer.SetTimer(2f);
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
        timer.SetTimer(2f);

        timer.UnsubscribeFromTimeChange(action);
        timer.StartTimer();
        //When
        yield return new WaitForSeconds(1f);

        //Then
        Assert.IsFalse(timeChanged);
    }
}
