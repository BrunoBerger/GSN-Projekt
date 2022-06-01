using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] RhythmController rhythmController;
    [SerializeField] ShakePreset shakePreset;

    AudioSource slurpSource;

    private void Start()
    {
        slurpSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Shaker.ShakeAll(shakePreset);
            if (gameState.speed > 1)
                rhythmController.getSober();
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            gameState.beersColectedTotal++;
            other.gameObject.transform.localScale = new Vector3(0, 0, 0);
            slurpSource.pitch = Random.Range(1.0f, 1.4f);
            slurpSource.Play();
        }
    }
}
