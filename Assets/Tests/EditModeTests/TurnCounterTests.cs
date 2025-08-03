using NUnit.Framework;
using System;

public class TurnCounterTests
{
    [Test]
    public void NullCannotSubscribed()
    {
        TurnCounter turn = new TurnCounter(new CardMatcher());
        Assert.Throws<NullReferenceException>(() => turn.SubscribeToTurnChange(null));
    }

    [Test]
    public void NullCannotBeUnsubscribed()
    {
        TurnCounter turn = new TurnCounter(new CardMatcher());
        Assert.Throws<NullReferenceException>(() => turn.UnsubscribeToTurnChange(null));
    }
}
