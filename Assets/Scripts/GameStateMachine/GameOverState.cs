using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IGameState
{
    private AudioPlayerService _audioService;
    private MonoBehaviour _mono;
    private GameStateMachine _stateMachine;

    internal GameOverState(AudioPlayerService audioService, MonoBehaviour mono, GameStateMachine stateMachine)
    {
        _audioService = audioService;
        _mono = mono;
        _stateMachine = stateMachine;
    }

    public void OnEnterState()
    {
        _audioService.PlayGameOverClip();
        _mono.StartCoroutine(waitForGameOverAudioToEnd());
    }

    private IEnumerator waitForGameOverAudioToEnd()
    {
        AudioClip clip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.GameOverAudio);
        yield return new WaitForSeconds(clip.length);
        OnExitState();
    }

    public void OnExitState()
    {
        _stateMachine.setState(_stateMachine.GameStartState);
    }
}
