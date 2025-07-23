using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class HelperMethods
{
    private static Texture2D createTexture()
    {
        var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        texture.SetPixel(1, 0, Color.clear);
        texture.SetPixel(0, 1, Color.white);
        texture.SetPixel(1, 1, Color.black);

        texture.Apply();
        return texture;
    }

    public static Sprite createSpriteStub()
    {
        Sprite sprite = Sprite.Create(createTexture(), new Rect(0, 0, 2, 2), Vector2.zero);
        return sprite;
    }

    public static CardView ConvertGameobjectIntoCard(GameObject g, CardMatcher cardMatcher)
    {
        CardView card = g.AddComponent<CardView>();
        card.FaceUpSprite = createSpriteStub();
        card.FaceDownSprite = createSpriteStub();
        card.CardMatcher = cardMatcher;

        g.AddComponent<Image>();
        g.AddComponent<Button>();
        return card;
    }

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

    public static ArrangementGenerator GetArrangementGenerator()
    {
        GameObject g = new GameObject();
        GameObject arrangementParent = new GameObject();

        ArrangementGenerator arrangementGenerator = g.AddComponent<ArrangementGenerator>();
        arrangementGenerator.LayoutGroup = arrangementParent.AddComponent<GridLayoutGroup>();
        arrangementGenerator.ArrangementParent = arrangementParent.transform;
        arrangementGenerator.CardMatcher = new CardMatcher();

        arrangementGenerator.CardPrefab = new GameObject();
        ConvertGameobjectIntoCard(arrangementGenerator.CardPrefab, arrangementGenerator.CardMatcher);

        arrangementGenerator.CardSprites = GetSprites();
        arrangementGenerator.FaceDownSprite = createSpriteStub();
        return arrangementGenerator;
    }
}
