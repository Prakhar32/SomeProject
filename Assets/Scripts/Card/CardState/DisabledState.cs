internal class DisabledState : CardState
{
    private CardView _card;
    internal DisabledState(CardView card)
    {
        _card = card;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _card.DisableView();
    }
}
