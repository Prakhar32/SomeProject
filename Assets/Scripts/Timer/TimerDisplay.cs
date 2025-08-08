using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI _timerText;
    private Timer _timer;

    public void SetTimer(Timer timer) {  _timer = timer; }

    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        if(_timerText == null)
        {
            Destroy(this);
            throw new MissingComponentException("TextMeshProUGUI is missing.");
        }

        if(_timer == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Timer is not assigned.");
        }

        _timer.SubscibeToTimeChange(displayTime);
    }

    private void displayTime()
    {
        _timerText.text = string.Format("Time Remaining : {0}", (int)_timer.GetTime());
    }
}

