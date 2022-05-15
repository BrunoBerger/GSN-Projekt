using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerThrowerPlayer : MonoBehaviour
{
    [SerializeField] GameObject beerPrefab;
    [SerializeField] StreetGenerator streetGenerator;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void dropBeer()
    {
        GameObject newBeer = Instantiate(beerPrefab, transform.position, Quaternion.identity);
        newBeer.tag = "None";
        streetGenerator._decorationQueue.Enqueue(newBeer);
    }
}
