using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite FaceUpSprite;
    public Sprite FaceDownSprite;
    public CardMatcher CardMatcher;

    private Image _image;

    void Start()
    {
        checkDependencies();

        _image = GetComponent<Image>();
        _image.sprite = FaceDownSprite;
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

    internal void CardMatched()
    {
        Destroy(gameObject);
    }

    internal void CardNotmatching()
    {
        _image.sprite = FaceDownSprite;
    }

    public void Selected()
    {
        _image.sprite = FaceUpSprite;
        CardMatcher.CardSelected(this); 
    }
}
