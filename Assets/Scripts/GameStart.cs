using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private ArrangementGenerator _arrangementGenerator;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;
    private DifficultySettor _difficultySettor;

    internal void setDependency(ArrangementGenerator arrangementGenerator, Score score, 
        TurnCounter turnCounter, DifficultySettor difficultySettor, Timer timer)
    {
        _arrangementGenerator = arrangementGenerator;
        _score = score;
        _turnCounter = turnCounter;
        _difficultySettor = difficultySettor;
        _timer = timer;
    }

    private void checkDependency()
    {
        if (_arrangementGenerator == null)
        {
            Destroy(this);
            throw new NullReferenceException("ArrangementGenerator cannot be null");
        }

        if (_score == null)
        {
            Destroy(this);
            throw new NullReferenceException("Score cannot be null");
        }

        if (_turnCounter == null)
        {
            Destroy(this);
            throw new NullReferenceException("TurnCounter cannot be null");
        }

        if (_difficultySettor == null)
        {
            Destroy(this);
            throw new NullReferenceException("DifficultySettor cannot be null");
        }

        if (_timer == null)
        {
            Destroy(this);
            throw new NullReferenceException("Timer cannot be null");
        }
    }

    private void Start()
    {
        checkDependency();
        _difficultySettor.SubscribeToDifficultyChange(startGame);
    }

    private void startGame()
    {
        _arrangementGenerator.GenerateArrangement(_difficultySettor.GetDifficulty());
        _score.setScore(0);
        _turnCounter.setTurnCounter(1);
        StartCoroutine(startTimerAfterView());
        _difficultySettor.UnsubscribeFromDifficultyChange(startGame);
    }

    private IEnumerator startTimerAfterView()
    {
        yield return new WaitForSeconds(Constants.ViewTime);
        _timer.SetTimer(Constants.TimeForDifficulty[_difficultySettor.GetDifficulty()]);
        _timer.StartTimer();
    }
}
