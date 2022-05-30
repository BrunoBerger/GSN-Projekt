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
    AudioSource policeAudio;
    [SerializeField]
    GameState gameState;
    [SerializeField]
    AudioClips audioClips;
    Strum[] pattern = { Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.none, Strum.down, Strum.up };

    bool fadeOutPolice = false, fadeInPolice = false;
    public float transitionTime = 1;
    float defaultPoliceVolume;

    void Start(){
        defaultPoliceVolume = policeAudio.volume;
    }

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
        StartCoroutine(swapPoliceClip());
        
    }

    IEnumerator swapPoliceClip(){
        AudioClip audioClip;
        if(policeAudio.clip == audioClips.chords[gameState.chord].downPolice) {
            audioClip = audioClips.chords[gameState.chord].upPolice;
        }
        else
        {
            audioClip = audioClips.chords[gameState.chord].downPolice;
        }

        yield return new WaitForSeconds((1 / gameState.speed) / 2 + 0.1f);

        fadeOutPolice = true;
        transitionTime = (1 / gameState.speed) / 10;
        yield return new WaitForSeconds(transitionTime);
        fadeOutPolice = false;

        policeAudio.Stop();
        policeAudio.clip = audioClip;

        fadeInPolice = true;
        transitionTime = (1 / gameState.speed) / 10;
        yield return new WaitForSeconds(transitionTime);
        fadeInPolice = false;
        policeAudio.volume = gameState.police * defaultPoliceVolume;
        policeAudio.Play();
    }

    void Update(){
        if(fadeOutPolice)
        {
            policeAudio.volume -= (Time.deltaTime / transitionTime) * gameState.police * defaultPoliceVolume;
        }
        else if(fadeInPolice)
        {
            policeAudio.volume += (Time.deltaTime / transitionTime) * gameState.police * defaultPoliceVolume;
        }
        else {
            policeAudio.volume = Mathf.Min(gameState.police * defaultPoliceVolume, defaultPoliceVolume);
        }
    }
}
