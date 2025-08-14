using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private ArrangementGenerator _generator;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;
    private DifficultySettor _difficultySettor;

    public void SetDependencies(ArrangementGenerator Generator, Score score, 
        TurnCounter turnCounter, Timer timer, DifficultySettor difficultySettor)
    {
        _generator = Generator;
        _score = score;
        _turnCounter = turnCounter;
        _timer = timer;
        _difficultySettor = difficultySettor;
    }

    private void Start()
    {
        checkDependencies();
    }

    private void checkDependencies()
    {
        if (_generator == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementGenerator is not assigned.");
        }

        if (_score == null)
        {
            Destroy(this);
            throw new NullReferenceException("Score cannot be null");
        }

        if (_turnCounter == null)
        {
            Destroy(this);
            throw new NullReferenceException("Turn Counter cannot be null");
        }

        if (_timer == null)
        {
            Destroy(this);
            throw new NullReferenceException("Timer cannot be null");
        }

        if (_difficultySettor == null)
        {
            Destroy(this);
            throw new NullReferenceException("DifficultySettor cannot be null");
        }
    }

    public void LoadLevel()
    {
        LevelData data = DataLoader.LoadData();
        _generator.GenerateArrangement(data.Difficulty);
        _score.setScore(data.Score);
        _turnCounter.setTurnCounter(data.Turn);
        _timer.SetTimer(data.TimeRemaining);
        _difficultySettor.SetDifficulty((int)data.Difficulty);

        StartCoroutine(assignData(data.CardData));
    }

    private IEnumerator assignData(Dictionary<int, CardMemeto> cardStates)
    {
        yield return null;
        for(int i = 0; i < _generator.ArrangementParent.childCount; i++)
        {
            CardView cardView = _generator.ArrangementParent.GetChild(i).GetComponent<CardView>();
            cardView.LoadState(cardStates[cardView.ID]);
        }
    }
}
