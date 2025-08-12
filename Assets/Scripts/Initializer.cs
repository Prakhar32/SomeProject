using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public ArrangementGenerator arrangementGenerator;
    public ScoreDisplay scoreDisplay;
    public TurnCounterDisplay turnCounterDisplay;
    public TimerDisplay timerDisplay;
    public DifficultySettor difficultySettor;

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
        _timer = new Timer(timerDisplay, difficultySettor);

        arrangementGenerator.CardMatcher = _cardMatcher;
        scoreDisplay.SetScore(_score);
        turnCounterDisplay.SetTurnCounter(_turnCounter);
        timerDisplay.SetTimer(_timer);

        levelLoader.SetDependencies(arrangementGenerator, _score, _turnCounter, _timer);
        levelSaver.SetDependencies(arrangementGenerator.transform, _score, _turnCounter, _timer);
    }
}
