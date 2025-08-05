using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public Score Score;
    private TextMeshProUGUI _text;

    void Start()
    {
        if(Score == null)
        {
            Destroy(this);
            throw new NullReferenceException("Score cannot be null");
        }

        _text = GetComponent<TextMeshProUGUI>();
        if(_text == null)
        {
            Destroy(this);
            throw new NullReferenceException("Text field cannot be null");
        }

        Score.SubscribeToScoreChange(displaytext);
    }

    private void displaytext()
    {
        _text.text = "Score : " + Score.getScore();
    }
}
