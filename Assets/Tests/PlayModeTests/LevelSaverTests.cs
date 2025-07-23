using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelSaverTests
{
    [UnityTest]
    public IEnumerator ArrangementParentNull()
    {
        LogAssert.ignoreFailingMessages = true;
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        yield return null;
        
        Assert.IsTrue(levelSaver == null);
    }

    [UnityTest]
    public IEnumerator SaveSuccessful()
    {
        //Given
        File.Delete(Constants.FilePath);
        
        GameObject g = new GameObject();
        LevelSaver levelSaver = g.AddComponent<LevelSaver>();
        levelSaver.SetDifficulty(Difficulty.Easy);

        ArrangementGenerator arrangementGenerator = HelperMethods.GetArrangementGenerator();
        levelSaver.ArrangementParent = arrangementGenerator.ArrangementParent;
        yield return null;

        arrangementGenerator.GenerateArrangement(Difficulty.Easy);
        yield return null;

        //When
        levelSaver.SaveLevel();
        yield return null;

        //Then
       Assert.IsTrue(File.Exists(Constants.FilePath));
    }
}
