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
        GameObject g = new GameObject();
        CardView cardView = HelperMethods.ConvertGameobjectIntoCard(g);
        cardView.CardMatcher = new CardMatcher();
        cardView.FaceUpSprite = HelperMethods.GetRandomSpriteFromAtlas();
        yield return null;

        //When
        DataSaver.SaveData(Difficulty.Easy, new List<CardMemeto> { cardView.SaveState() });
        cardView.Selected();
        LevelData data = DataLoader.LoadData();
        cardView.LoadState(data.CardData[0]);

        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);
        Assert.IsTrue(g.GetComponent<Image>().sprite == cardView.FaceDownSprite);
    }
}
