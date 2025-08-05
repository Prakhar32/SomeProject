using System.Collections.Generic;
using UnityEngine;

public class HelperMethods
{
    public static List<Sprite> GetSprites()
    {
        List<Sprite> sprites = new List<Sprite>();
        Sprite[] sp = Resources.LoadAll<Sprite>("Sprites");
        sprites.AddRange(sp);
        return sprites;
    }

    public static Sprite GetRandomSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");
        return sprites[Random.Range(0, sprites.Length)];
    }

    public static GameObject GetCard(CardMatcher cardMatcher)
    {
        GameObject card = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
        CardView cardView = card.GetComponent<CardView>();
        cardView.CardMatcher = cardMatcher;
        cardView.FaceUpSprite = GetRandomSprite();
        return card;
    }
}
