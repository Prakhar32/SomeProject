using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangementGenerator : MonoBehaviour
{
    public Transform ArrangementParent;
    public GameObject CardPrefab;
    public GridLayoutGroup LayoutGroup;

    void Start()
    {
        if( ArrangementParent == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementParent cannot be null");
        }

        if( CardPrefab == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Prefab cannot be null");
        }

        if(LayoutGroup == null)
        {
            Destroy(this);
            throw new MissingReferenceException("LayoutGroup cannot be null");
        }
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
            Instantiate(CardPrefab, ArrangementParent);
    }
}
