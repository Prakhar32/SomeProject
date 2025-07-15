using System.Collections;
using UnityEngine;

internal class PauseBeforeDestructionState : CardState
{
    private CardView _card;
    private MonoBehaviour _mono;

    internal PauseBeforeDestructionState(CardView card, MonoBehaviour mono)
    {
        _card = card;
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
        _card.Destroy();
    }
}
