using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public Difficulty Difficulty;
    public Dictionary<int, CardMemeto> CardData;
    public int Score;
    public int TimeRemaining;
    public int Turn;

    public LevelData(Difficulty difficulty, Dictionary<int, CardMemeto> cardData, int score, int turn, int time)
    {
        Difficulty = difficulty;
        CardData = cardData;
        Score = score;
        TimeRemaining = time;
        Turn = turn;
    }
}
