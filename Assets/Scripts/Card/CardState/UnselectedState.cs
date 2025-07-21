internal class UnselectedState : CardState
{
    private CardStateMachine _cardStateMachine;
    private CardView _card;

    internal UnselectedState(CardStateMachine cardStateMachine, CardView card)
    {
        _cardStateMachine = cardStateMachine;
        _card = card;
    }

    public override void Selected()
    {
        _cardStateMachine.SetState(_cardStateMachine.SelectedState);
    }

    internal override void OnEnterState()
    {
        _card.FaceDownCard();
        _card.EnableInteraction();
    }
}
