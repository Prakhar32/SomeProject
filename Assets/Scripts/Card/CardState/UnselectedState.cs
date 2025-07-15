internal class UnselectedState : CardState
{
    private Card _card;
    internal UnselectedState(Card card)
    {
        _card = card;
    }

    public override void Selected()
    {
        _card.SetState(_card.SelectedState);
    }

    internal override void OnEnterState()
    {
        _card.FaceDownCard();
    }
}
