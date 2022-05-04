using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmVisualisation : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    PlayRythm playRythm;
    [SerializeField]
    AudioClips audioClips;
    Image[] sprites;
    bool nextSprite;
    float slowestTactSpeed;
    void Start()
    {
        sprites = new Image[transform.childCount];
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = gameObject.transform.GetChild(i).GetComponent<Image>();
        }
        nextSprite = true;
        slowestTactSpeed = 1/gameState.speed;
    }

    void Update()
    {
        if (nextSprite)
        {
            nextSprite = false;
            Image lastSprite;
            if (playRythm.beatPos == 0)
                lastSprite = sprites[sprites.Length - 1];
            else
                lastSprite = sprites[playRythm.beatPos - 1];

            if (lastSprite.tag == "None" && lastSprite.color == Color.white)
                lastSprite.color = Color.green;
            else if (lastSprite.color != Color.green)
                lastSprite.color = Color.black;
            sprites[playRythm.beatPos].color = Color.white;
            StartCoroutine(tact(playRythm.tactSpeed));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (sprites[playRythm.beatPos].color == Color.white && sprites[playRythm.beatPos].tag == "Left")
                sprites[playRythm.beatPos].color = Color.green;
            else
                sprites[playRythm.beatPos].color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (sprites[playRythm.beatPos].color == Color.white && sprites[playRythm.beatPos].tag == "Right")
                sprites[playRythm.beatPos].color = Color.green;
            else
                sprites[playRythm.beatPos].color = Color.red;
        }
    }

    IEnumerator tact(float time)
    {
        yield return new WaitForSeconds(time);
        if (playRythm.beatPos == sprites.Length - 1)
        {
            bool allCorrect = true;
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i == sprites.Length - 1 && sprites[i].color != Color.red && sprites[i].tag == "None")
                    continue;
                if (sprites[i].color != Color.green)
                {
                    allCorrect = false;
                    break;
                }
            }
            playRythm.beatPos = 0;
            if (allCorrect)
            {
                playRythm.tactSpeed /= 1.25f;
                gameState.speed *= 1.25f;
            }

            else if (playRythm.tactSpeed < slowestTactSpeed)
            {
                playRythm.tactSpeed *= 1.25f;
                gameState.speed /= 1.25f;
            }
            if (gameState.chord < audioClips.chords.Count - 1)
                gameState.chord++;
            else
                gameState.chord = 0;
        }
        else
            playRythm.beatPos++;

        nextSprite = true;
    }
}
