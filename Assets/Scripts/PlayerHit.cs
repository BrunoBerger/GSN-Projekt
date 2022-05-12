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
            if (gameState.speed > 2)
                gameState.speed -= 1;
            else
            {
                gameState.speed = 1;
            }
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            gameState.speed += 1;
        }
    }
}
