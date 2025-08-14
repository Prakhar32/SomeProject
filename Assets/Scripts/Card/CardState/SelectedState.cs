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
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _cardMatcher.SubscribeToSuccessfulMatch(CardMatched);
        _cardMatcher.SubscribeToUnsuccessfulMatch(CardNotMatching);
        _card.FaceUpCard();
        _cardMatcher.CardSelected(_card);
    }

    private void CardMatched()
    {
        _cardMatcher.UnsubscribeToSuccessfulMatch(CardMatched);
        _cardMatcher.UnsubscribeToUnsuccessfulMatch(CardNotMatching);
        _cardStateMachine.SetState(_cardStateMachine.PauseBeforeDestructionState);
    }

    private void CardNotMatching()
    {
        _cardMatcher.UnsubscribeToSuccessfulMatch(CardMatched);
        _cardMatcher.UnsubscribeToUnsuccessfulMatch(CardNotMatching);
        _cardStateMachine.SetState(_cardStateMachine.PauseBeforeResetState);
    }
}
