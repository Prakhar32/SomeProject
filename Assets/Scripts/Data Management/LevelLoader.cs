using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public ArrangementGenerator Generator;

    private void Start()
    {
        if (Generator == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementGenerator is not assigned.");
        }
    }

    public void LoadLevel()
    {
        LevelData data = DataLoader.LoadData();
        Generator.GenerateArrangement(data.Difficulty);
        StartCoroutine(assignData(data.CardData));
    }

    private IEnumerator assignData(Dictionary<int, CardMemeto> cardStates)
    {
        yield return null;
        for(int i = 0; i < Generator.ArrangementParent.childCount; i++)
        {
            CardView cardView = Generator.ArrangementParent.GetChild(i).GetComponent<CardView>();
            cardView.LoadState(cardStates[cardView.ID]);
        }
    }
}
