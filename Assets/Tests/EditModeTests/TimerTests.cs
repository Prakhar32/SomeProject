using NUnit.Framework;
using System;
using UnityEngine;

public class TimerTests
{
    [Test]
    public void MonobehaviourCannotBeNull()
    {
        Assert.Throws<NullReferenceException>(() => new Timer(null, null));
    }

    [Test]
    public void DifficultySettorCannotBeNull()
    {
        GameObject g = new GameObject();
        Assert.Throws<NullReferenceException>(() => new Timer(g.AddComponent<TimerDisplay>(), null));
    }
}
