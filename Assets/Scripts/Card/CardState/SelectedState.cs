using UnityEngine.UI;

internal class SelectedState : CardState
{
    private CardView _card;
    private CardStateMachine _cardStateMachine;
    private CardMatcher _cardMatcher;
    internal SelectedState(CardView card, CardStateMachine cardStateMachine, CardMatcher cardMatcher)
    {
        _card = card;
        _cardStateMachine = cardStateMachine;
        _cardMatcher = cardMatcher;

        _cardMatcher.SubscribeToSuccessfulMatch(CardMatched);
        _cardMatcher.SubscribeToUnsuccessfulMatch(CardNotMatching);
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _card.FaceUpCard();
        _cardMatcher.CardSelected(_card);
    }

    private void CardMatched()
    {
        _cardStateMachine.SetState(_cardStateMachine.PauseBeforeDestructionState);
    }

    private void CardNotMatching()
    {
        _cardStateMachine.SetState(_cardStateMachine.PauseBeforeResetState);
    }
}
