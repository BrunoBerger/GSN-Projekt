using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerThrowerPlayer : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] GameObject beerPrefab;
    [SerializeField] StreetGenerator streetGenerator;
    [SerializeField] JumpPreset jumpPreset;
    [SerializeField] AnimationStateController animationStateController;
    [SerializeField] float dropBeerPoliceSetback = 0.05f;

    public void dropBeer()
    {
        StartCoroutine(dropBeerAnimation());
    }

    IEnumerator dropBeerAnimation()
    {
        animationStateController.startDrop();
        yield return new WaitForSeconds(jumpPreset.jumpDuration/2);
        GameObject newBeer = Instantiate(beerPrefab, transform.position, Quaternion.identity);
        newBeer.tag = "None";
        streetGenerator._decorationQueue.Enqueue(newBeer);
        yield return new WaitForSeconds(jumpPreset.jumpDuration / 2);
        animationStateController.stopDrop();
        yield return new WaitForSeconds(MathF.Max(0f, 1 - gameState.police) / gameState.speed);
        gameState.policeChange = -MathF.Abs(gameState.policeChange) - dropBeerPoliceSetback;
    }
}
