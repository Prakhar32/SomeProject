using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private Score _score;
    private TextMeshProUGUI _text;

    public void SetScore(Score score) {  _score = score; }

    void Start()
    {
        if(_score == null)
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

        _score.SubscribeToScoreChange(displaytext);
    }

    private void displaytext()
    {
        _text.text = "Score : " + _score.getScore();
    }
}
