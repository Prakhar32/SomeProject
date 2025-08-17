using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IGameState
{
    private CardMatcher _cardMatcher;
    private Score _score;
    private Timer _timer;
    private ArrangementGenerator _generator;
    private AudioPlayerService _audioService;
    private MonoBehaviour _mono;
    private GameStateMachine _stateMachine;

    internal GameplayState(CardMatcher cardMatcher, Score score, Timer timer, GameStateMachine stateMachine,
        ArrangementGenerator arrangementGenerator, AudioPlayerService audioService, MonoBehaviour mono)
    {
        _cardMatcher = cardMatcher;
        _score = score;
        _timer = timer;
        _generator = arrangementGenerator;
        _audioService = audioService;
        _mono = mono;
        _stateMachine = stateMachine;
    }

    public void OnEnterState()
    {
        _cardMatcher.SubscribeToSuccessfulMatch(successfulMatch);
        _cardMatcher.SubscribeToUnsuccessfulMatch(playincorrectAudio);
        _timer.SubscibeToTimeOverEvent(gameOver);
    }

    private void successfulMatch()
    {
        int cardsLeft = getCardLeft();
        if (cardsLeft == 0)
            gameWon();
        else if (cardsLeft == 2)
            _mono.StartCoroutine(waitBeforeEvaluating());
        else
            playCorrectAudio();
    }

    //To avoid race conditioon
    private IEnumerator waitBeforeEvaluating()
    {
        yield return null;
        if (getCardLeft() == 0)
            gameWon();
        else
            playCorrectAudio();
    }

    private void gameWon()
    {
        OnExitState();
        _stateMachine.setState(_stateMachine.GameWonState);
    }

    private void gameOver()
    {
        OnExitState();
        _stateMachine.setState(_stateMachine.GameOverState);
    }

    private int getCardLeft()
    {
        int numberofCards = _generator.transform.childCount;
        int cardsLeft = numberofCards - _score.getScore() * 2;
        return cardsLeft;
    }

    public void OnExitState()
    {
        _cardMatcher.UnsubscribefromUnsuccessfulMatch(successfulMatch);
        _cardMatcher.UnsubscribefromUnsuccessfulMatch(playincorrectAudio);
        _timer.UnscubscribeFromTimeStartEvent(gameOver);
    }

    private void playCorrectAudio()
    {
        _audioService.PlayMatchedClip();
    }

    private void playincorrectAudio()
    {
        _audioService.PlayIncorrectMatchClip();
    }
}
