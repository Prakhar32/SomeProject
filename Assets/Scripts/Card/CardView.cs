using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour, ISelected
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;
    private Image _image;

    private CardStateMachine stateMachine;

    void Start()
    {
        checkDependencies();
        _image = GetComponent<Image>();

        InitializeStateMachine();
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

    private void InitializeStateMachine()
    {
        stateMachine = new CardStateMachine(this, CardMatcher);
    }

    internal void Evaluation(bool result)
    {
        stateMachine.Evaluation(result);
    }

    public void Selected()
    {
        stateMachine.Selected();
    }

    internal  void FaceUpCard() 
    { 
        _image.sprite = FaceUpSprite;
    }

    internal void FaceDownCard()
    {
        _image.sprite = FaceDownSprite;
    }

    internal void Destroy()
    {
        Destroy(gameObject);
    }
}