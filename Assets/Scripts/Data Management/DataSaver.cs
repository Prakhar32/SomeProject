using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaver
{
    public static void SaveData(Difficulty difficulty, Dictionary<int, CardMemeto> cardData, int score, int turn, int time)
    {
        LevelData levelData = new LevelData(difficulty, cardData, score, turn, time);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Constants.FilePath, FileMode.Create);
        formatter.Serialize(stream, levelData);
        stream.Close();
    }
}
