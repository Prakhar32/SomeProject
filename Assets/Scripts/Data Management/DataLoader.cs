using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataLoader 
{
    public static LevelData LoadData()
    {
        string filePath = Constants.FilePath;
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The specified file does not exist.", filePath);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        LevelData levelData = formatter.Deserialize(fileStream) as LevelData;
        fileStream.Close();
        return levelData;
    }
}
