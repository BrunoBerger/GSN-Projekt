using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerThrowerPlayer : MonoBehaviour
{
    [SerializeField] GameObject beerPrefab;
    [SerializeField] StreetGenerator streetGenerator;
    [SerializeField] JumpPreset jumpPreset;
    [SerializeField] AnimationStateController animationStateController;

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

    }
}
