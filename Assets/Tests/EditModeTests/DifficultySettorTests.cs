using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

public class DifficultySettorTests
{
    [Test]
    public void CanSetDifficulty()
    {
        GameObject gameObject = new GameObject();
        DifficultySettor difficultySettor = gameObject.AddComponent<DifficultySettor>();
        
        difficultySettor.SetDifficulty(0);
        Assert.IsTrue(difficultySettor.GetDifficulty() == Difficulty.Easy);
    }

    [Test]
    public void CannotSetInvalidDifficulty()
    {
        GameObject gameObject = new GameObject();
        DifficultySettor difficultySettor = gameObject.AddComponent<DifficultySettor>();

        Assert.Throws<InvalidOperationException>(() => difficultySettor.SetDifficulty(3));
    }

    [Test]
    public void NullCannotSubscribeToDifficultyChange()
    {
        GameObject gameObject = new GameObject();
        DifficultySettor difficultySettor = gameObject.AddComponent<DifficultySettor>();
        Assert.Throws<NullReferenceException>(() => difficultySettor.SubscribeToDifficultyChange(null));        
    }

    [Test]
    public void CanSubscribeToDifficultyChange()
    {
        GameObject gameObject = new GameObject();
        DifficultySettor difficultySettor = gameObject.AddComponent<DifficultySettor>();
        bool changed = false;
        difficultySettor.SubscribeToDifficultyChange(() => changed = true);
        difficultySettor.SetDifficulty(1);
        Assert.IsTrue(changed);
    }

    [Test]
    public void CanUnsubscribeFromDifficultyChange()
    {
        GameObject gameObject = new GameObject();
        DifficultySettor difficultySettor = gameObject.AddComponent<DifficultySettor>();
        bool changed = false;
        UnityAction action = () => changed = true;
        
        difficultySettor.SubscribeToDifficultyChange(action);
        difficultySettor.UnsubscribeFromDifficultyChange(action);
        difficultySettor.SetDifficulty(0);
        Assert.IsFalse(changed);
    }
}
