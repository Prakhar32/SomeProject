using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardView : MonoBehaviour, ISelected
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;
    public int ID;

    private Image _image;
    private Button _button;

    private CardStateMachine stateMachine;

    void Start()
    {
        checkDependencies();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

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

        if (CardMatcher == null)
        {
            Destroy(this);
            throw new MissingReferenceException("CardMatcher is not assigned in the Card component.");
        }

        if (GetComponent<Image>() == null)
        {
            Destroy(this);
            throw new MissingComponentException("Image component is missing on the GameObject.");
        }

        if(GetComponent<Button>() == null)
        {
            Destroy(this);
            throw new MissingComponentException("Button component is missing on the GameObject.");
        }
    }

    private void InitializeStateMachine()
    {
        stateMachine = new CardStateMachine(this, CardMatcher);
    }

    public void Selected()
    {
        stateMachine.Selected();
    }

    internal void FaceUpCard() 
    { 
        _image.sprite = FaceUpSprite;
    }

    internal void FaceDownCard()
    {
        _image.sprite = FaceDownSprite;
    }
    internal void EnableInteraction()
    {
        _button.interactable = true;
    }

    internal void DisableInteraction()
    {
        _button.interactable = false;
    }

    internal void DisableView()
    {
        gameObject.SetActive(false);
    }

    public CardMemeto SaveState()
    {
        return stateMachine.SaveState();
    }

    public void LoadState(CardMemeto memeto)
    {
        FaceUpSprite = Resources.Load<SpriteAtlas>("Sprites/Atlas").GetSprite(memeto.GetCardSpriteName());
        stateMachine.LoadState(memeto);
    }
}