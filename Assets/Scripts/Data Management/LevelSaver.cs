using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    public Transform ArrangementParent;
    public Score Score;
    public TurnCounter TurnCounter;
    public Timer Timer;

    private Difficulty _difficulty;
    
    public void SetDifficulty(Difficulty difficulty)
    {
        _difficulty = difficulty;
    }

    void Start()
    {
        if(ArrangementParent == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementParent cannot be null");
        }

        if(Score == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Score cannot be null");
        }

        if(TurnCounter == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Turn Counter cannot be null");
        }

        if(Timer == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Timer cannot be null");
        }
    }

    public void SaveLevel()
    {
        List<CardView> cardViews = getAllCards();
        Dictionary<int, CardMemeto> mementos = GetMementos(cardViews);
        DataSaver.SaveData(_difficulty, mementos, Score.getScore(), TurnCounter.getTurnCounter(), (int)Timer.GetTime());
    }

    private Dictionary<int, CardMemeto> GetMementos(List<CardView> cardViews)
    {
        Dictionary<int, CardMemeto> mementos = new Dictionary<int, CardMemeto>();
        foreach (CardView cardView in cardViews)
        {
            CardMemeto memeto = cardView.SaveState();
            mementos.Add(cardView.ID, memeto);
        }

        return mementos;
    }

    private List<CardView> getAllCards()
    {
        List<CardView> cardViews = new List<CardView>();
        for(int i = 0; i < ArrangementParent.childCount; i++)
        {
            CardView cardView = ArrangementParent.GetChild(i).GetComponent<CardView>();
            cardViews.Add(cardView);
        }

        return cardViews;
    }
}
