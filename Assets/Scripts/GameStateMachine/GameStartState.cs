using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : IGameState
{
    private GameObject _difficultSelectionPanel;
    private ArrangementGenerator _arrangementGenerator;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;
    private DifficultySettor _difficultySettor;
    private MonoBehaviour _mono;
    private GameStateMachine _gameStateMachine;

    internal GameStartState(GameObject DifficultSelectionPanel, ArrangementGenerator arrangementGenerator, 
        Score score, TurnCounter turnCounter, DifficultySettor difficultySettor, 
        Timer timer, MonoBehaviour mono, GameStateMachine stateMachine)
    {
        _arrangementGenerator = arrangementGenerator;
        _score = score;
        _turnCounter = turnCounter;
        _difficultySettor = difficultySettor;
        _timer = timer;
        _mono = mono;
        _gameStateMachine = stateMachine;
        _difficultSelectionPanel = DifficultSelectionPanel;
    }

    public void OnEnterState()
    {
        _difficultSelectionPanel.SetActive(true);
        _difficultySettor.SubscribeToDifficultyChange(startGame);
    }

    private void startGame()
    {
        _difficultSelectionPanel.SetActive(false);
        _arrangementGenerator.GenerateArrangement(_difficultySettor.GetDifficulty());
        _score.setScore(0);
        _turnCounter.setTurnCounter(1);
        _difficultySettor.UnsubscribeFromDifficultyChange(startGame);
        _mono.StartCoroutine(startTimerAfterView());
    }

    private IEnumerator startTimerAfterView()
    {
        yield return new WaitForSeconds(Constants.ViewTime);
        OnExitState();
    }

    public void OnExitState()
    {
        _timer.SetTimer(Constants.TimeForDifficulty[_difficultySettor.GetDifficulty()]);
        _timer.StartTimer();
        _gameStateMachine.setState(_gameStateMachine.GamePlayState);
    }
}
