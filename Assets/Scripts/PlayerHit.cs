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

            if (gameState.GetState() == States.Playing)
                gameState.timesHit++;
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            if (gameState.GetState() == States.Playing)
            {
                gameState.totalBeerCount++;
            }

            // gameState.speed += 1;
            other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false; // Doesnt work with keg-prefab
            //other.gameObject.transform.localScale = new Vector3(0, 0, 0);
            // TODO: some sound effect
        }
    }
}
