using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PauseBeforeResetState : CardState
{
    private MonoBehaviour _mono;
    private CardStateMachine _cardStateMachine;

    internal PauseBeforeResetState(CardStateMachine cardStateMachine, MonoBehaviour mono)
    {
        _mono = mono;
        _cardStateMachine = cardStateMachine;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _mono.StartCoroutine(PauseBeforeReset());
    }

    private IEnumerator PauseBeforeReset()
    {
        yield return new WaitForSeconds(Constants.ResetTime);
        _cardStateMachine.SetState(_cardStateMachine.UnselectedState);
    }
}
