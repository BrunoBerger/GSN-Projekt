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

public class PlayRhythm : MonoBehaviour
{
    [SerializeField]
    AudioSource backgroundMusic;
    [SerializeField]
    AudioSource playerInput;
    [SerializeField]
    GameState gameState;
    [SerializeField]
    AudioClips audioClips;
    Strum[] pattern = { Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.up };

    public void play(int beatPos)
    {
        playerInput.Stop();
        if (pattern[beatPos] == Strum.up)
        {
            playerInput.clip = audioClips.chords[gameState.chord].upPlayer;
        }
        else 
        {
            playerInput.clip = audioClips.chords[gameState.chord].downPlayer;
        }
        playerInput.Play();
    }

    public void up()
    {
        playerInput.Stop();
        playerInput.clip = audioClips.chords[gameState.chord].upPlayer;
        playerInput.Play();
    }

    public void down()
    {
        playerInput.Stop();
        playerInput.clip = audioClips.chords[gameState.chord].downPlayer;
        playerInput.Play();
    }

    public void throwBeer()
    {
        playerInput.Stop();
        playerInput.clip = audioClips.chords[gameState.chord].throwPlayer;
        playerInput.Play();
    }

    public void onTact(int beatPos)
    {
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
    }
}
