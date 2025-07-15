internal class UnselectedState : CardState
{
    private CardStateMachine _cardStateMachine;
    private CardMatcher _matcher;
    private Card _card;

    internal UnselectedState(CardStateMachine cardStateMachine, CardMatcher cardMatcher, Card card)
    {
        _cardStateMachine = cardStateMachine;
        _matcher = cardMatcher;
        _card = card;
    }

    public override void Selected()
    {
        _matcher.CardSelected(_card);
        _cardStateMachine.SetState(_cardStateMachine.SelectedState);
    }

    internal override void OnEnterState()
    {
        _card.FaceDownCard();
    }
}
