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
    public IEnumerator TextFieldCannotBeNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        ScoreDisplay scoreDisplay = g.AddComponent<ScoreDisplay>();
        scoreDisplay.score = new Score(new CardMatcher());
        yield return null;

        Assert.IsTrue(scoreDisplay == null);
    }

    [UnityTest]
    public IEnumerator ScoreIncreasesWhenCardIsMatched()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = new GameObject();
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(g1, cardMatcher);

        GameObject g2 = new GameObject();
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(g2, cardMatcher);
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;


        GameObject g3 = new GameObject();
        ScoreDisplay score = g3.AddComponent<ScoreDisplay>();
        score.score = new Score(cardMatcher);
        score.Text = g3.AddComponent<TextMeshProUGUI>();

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
