using UnityEngine;
using UnityEngine.UI;

internal class CardStateMachine
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;

    internal CardState SelectedState { get; private set; }
    internal CardState UnselectedState { get; private set; }
    internal CardState DisabledState { get; private set; }

    private CardState _hintState;
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
        DisabledState = new DisabledState(_card);
        _hintState = new HintState(_card, _card, this);
        _pauseBeforeDestructionState = new PauseBeforeDestructionState(this, _card);
        _pauseBeforeResetState = new PauseBeforeResetState(this, _card);

        _currentState = _hintState;
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

    internal CardMemeto SaveState()
    {
        return new CardMemeto(_currentState);
    }

    internal void LoadState(CardMemeto memeto)
    {
        _currentState = memeto.GetState();
        _currentState.OnEnterState();
    }
}
