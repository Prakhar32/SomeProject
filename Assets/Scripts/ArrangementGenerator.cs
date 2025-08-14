using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ArrangementGenerator : MonoBehaviour
{
    public Transform ArrangementParent;
    public GameObject CardPrefab;
    public GridLayoutGroup LayoutGroup;
    public CardMatcher CardMatcher;
    public List<Sprite> CardSprites;
    public Sprite FaceDownSprite;

    private CardSetter _cardSetter;
    private ContentSizeFitter _contentSizeFitter;

    void Start()
    {
        if( ArrangementParent == null)
        {
            Destroy(this);
            throw new NullReferenceException("ArrangementParent cannot be null");
        }

        if(LayoutGroup == null)
        {
            Destroy(this);
            throw new NullReferenceException("LayoutGroup cannot be null");
        }

        if (CardPrefab == null)
        {
            Destroy(this);
            throw new NullReferenceException("Prefab cannot be null");
        }

        if(CardPrefab.GetComponent<CardView>() == null)
        {
            Destroy(this);
            throw new MissingComponentException("CardPrefab must be a card");
        }

        if(CardMatcher == null)
        {
            Destroy(this);
            throw new NullReferenceException("Card Matcher cannot be null");
        }

        if(FaceDownSprite == null)
        {
            Destroy(this);
            throw new NullReferenceException("FaceDownSprite cannot be null");
        }

        _contentSizeFitter = GetComponent<ContentSizeFitter>();
        if (_contentSizeFitter == null)
        {
            Destroy(this);
            throw new MissingComponentException("ContentSizeFitter not present");
        }

        _cardSetter = new CardSetter(CardSprites);
    }

    public void GenerateArrangement(Difficulty difficulty)
    {
        ResetArrangement();
        CardMatcher.ResetMatcher();
        setCellSize(difficulty);
        setCellSpacing(difficulty);

        int rows = Constants.dataMapper[difficulty].Rows;
        LayoutGroup.constraintCount = rows;
        int numberofElements = rows * Constants.dataMapper[difficulty].Columns;
        
        initialiseElements(numberofElements);
    }

    private void setCellSize(Difficulty difficulty)
    {
        int cellSize = Constants.dataMapper[difficulty].CellSize;
        LayoutGroup.cellSize = new Vector2(cellSize, cellSize);
    }

    private void setCellSpacing(Difficulty difficulty)
    {
        int cellSpacing = Constants.dataMapper[difficulty].CellSpacing;
        LayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);
    }

    private void initialiseElements(int numberofElements)
    {
        LayoutGroup.enabled = true;
        List<CardView> cards = new List<CardView>();
        for (int i = 0; i < numberofElements; i++)
        {
            GameObject g = Instantiate(CardPrefab, ArrangementParent);
            CardView view = g.GetComponent<CardView>();
            view.CardMatcher = CardMatcher;
            view.ID = i;
            view.FaceUpSprite = null;
            cards.Add(view);
        }

        _cardSetter.SetupCards(cards);
        _contentSizeFitter.enabled = true;
        StartCoroutine(disableAfterSomeTime());
    }

    public void ResetArrangement()
    {
        for(int i = 0; i < ArrangementParent.childCount; i++)
        {
            Destroy(ArrangementParent.GetChild(i).gameObject);
        }
    }

    private IEnumerator disableAfterSomeTime()
    {
        yield return null;
        _contentSizeFitter.enabled = false;
        LayoutGroup.enabled = false;
    }
}
