using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreTests
{
    [UnityTest]
    public IEnumerator CardMatcherMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        Score score = g.AddComponent<Score>();
        yield return null;
        Assert.IsTrue(score == null);
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
        Score score = g3.AddComponent<Score>();
        score.CardMatcher = cardMatcher;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        int initialScore = score.getScore();
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(initialScore + 1 == score.getScore());
    }
}
