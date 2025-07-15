using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ISelected
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;
    private Image _image;

    internal CardState SelectedState;
    internal CardState UnselectedState;
    
    private CardState _pauseState;
    private CardState _currentState;

    internal bool HasMatched {get;private set;} = false;

    void Start()
    {
        checkDependencies();
        _image = GetComponent<Image>();

        InitializeStates();
    }

    private void checkDependencies()
    {
        if (FaceUpSprite == null)
        {
            Destroy(this);
            throw new MissingReferenceException("FaceUpSprite is not assigned in the Card component.");
        }

        if (FaceDownSprite == null)
        {
            Destroy(this);
            throw new MissingReferenceException("FaceDownSprite is not assigned in the Card component.");
        }

        if (GetComponent<Image>() == null)
        {
            Destroy(this);
            throw new MissingComponentException("Image component is missing on the GameObject.");
        }

        if (CardMatcher == null)
        {
            Destroy(this);
            throw new MissingReferenceException("CardMatcher is not assigned in the Card component.");
        }
    }

    private void InitializeStates()
    {
        UnselectedState = new UnselectedState(this);
        SelectedState = new SelectedState(this);
        _pauseState = new PauseState(this, this);
        _currentState = UnselectedState;
        _currentState.OnEnterState();
    }

    internal void Evaluation(bool result)
    {
        HasMatched = result;
        SetState(_pauseState);
    }

    public void Selected()
    {
        _currentState.Selected();
        CardMatcher.CardSelected(this);
    }

    internal  void FaceUpCard() 
    { 
        _image.sprite = FaceUpSprite;
    }

    internal void FaceDownCard()
    {
        _image.sprite = FaceDownSprite;
    }

    internal void SetState(CardState cardState)
    {
        _currentState = cardState;
        _currentState.OnEnterState();
    }

    internal void Destroy()
    {
        Destroy(gameObject);
    }
}