using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _currentTime;
    private MonoBehaviour _mono;
    private UnityEvent _timeChangeEvent;

    public Timer(MonoBehaviour mono)
    {
        if (mono == null)
            throw new NullReferenceException("Monobehaviour cannot be null");

        _mono = mono;
        _timeChangeEvent = new UnityEvent();
    }

    public void SetTimer(float time)
    {
        _currentTime = time;
        _mono.StopCoroutine(reduceTime());
    }

    public void StartTimer()
    {
        _mono.StartCoroutine(reduceTime());
    }

    private IEnumerator reduceTime()
    {
        _timeChangeEvent.Invoke();
        
        while (_currentTime > 0)
        {
            yield return null;
            _timeChangeEvent.Invoke();
            _currentTime -= Time.deltaTime;
        }

        _currentTime = 0;
    }

    public float GetTime()
    {
        return _currentTime;
    }

    public void SubscibeToTimeChange(UnityAction action)
    {
        if(action == null)
            throw new NullReferenceException("Action cannot be null");

        _timeChangeEvent.AddListener(action);
    }

    public void UnsubscribeFromTimeChange(UnityAction action)
    {
        _timeChangeEvent.RemoveListener(action);
    }
}
