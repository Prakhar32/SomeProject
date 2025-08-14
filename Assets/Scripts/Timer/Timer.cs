using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private MonoBehaviour _mono;

    private UnityEvent _timeOverEvent;
    private UnityEvent _timerStartedEvent;
    private float _currentTime;

    public Timer(MonoBehaviour mono)
    {
        if (mono == null)
            throw new NullReferenceException("Monobehaviour cannot be null");

        _mono = mono;
        _timeOverEvent = new UnityEvent();
        _timerStartedEvent = new UnityEvent();
    }

    public void SetTimer(float time)
    {
        _currentTime = time;
        _mono.StopCoroutine(reduceTime());
    }

    public void StartTimer()
    {
        _mono.StartCoroutine(reduceTime());
        _timerStartedEvent.Invoke();
    }

    private IEnumerator reduceTime()
    {
        while (_currentTime > 0)
        {
            yield return null;
            _currentTime -= Time.deltaTime;
        }

        _timeOverEvent.Invoke();
        _currentTime = 0;
    }

    public float GetTime()
    {
        return _currentTime;
    }

    public void SubscibeToTimeOverEvent(UnityAction action)
    {
        if(action == null)
            throw new NullReferenceException("Action cannot be null");

        _timeOverEvent.AddListener(action);
    }

    public void UnsubscribeFromTimeOverEvent(UnityAction action)
    {
        _timeOverEvent.RemoveListener(action);
    }

    public void SubscribeToTimeStartEvent(UnityAction action)
    {
        if (action == null)
            throw new NullReferenceException("Action cannot be null");

        _timerStartedEvent.AddListener(action);
    }

    public void UnscubscribeFromTimeStartEvent(UnityAction action)
    {
        _timerStartedEvent.RemoveListener(action);
    }
}
