
using System.Collections;
using UnityEngine;

internal class PauseState : CardState
{
    private CardView _card;
    private MonoBehaviour _mono;
    private CardStateMachine _cardStateMachine;

    internal PauseState(CardStateMachine cardStateMachine, CardView card, MonoBehaviour mono)
    {
        _card = card;
        _mono = mono;
        _cardStateMachine = cardStateMachine;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        if (_cardStateMachine.HasMatched)
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
        _cardStateMachine.SetState(_cardStateMachine.UnselectedState);
    }
}
