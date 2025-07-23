using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public Difficulty Difficulty;
    public Dictionary<int, CardMemeto> CardData;

    public LevelData(Difficulty difficulty, Dictionary<int, CardMemeto> cardData)
    {
        Difficulty = difficulty;
        CardData = cardData;
    }
}
