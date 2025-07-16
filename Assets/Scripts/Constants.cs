using System.Collections.Generic;

public class Constants 
{
    public const float TimeBeforeDestruction = 1.5f;
    public const float ResetTime = 1.0f;
    public const float ViewTime = 3.0f;

    public static Dictionary<Difficulty, LevelData> dataMapper = new Dictionary<Difficulty, LevelData>()
    {
        { Difficulty.Easy, new LevelData(2, 3, 200, 50) },
        { Difficulty.Medium, new LevelData(3, 4, 150, 25) },
        { Difficulty.Hard, new LevelData(4, 5, 110, 15) }
    };
}

public enum Difficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class LevelData
{
    public int Rows;
    public int Columns;
    public int CellSize;
    public int CellSpacing;

    public LevelData(int rows, int columns, int cellSize, int cellSpacing)
    {
        Rows = rows;
        Columns = columns;
        CellSize = cellSize;
        CellSpacing = cellSpacing;
    }
}