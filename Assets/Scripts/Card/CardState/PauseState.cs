
using System.Collections;
using UnityEngine;

internal class PauseState : CardState
{
    private Card _card;
    MonoBehaviour _mono;
    internal PauseState(Card card, MonoBehaviour mono)
    {
        _card = card;
        _mono = mono;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        if (_card.HasMatched)
            _mono.StartCoroutine(PauseBeforeDestruction());
        else
            _mono.StartCoroutine(PauseBeforeReset());
    }

    private IEnumerator PauseBeforeDestruction()
    {
        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        _card.Destroy();
    }

    private IEnumerator PauseBeforeReset()
    {
        yield return new WaitForSeconds(Constants.ResetTime);
        _card.SetState(_card.UnselectedState);
    }
}
