using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmVisualisation : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    Image[] sprites;
    int beatPos;
    bool nextSprite;
    public float tactSpeed = 1f;
    float slowestTactSpeed;
    void Start()
    {
        beatPos = 0;
        sprites = new Image[transform.childCount];
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = gameObject.transform.GetChild(i).GetComponent<Image>();
        }
        nextSprite = true;
        tactSpeed = 1/gameState.speed;
        slowestTactSpeed = tactSpeed;
    }

    void Update()
    {
        if (nextSprite)
        {
            nextSprite = false;
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
            StartCoroutine(tact(tactSpeed));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (sprites[beatPos].color == Color.white && sprites[beatPos].tag == "Left")
                sprites[beatPos].color = Color.green;
            else
                sprites[beatPos].color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (sprites[beatPos].color == Color.white && sprites[beatPos].tag == "Right")
                sprites[beatPos].color = Color.green;
            else
                sprites[beatPos].color = Color.red;
        }
    }

    IEnumerator tact(float time)
    {
        yield return new WaitForSeconds(time);
        if (beatPos == sprites.Length - 1)
        {
            bool allCorrect = true;
            for (int i = 0; i < sprites.Length; i++)
            {
                if(i == sprites.Length - 1 && sprites[i].color != Color.red && sprites[i].tag == "None")
                    continue;
                if (sprites[i].color != Color.green)
                {
                    allCorrect = false;
                    break;
                }
            }
            beatPos = 0;
            if (allCorrect)
            {
                tactSpeed /= 1.25f;
                gameState.speed *= 1.25f;
            }

            else if (tactSpeed < slowestTactSpeed)
            {
                tactSpeed *= 1.25f;
                gameState.speed /= 1.25f;
            }
        }
        else
            beatPos++;

        nextSprite = true;
    }
}
