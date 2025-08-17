
using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public GameObject DifficultySelectionPanel;
    public GameObject GamePanel;
    public AudioPlayerService AudioService;
    
    private ArrangementGenerator _arrangementGenerator;
    private CardMatcher _cardMatcher;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;
    private DifficultySettor _difficultySettor;

    internal IGameState GameStartState;
    internal IGameState GamePlayState;
    internal IGameState GameWonState;
    internal IGameState GameOverState;

    private IGameState _currentState;

    public void SetDependencies(ArrangementGenerator arrangementGenerator, CardMatcher cardMatcher, Score score, 
        TurnCounter turnCounter, Timer timer, DifficultySettor difficultySettor)
    {
        _arrangementGenerator = arrangementGenerator;
        _cardMatcher = cardMatcher;
        _score = score;
        _turnCounter = turnCounter;
        _timer = timer;
        _difficultySettor = difficultySettor;
    }

    void Start()
    {
        try
        {
            checkDependencies();
        }
        catch(Exception e)
        {
            Destroy(this);
            throw e;
        }

        GameStartState = new GameStartState(DifficultySelectionPanel, _arrangementGenerator, _score, _turnCounter, 
            _difficultySettor, _timer, this, this);
        GamePlayState = new GameplayState(_cardMatcher, _score, _timer, this, _arrangementGenerator, AudioService, this);
        GameWonState = new GameWonState(AudioService, this, this);
        GameOverState = new GameOverState(AudioService, this, this);
        setState(GameStartState);
    }

    private void checkDependencies()
    {
        if (DifficultySelectionPanel == null)
            throw new MissingReferenceException("DifficultySelectionPanel is not assigned.");

        if (GamePanel == null)
            throw new MissingReferenceException("GamePanel is not assigned.");

        if(AudioService == null)
            throw new MissingReferenceException("AudioPlayerService has not been assigned.");

        if (_arrangementGenerator == null)
            throw new MissingReferenceException("ArrangementGenerator is not assigned.");

        if (_cardMatcher == null)
            throw new NullReferenceException("CardMatcher cannot be null");

        if (_score == null)
            throw new NullReferenceException("Score cannot be null");

        if (_turnCounter == null)
            throw new NullReferenceException("TurnCounter cannot be null");

        if (_timer == null)
            throw new NullReferenceException("Timer cannot be null");

        if (_difficultySettor == null)
            throw new NullReferenceException("DifficultySettor cannot be null");
    }

    internal void setState(IGameState gameState)
    {
        _currentState = gameState;
        _currentState.OnEnterState();
    }
}
