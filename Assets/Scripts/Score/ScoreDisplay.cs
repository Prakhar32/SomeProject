using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public Score score;
    public TextMeshProUGUI Text;

    void Start()
    {
        if(score == null)
        {
            Destroy(this);
            throw new NullReferenceException("Score cannot be null");
        }

        if(Text == null)
        {
            Destroy(this);
            throw new NullReferenceException("Text field cannot be null");
        }

        score.SubscribeToScoreChange(displaytext);
    }

    private void displaytext()
    {
        Text.text = "Score : " + score.getScore();
    }
}
