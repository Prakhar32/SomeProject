using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        _cardSetter = new CardSetter(CardSprites, CardMatcher);
    }

    public void GenerateArrangement(Difficulty difficulty)
    {
        int cellSize = Constants.dataMapper[difficulty].CellSize;
        LayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        int cellSpacing = Constants.dataMapper[difficulty].CellSpacing;
        LayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);

        int rows = Constants.dataMapper[difficulty].Rows;
        LayoutGroup.constraintCount = rows;

        int numberofElements = rows * Constants.dataMapper[difficulty].Columns;
        for (int i = 0; i < numberofElements; i++)
        {
            GameObject g = Instantiate(CardPrefab, ArrangementParent);
            CardView view = g.GetComponent<CardView>();
            _cardSetter.SetFaceUpSprites(view);
        }
    }
}
