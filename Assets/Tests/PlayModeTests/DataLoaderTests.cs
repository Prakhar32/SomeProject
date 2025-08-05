using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class DataLoaderTests
{
    [Test]
    public void FileNotFound()
    {
        if(!System.IO.File.Exists(Constants.FilePath))
            Assert.Throws(typeof(System.IO.FileNotFoundException), () => DataLoader.LoadData());
        Assert.Pass();
    }

    [UnityTest]
    public IEnumerator DataLoaded()
    {
        //Given
        GameObject g = HelperMethods.GetCard(new CardMatcher());
        CardView cardView = g.GetComponent<CardView>();
        cardView.FaceUpSprite = HelperMethods.GetRandomSprite();
        yield return null;

        //When
        DataSaver.SaveData(Difficulty.Easy, new Dictionary<int, CardMemeto>{ { 0, cardView.SaveState() } });
        cardView.Selected();
        LevelData data = DataLoader.LoadData();
        cardView.LoadState(data.CardData[0]);

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        Assert.IsTrue(g.GetComponent<Image>().sprite == cardView.FaceDownSprite);
    }
}
