using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public Difficulty Difficulty;
    public List<CardMemeto> CardData;

    public LevelData(Difficulty difficulty, List<CardMemeto> cardData)
    {
        Difficulty = difficulty;
        CardData = cardData;
    }
}
