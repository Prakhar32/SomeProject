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
        GameObject g = new GameObject();
        CardView cardView = HelperMethods.ConvertGameobjectIntoCard(g, new CardMatcher());
        cardView.FaceUpSprite = HelperMethods.GetRandomSprite();
        yield return null;

        Dictionary<int, CardMemeto> mementos = new Dictionary<int, CardMemeto>();
        int rows = Constants.dataMapper[Difficulty.Easy].Rows;
        for (int i = 0; i < rows * Constants.dataMapper[Difficulty.Easy].Columns; i++)
            mementos.Add(i, cardView.SaveState());

        //When
        DataSaver.SaveData(Difficulty.Easy, mementos);

        //Then
        Assert.IsTrue(File.Exists(Constants.FilePath));
    }
}
