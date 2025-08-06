using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public ArrangementGenerator Generator;
    public Score score;
    public TurnCounter turnCounter;
    public Timer timer;

    private void Start()
    {
        if (Generator == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementGenerator is not assigned.");
        }

        if(score == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Score cannot be null");
        }

        if(turnCounter == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Turn Counter cannot be null");
        }

        if(timer == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Timer cannot be null");
        }
    }

    public void LoadLevel()
    {
        LevelData data = DataLoader.LoadData();
        Generator.GenerateArrangement(data.Difficulty);
        score.setScore(data.Score);
        turnCounter.setTurnCounter(data.Turn);
        timer.SetTimer(data.TimeRemaining);

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
