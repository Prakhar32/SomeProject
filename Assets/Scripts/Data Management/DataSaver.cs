using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaver
{
    public static void SaveData(Difficulty difficulty, List<CardMemeto> cardData)
    {
        LevelData levelData = new LevelData(difficulty, cardData);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Constants.FilePath, FileMode.Create);
        formatter.Serialize(stream, levelData);
        stream.Close();
    }
}
