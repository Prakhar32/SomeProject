using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class TurnTests
{
    [UnityTest]
    public IEnumerator CardMatcherMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        TurnCounter turn = g.AddComponent<TurnCounter>();
        yield return null;
        Assert.IsTrue(turn == null);
    }

    [UnityTest]
    public IEnumerator TurnIncresesOnUnsuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = new GameObject();
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(g1, cardMatcher);

        GameObject g2 = new GameObject();
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(g2, cardMatcher);
        

        GameObject g3 = new GameObject();
        TurnCounter turn = g3.AddComponent<TurnCounter>();
        turn.Matcher = cardMatcher;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        int initialScore = turn.getTurnCount();
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(initialScore + 1 == turn.getTurnCount());
    }

    [UnityTest]
    public IEnumerator TurnIncresesOnSuccessfulMatch()
    {
        //Given
        CardMatcher cardMatcher = new CardMatcher();
        GameObject g1 = new GameObject();
        CardView cardView1 = HelperMethods.ConvertGameobjectIntoCard(g1, cardMatcher);

        GameObject g2 = new GameObject();
        CardView cardView2 = HelperMethods.ConvertGameobjectIntoCard(g2, cardMatcher);
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;


        GameObject g3 = new GameObject();
        TurnCounter turn = g3.AddComponent<TurnCounter>();
        turn.Matcher = cardMatcher;
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        int initialScore = turn.getTurnCount();
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(initialScore + 1 == turn.getTurnCount());
    }
}
