using UnityEngine;
using UnityEngine.UI;

internal class CardStateMachine
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;

    internal CardState SelectedState { get; private set; }
    internal CardState UnselectedState { get; private set; }

    private CardState _pauseState;
    private CardState _currentState;
    private CardView _card;
    internal bool HasMatched { get; private set; } = false;

    internal CardStateMachine(CardView card, CardMatcher cardMatcher)
    {
        _card = card;
        CardMatcher = cardMatcher;
        InitializeStates();
    }

    private void InitializeStates()
    {
        UnselectedState = new UnselectedState(this, CardMatcher, _card);
        SelectedState = new SelectedState(_card);
        _pauseState = new PauseState(this, _card, _card);
        _currentState = UnselectedState;
        _currentState.OnEnterState();
    }

    public void Selected()
    {
        _currentState.Selected();
    }

    internal void SetState(CardState cardState)
    {
        _currentState = cardState;
        _currentState.OnEnterState();
    }

    internal void Evaluation(bool result)
    {
        HasMatched = result;
        SetState(_pauseState);
    }
}
