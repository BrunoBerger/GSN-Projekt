using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] RhythmController rhythmController;
    [SerializeField] ShakePreset shakePreset;
    [SerializeField] AudioSource carHitSound;

    

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Shaker.ShakeAll(shakePreset);
            if (gameState.speed > 1)
                rhythmController.getSober();
            gameState.policeChange += 0.02f * gameState.speed;
            carHitSound.Play();
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            gameState.beersColectedTotal++;
            other.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
