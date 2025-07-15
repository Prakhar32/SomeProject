using UnityEngine.UI;

internal class SelectedState : CardState
{
    private CardView _card;
    internal SelectedState(CardView card)
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
