using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class TurnDisplayTests
{
    [UnityTest]
    public IEnumerator TurnCounterMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        TurnCounterDisplay turn = g.AddComponent<TurnCounterDisplay>();
        yield return null;
        Assert.IsTrue(turn == null);
    }

    [UnityTest]
    public IEnumerator TextFieldMissing()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        TurnCounterDisplay turn = g.AddComponent<TurnCounterDisplay>();
        turn.TurnCounter = new TurnCounter(new CardMatcher());
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
        TurnCounterDisplay turn = g3.AddComponent<TurnCounterDisplay>();
        turn.TurnCounter = new TurnCounter(cardMatcher);
        turn.Text = g3.AddComponent<TextMeshProUGUI>();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When;
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.AreEqual(g3.GetComponent<TextMeshProUGUI>().text, "Turn : 2");
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
        TurnCounterDisplay turn = g3.AddComponent<TurnCounterDisplay>();
        turn.TurnCounter = new TurnCounter(cardMatcher);
        turn.Text = g3.AddComponent<TextMeshProUGUI>();
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When;
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.AreEqual(g3.GetComponent<TextMeshProUGUI>().text, "Turn : 2");
    }
}
