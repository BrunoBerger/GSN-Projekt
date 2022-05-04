using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
enum Strum
{
    up,
    down,
    none
};

public class PlayRythm : MonoBehaviour
{
    [SerializeField]
    AudioSource backgroundMusic;
    [SerializeField]
    AudioSource playerInput;
    [SerializeField]
    GameState gameState;
    [SerializeField]
    AudioClips audioClips;
    public float tactSpeed = 1f;
    Strum[] pattern = { Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.up };
    bool nextTact = false;
    public int beatPos = 0;

    void Start()
    {
        gameState.speed = 1;
        tactSpeed = 1 / gameState.speed;
        nextTact = true;
        beatPos = 0;
    }


    void Update()
    {
        if (nextTact)
        {
            nextTact = false;
            StartCoroutine(tact());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerInput.Stop();
            playerInput.clip = audioClips.chords[gameState.chord].upPlayer;
            playerInput.Play();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerInput.Stop();
            playerInput.clip = audioClips.chords[gameState.chord].downPlayer;
            playerInput.Play();
        }
    }

    IEnumerator tact()
    {
        yield return new WaitForSeconds(tactSpeed / 2);
        if (pattern[beatPos] == Strum.up)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = audioClips.chords[gameState.chord].upMusic;
            backgroundMusic.Play();
        }
        if (pattern[beatPos] == Strum.down)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = audioClips.chords[gameState.chord].downMusic;
            backgroundMusic.Play();
        }
        yield return new WaitForSeconds(tactSpeed / 2);
        nextTact = true;
    }
}
