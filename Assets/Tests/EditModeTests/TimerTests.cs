using NUnit.Framework;
using System;

public class TimerTests
{
    [Test]
    public void MonobehaviourCannotBeNull()
    {
        Assert.Throws<NullReferenceException>(() => new Timer(null));
    }
}
