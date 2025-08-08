using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreDisplayTests
{
    [UnityTest]
    public IEnumerator ScoreCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ScoreDisplay scoreDisplay = g.AddComponent<ScoreDisplay>();
        yield return null;
        
        Assert.IsTrue(scoreDisplay == null);
    }

    [UnityTest]
    public IEnumerator TextMeshProComponentMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ScoreDisplay scoreDisplay = g.AddComponent<ScoreDisplay>();
        scoreDisplay.SetScore(new Score(new CardMatcher()));
        yield return null;

        Assert.IsTrue(scoreDisplay == null);
    }

    [UnityTest]
    public IEnumerator ScoreIncreasesWhenCardIsMatched()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = HelperMethods.GetCard(cardMatcher);
        CardView cardView1 = g1.GetComponent<CardView>();

        GameObject g2 = HelperMethods.GetCard(cardMatcher);
        CardView cardView2 = g2.GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;


        GameObject g3 = new GameObject(); 
        g3.AddComponent<TextMeshProUGUI>();
        ScoreDisplay score = g3.AddComponent<ScoreDisplay>();
        score.SetScore(new Score(cardMatcher));
        
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(g3.GetComponent<TextMeshProUGUI>().text.Equals("Score : 1"));
    }
}
