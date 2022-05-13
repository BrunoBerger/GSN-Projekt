using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmVisualisation : MonoBehaviour
{
    Image[] sprites;
    void Start()
    {
        sprites = new Image[transform.childCount];
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = gameObject.transform.GetChild(i).GetComponent<Image>();
        }

    }

    public void resetAllColors()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.black;
        }
    }

    public void jump(int beatPos)
    {
        if (sprites[beatPos].color == Color.white && sprites[beatPos].tag != "None")
            sprites[beatPos].color = Color.green;
        else
            sprites[beatPos].color = Color.red;
    }

    public void down(int beatPos)
    {
        if (sprites[beatPos].color == Color.white && sprites[beatPos].tag != "None")
            sprites[beatPos].color = Color.green;
        else
            sprites[beatPos].color = Color.red;
    }

    public void onTact(int beatPos)
    {
        Image lastSprite;
        if (beatPos == 0)
            lastSprite = sprites[sprites.Length - 1];
        else
            lastSprite = sprites[beatPos - 1];

        if (lastSprite.tag == "None" && lastSprite.color == Color.white)
            lastSprite.color = Color.green;
        else if (lastSprite.color != Color.green)
            lastSprite.color = Color.black;
        sprites[beatPos].color = Color.white;
    }

    public bool rhythmCorrect()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (i == sprites.Length - 1 && sprites[i].color != Color.red && sprites[i].tag == "None")
                continue;
            if (sprites[i].color != Color.green)
            {
                return false;
            }
        }
        return true;
    }
}
