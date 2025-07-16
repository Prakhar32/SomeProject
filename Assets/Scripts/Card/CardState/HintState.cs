using System.Collections;
using UnityEngine;

internal class HintState : CardState
{
    CardView _cardView;
    MonoBehaviour _mono;
    CardStateMachine _stateMachine;
    internal HintState(CardView cardView, MonoBehaviour mono, CardStateMachine stateMachine)
    {
        _cardView = cardView;
        _mono = mono;
        _stateMachine = stateMachine;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _cardView.DisableInteraction();
        _cardView.FaceUpCard();
        _mono.StartCoroutine(pauseForViewing());
    }

    private IEnumerator pauseForViewing()
    {
        yield return new WaitForSeconds(Constants.ViewTime);
        _stateMachine.SetState(_stateMachine.UnselectedState);
    }
}
