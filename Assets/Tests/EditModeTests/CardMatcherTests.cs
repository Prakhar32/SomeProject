using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardMatcherTests
{
    [Test]
    public void NullCannotSubscribeToSuccessfulMatch()
    {
        CardMatcher matcher = new CardMatcher();
        Assert.Throws<NullReferenceException>(() => matcher.SubscribeToSuccessfulMatch(null));
    }

    [Test]
    public void NullCannotSubscribeToUnsuccessfulmatch()
    {
        CardMatcher matcher = new CardMatcher();
        Assert.Throws<NullReferenceException>(() => matcher.SubscribeToUnsuccessfulMatch(null));
    }

    [Test]
    public void NullCannotUnsubscribefromSuccessfulmatch()
    {
        CardMatcher matcher = new CardMatcher();
        Assert.Throws<NullReferenceException>(() => matcher.UnsubscribefromSuccessfulMatch(null));
    }

    [Test]
    public void NullCannotUnsubscribefromUnsuccessfulmatch()
    {
        CardMatcher matcher = new CardMatcher();
        Assert.Throws<NullReferenceException>(() => matcher.UnsubscribefromUnsuccessfulMatch(null));
    }
}
