using UnityEngine.UI;

internal class SelectedState : CardState
{
    private Card _card;
    internal SelectedState(Card card)
    {
        _card = card;
    }

    public override void Selected()
    {
    }

    internal override void OnEnterState()
    {
        _card.FaceUpCard();
    }
}
