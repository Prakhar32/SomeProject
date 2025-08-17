using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SystemTests
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        bool sceneLoaded = false;
        SceneManager.sceneLoaded += (scene, mode) => { sceneLoaded = true; };
        SceneManager.LoadScene(Constants.GammeSceneName);
        yield return new WaitUntil(() => sceneLoaded);
    }

    [UnityTest]
    public IEnumerator StartLevelOnDifficultySelection()
    {
        //Given
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();

        //When
        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;

        //Then
        Assert.IsTrue(arrangementGenerator.transform.childCount > 0);
    }

    [UnityTest]
    public IEnumerator MatchedAudioPlayedWhenMatched()
    {
        //Given
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();

        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;

        CardView cardView1 = arrangementGenerator.transform.GetChild(0).GetComponent<CardView>();
        CardView cardView2 = arrangementGenerator.transform.GetChild(1).GetComponent<CardView>();
        cardView1.FaceUpSprite = cardView2.FaceUpSprite;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(audioSource.isPlaying);
        Assert.IsTrue(audioSource.clip == Resources.Load<AudioClip>(Constants.PathToAudio + Constants.CorrectMatchAudio));
    }

    [UnityTest]
    public IEnumerator IncorrectAudioPlayedWhenNotMatched()
    {
        //Given
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();

        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;

        CardView cardView1 = arrangementGenerator.transform.GetChild(0).GetComponent<CardView>();
        CardView cardView2 = arrangementGenerator.transform.GetChild(1).GetComponent<CardView>();
        cardView1.FaceUpSprite = null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        cardView1.Selected();
        cardView2.Selected();
        yield return null;

        //Then
        Assert.IsTrue(audioSource.isPlaying);
        Assert.IsTrue(audioSource.clip == Resources.Load<AudioClip>(Constants.PathToAudio + Constants.IncorrectMatchAudio));
    }



    [UnityTest]
    public IEnumerator GameWonWhenNoCardsLeft()
    {
        //Given
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        ArrangementGenerator arrangementGenerator = GameObject.FindObjectOfType<ArrangementGenerator>();
        AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();

        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        List<CardView> cards = getAllCards(arrangementGenerator);
        setAllCardsSame(cards);
        selectAll(cards);
        yield return null;

        //Then
        Assert.IsTrue(audioSource.isPlaying);
        Assert.IsTrue(audioSource.clip == Resources.Load<AudioClip>(Constants.PathToAudio + Constants.WinAudio));
    }

    private List<CardView> getAllCards(ArrangementGenerator generator)
    {
        List<CardView> cards = new List<CardView>();
        for(int i = 0; i < generator.ArrangementParent.childCount; i++)
        {
            cards.Add(generator.ArrangementParent.GetChild(i).GetComponent<CardView>());
        }
        return cards;
    }

    private void setAllCardsSame(List<CardView> cards)
    {
        for (int i = 1; i < cards.Count; i++)
            cards[i].FaceUpSprite = cards[0].FaceUpSprite;
    }

    private void selectAll(List<CardView> cards)
    {
        for(int i = 0; i < cards.Count; i += 2)
        {
            cards[i].Selected();
            cards[i + 1].Selected();
        }
    }


    [UnityTest]
    public IEnumerator GameOverWhenTimerOut()
    {
        DifficultySettor difficultySettor = GameObject.FindObjectOfType<DifficultySettor>();
        AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();

        difficultySettor.SetDifficulty((int)Difficulty.Easy);
        yield return null;
        yield return new WaitForSeconds(Constants.ViewTime);

        //When
        Time.timeScale = 30;
        yield return new WaitForSeconds(30);

        //Then
        Assert.IsTrue(audioSource.isPlaying);
        Assert.IsTrue(audioSource.clip == Resources.Load<AudioClip>(Constants.PathToAudio + Constants.GameOverAudio));
        Time.timeScale = 1;
    }
}
