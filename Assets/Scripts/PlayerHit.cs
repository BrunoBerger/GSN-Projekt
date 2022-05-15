using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] GameState gameState;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (gameState.speed > 1)
                gameState.speed /= 1.25f;
            else
            {
                gameState.speed *= 1.25f;
            }
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
