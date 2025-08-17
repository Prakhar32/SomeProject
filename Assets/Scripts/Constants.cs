using System.Collections.Generic;
using UnityEngine;

public class Constants 
{
    public const float TimeBeforeDestruction = 1.5f;
    public const float ResetTime = 1.0f;
    public const float ViewTime = 3.0f;

    public static Dictionary<Difficulty, LevelLayoutData> dataMapper = new Dictionary<Difficulty, LevelLayoutData>()
    {
        { Difficulty.Easy, new LevelLayoutData(2, 3, 200, 50) },
        { Difficulty.Medium, new LevelLayoutData(3, 4, 150, 25) },
        { Difficulty.Hard, new LevelLayoutData(4, 5, 110, 15) }
    };

    public static string FilePath = Application.persistentDataPath + "/level.fun";
    public const string PathToSprites = "Sprites/";

    public static Dictionary<Difficulty, int> TimeForDifficulty = new Dictionary<Difficulty, int>()
    {
        { Difficulty.Easy, 30 },
        { Difficulty.Medium, 50 },
        { Difficulty.Hard, 80 }
    };

    public const string GammeSceneName = "GameScene";

    public const string PathToAudio = "Audio/";
    public const string CorrectMatchAudio = "correct_choice";
    public const string IncorrectMatchAudio = "wrong_choice";
    public const string WinAudio = "win";
    public const string GameOverAudio = "gameover";
}

public enum Difficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class LevelLayoutData
{
    public int Rows;
    public int Columns;
    public int CellSize;
    public int CellSpacing;

    public LevelLayoutData(int rows, int columns, int cellSize, int cellSpacing)
    {
        Rows = rows;
        Columns = columns;
        CellSize = cellSize;
        CellSpacing = cellSpacing;
    }
}
