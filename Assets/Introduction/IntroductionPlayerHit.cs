using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionPlayerHit : MonoBehaviour
{
    [SerializeField] IntroductionController introductionController;
    [SerializeField] 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            introductionController.playerHitCar = true;
        }
    }
}
