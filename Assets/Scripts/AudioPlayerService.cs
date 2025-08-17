using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioPlayerService : MonoBehaviour
{
    private AudioClip _matchClip;
    private AudioClip _incorrectMatchClip;
    private AudioClip _winClip;
    private AudioClip _gameOverClip;

    private AudioSource _audioSource;

    private void Start()
    {
        try 
        {
            getAudioDependencies();
            checkDependencies(); 
        }
        catch(Exception e)
        {
            Destroy(this);
            throw e;
        }
    }

    private void getAudioDependencies()
    {
        _audioSource = GetComponent<AudioSource>();
        _matchClip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.CorrectMatchAudio);
        _incorrectMatchClip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.IncorrectMatchAudio);
        _winClip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.WinAudio);
        _gameOverClip = Resources.Load<AudioClip>(Constants.PathToAudio + Constants.GameOverAudio);
    }

    private void checkDependencies()
    {
        if (_audioSource == null)
            throw new MissingComponentException("Audio Source not attached to gameobject");

        if(_matchClip == null)
            throw new MissingReferenceException("Match clip cannot be null");

        if(_incorrectMatchClip == null)
            throw new MissingReferenceException("Notmatchingclip cannot be null");

        if(_winClip == null)
            throw new MissingReferenceException("Win clip cannot be null");

        if(_gameOverClip == null)
            throw new MissingReferenceException("Game over clip cannot be null");
    }

    internal void PlayMatchedClip()
    {
        _audioSource.clip = _matchClip;
        _audioSource.Play();
    }

    internal void PlayIncorrectMatchClip()
    {
        _audioSource.clip = _incorrectMatchClip;
        _audioSource.Play();
    }

    internal void PlayWinClip()
    {
        _audioSource.clip = _winClip;
        _audioSource.Play();
    }

    internal void PlayGameOverClip()
    {
        _audioSource.clip = _gameOverClip;
        _audioSource.Play();
    }
}
