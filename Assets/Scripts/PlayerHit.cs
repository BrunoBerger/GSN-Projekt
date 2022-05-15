using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] RhythmController rhythmController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (gameState.speed > 1)
                rhythmController.getSober();
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            // gameState.speed += 1;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            // TODO: some sound effect
        }
    }
}
