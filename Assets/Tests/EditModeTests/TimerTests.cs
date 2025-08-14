using NUnit.Framework;
using System;
using UnityEngine;

public class TimerTests
{
    [Test]
    public void MonobehaviourCannotBeNull()
    {
        Assert.Throws<NullReferenceException>(() => new Timer(null));
    }
}
