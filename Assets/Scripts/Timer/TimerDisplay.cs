using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Timer Timer;

    void Start()
    {
        if(timerText == null)
        {
            Destroy(this);
            throw new MissingReferenceException("TextMeshProUGUI is not assigned.");
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
        timerText.text = string.Format("Time : {0}", (int)Timer.GetTime());
    }
}

