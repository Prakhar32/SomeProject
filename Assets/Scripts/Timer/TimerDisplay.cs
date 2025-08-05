using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI _timerText;
    public Timer Timer;

    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        if(_timerText == null)
        {
            Destroy(this);
            throw new MissingComponentException("TextMeshProUGUI is missing.");
        }

        if(Timer == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Timer is not assigned.");
        }

        Timer.SubscibeToTimeChange(displayTime);
    }

    private void displayTime()
    {
        _timerText.text = string.Format("Time Remaining : {0}", (int)Timer.GetTime());
    }
}

