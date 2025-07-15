using System.Collections;
using UnityEngine;

internal class PauseBeforeDestructionState : CardState
{
    private CardStateMachine _stateMachine;
    private MonoBehaviour _mono;

    internal PauseBeforeDestructionState(CardStateMachine stateMachine, MonoBehaviour mono)
    {
        _stateMachine = stateMachine;
        _mono = mono;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _mono.StartCoroutine(PauseBeforeDestruction());
    }

    private IEnumerator PauseBeforeDestruction()
    {
        yield return new WaitForSeconds(Constants.TimeBeforeDestruction);
        _stateMachine.SetState(_stateMachine.DisabledState);
    }
}
