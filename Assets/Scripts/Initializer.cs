using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public ArrangementGenerator arrangementGenerator;
    public ScoreDisplay scoreDisplay;
    public TurnCounterDisplay turnCounterDisplay;
    public TimerDisplay timerDisplay;

    public LevelLoader levelLoader;
    public LevelSaver levelSaver;

    private CardMatcher _cardMatcher;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;

    private void Awake()
    {
        _cardMatcher = new CardMatcher();
        _score = new Score(_cardMatcher);
        _turnCounter = new TurnCounter(_cardMatcher);
        _timer = new Timer(timerDisplay);

        arrangementGenerator.CardMatcher = _cardMatcher;
        scoreDisplay.Score = _score;
        turnCounterDisplay.TurnCounter = _turnCounter;
        timerDisplay.Timer = _timer;

        initialiseLevelLoader();
        initialiseLevelSaver();
    }

    private void initialiseLevelLoader()
    {
        levelLoader.Generator = arrangementGenerator;
        levelLoader.score = _score;
        levelLoader.turnCounter = _turnCounter;
        levelLoader.timer = _timer;
    }

    private void initialiseLevelSaver()
    {
        levelSaver.ArrangementParent = arrangementGenerator.transform;
        levelSaver.Score = _score;
        levelSaver.TurnCounter = _turnCounter;
        levelSaver.Timer = _timer;
    }
}
