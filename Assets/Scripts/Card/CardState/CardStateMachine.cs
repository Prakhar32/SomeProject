using UnityEngine;
using UnityEngine.UI;

internal class CardStateMachine
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;

    internal CardState SelectedState { get; private set; }
    internal CardState UnselectedState { get; private set; }

    private CardState _pauseBeforeDestructionState;
    private CardState _pauseBeforeResetState;
    private CardState _currentState;
    private CardView _card;

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
        _pauseBeforeDestructionState = new PauseBeforeDestructionState(_card, _card);
        _pauseBeforeResetState = new PauseBeforeResetState(this, _card);

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
        if(result)
            _currentState = _pauseBeforeDestructionState;
        else
            _currentState = _pauseBeforeResetState;
        _currentState.OnEnterState();
    }
}
