using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    private Transform _arrangementParent;
    private Score _score;
    private TurnCounter _turnCounter;
    private Timer _timer;

    private Difficulty _difficulty;
    
    public void SetDependencies(Transform ArrangementParent, Score score, TurnCounter turnCounter, Timer timer)
    {
        _arrangementParent = ArrangementParent;
        _score = score;
        _turnCounter = turnCounter;
        _timer = timer;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        _difficulty = difficulty;
    }

    void Start()
    {
        if(_arrangementParent == null)
        {
            Destroy(this);
            throw new MissingReferenceException("ArrangementParent cannot be null");
        }

        if(_score == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Score cannot be null");
        }

        if(_turnCounter == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Turn Counter cannot be null");
        }

        if(_timer == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Timer cannot be null");
        }
    }

    public void SaveLevel()
    {
        List<CardView> cardViews = getAllCards();
        Dictionary<int, CardMemeto> mementos = GetMementos(cardViews);
        DataSaver.SaveData(_difficulty, mementos, _score.getScore(), _turnCounter.getTurnCounter(), (int)_timer.GetTime());
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
        for(int i = 0; i < _arrangementParent.childCount; i++)
        {
            CardView cardView = _arrangementParent.GetChild(i).GetComponent<CardView>();
            cardViews.Add(cardView);
        }

        return cardViews;
    }
}
