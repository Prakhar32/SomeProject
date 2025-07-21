using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class CardStateMachine
{
    public CardMatcher CardMatcher;

    internal CardState SelectedState { get; private set; }
    internal CardState UnselectedState { get; private set; }
    internal CardState DisabledState { get; private set; }
    internal CardState PauseBeforeDestructionState { get; private set; }
    internal CardState PauseBeforeResetState { get; private set; }


    private CardState _hintState;
    private CardState _currentState;
    private CardView _card;

    Dictionary<string, CardState> _states = new Dictionary<string, CardState>();

    internal CardStateMachine(CardView card, CardMatcher cardMatcher)
    {
        _card = card;
        CardMatcher = cardMatcher;
        InitializeStates();
    }

    private void InitializeStates()
    {
        UnselectedState = new UnselectedState(this, _card);
        SelectedState = new SelectedState(_card, this, CardMatcher);
        DisabledState = new DisabledState(_card);
        _hintState = new HintState(_card, _card, this);
        PauseBeforeDestructionState = new PauseBeforeDestructionState(this, _card);
        PauseBeforeResetState = new PauseBeforeResetState(this, _card);

        _currentState = _hintState;
        _currentState.OnEnterState();

        _states.Add(UnselectedState.GetType().ToString(), UnselectedState);
        _states.Add(SelectedState.GetType().ToString(), SelectedState);
        _states.Add(DisabledState.GetType().ToString(), DisabledState);
        _states.Add(_hintState.GetType().ToString(), _hintState);
        _states.Add(PauseBeforeDestructionState.GetType().ToString(), PauseBeforeDestructionState);
        _states.Add(PauseBeforeResetState.GetType().ToString(), PauseBeforeResetState);
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

    internal CardMemeto SaveState()
    {
        return new CardMemeto(_currentState.GetType().ToString(), _card.FaceUpSprite.name);
    }

    internal void LoadState(CardMemeto memeto)
    {
        _currentState = _states[memeto.GetState()];
        _currentState.OnEnterState();
    }
}
