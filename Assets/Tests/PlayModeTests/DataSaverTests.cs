using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;

public class DataSaverTests
{
    [UnityTest]
    public IEnumerator SaveData()
    {
        //Given
        GameObject g = HelperMethods.GetCard(new CardMatcher());
        CardView cardView = g.GetComponent<CardView>();
        cardView.FaceUpSprite = HelperMethods.GetRandomSprite();
        yield return null;

        Dictionary<int, CardMemeto> mementos = new Dictionary<int, CardMemeto>();
        int rows = Constants.dataMapper[Difficulty.Easy].Rows;
        for (int i = 0; i < rows * Constants.dataMapper[Difficulty.Easy].Columns; i++)
            mementos.Add(i, cardView.SaveState());

        //When
        DataSaver.SaveData(Difficulty.Easy, mementos, 0, 1, Constants.TimeForDifficulty[Difficulty.Easy]);

        //Then
        Assert.IsTrue(File.Exists(Constants.FilePath));
    }
}
