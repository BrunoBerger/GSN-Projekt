using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameState : MonoBehaviour
{
    [SerializeField] GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState.beerCounter = 0;
    }
}
