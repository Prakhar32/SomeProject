using System.Collections;
using UnityEngine;

public class GameWonState : IGameState
{
    private AudioPlayerService _audioService;
    private MonoBehaviour _mono;
    private GameStateMachine _stateMachine;

    internal GameWonState(AudioPlayerService audioService, MonoBehaviour mono, GameStateMachine stateMachine)
    {
        _audioService = audioService;
        _mono = mono;
        _stateMachine = stateMachine;
    }

    public void OnEnterState()
    {
        _audioService.PlayWinClip();
        _mono.StartCoroutine(waitForWinAudioToEnd());
    }

    private IEnumerator waitForWinAudioToEnd()
    {
        AudioClip clip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.WinAudio);
        yield return new WaitForSeconds(clip.length);
        OnExitState();
    }

    public void OnExitState()
    {
        _stateMachine.setState(_stateMachine.GameStartState);
    }
}
