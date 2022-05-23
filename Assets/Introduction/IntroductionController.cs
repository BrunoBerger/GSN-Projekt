using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IntroductionController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameState gameState;
    [SerializeField] RhythmController rhythmController;
    [SerializeField] RhythmVisualisation rhythmVisualisation;
    [SerializeField] StreetGenerator streetGenerator;
    [SerializeField] GameObject beerGiver;
    [SerializeField] PoliceController policeController;
    [SerializeField] GameObject police;
    [SerializeField] GameObject strumPattern;
    [SerializeField] GameObject beerCounter;
    [SerializeField] GameObject distanceElement;

    ///////////////////////////////////

    bool jumpedLeft = false;
    bool jumpedRight = false;
    float defaultObstacleSpawnRate;
    float defaulScrollSpeedFactor;

    void Start()
    {
        rhythmController.enabled = false;
        rhythmVisualisation.enabled = false;
        defaultObstacleSpawnRate = streetGenerator.ObstacleSpawnRate;
        streetGenerator.ObstacleSpawnRate = 0;
        defaulScrollSpeedFactor = streetGenerator.ScrollSpeedFactor;
        streetGenerator.ScrollSpeedFactor = 1.1f;
        beerGiver.SetActive(false);
        policeController.enabled = false;
        police.SetActive(false);
        strumPattern.SetActive(false);
        beerCounter.SetActive(false);
        distanceElement.SetActive(false);
    }

    void Update()
    {
        if(!rhythmController.enabled && jumpedLeft && jumpedRight){
            EnableRhythm();
        }
    }

    public void DirectionInput(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();

        if(direction.x < 0){
            jumpedLeft = true;
        }
        else if(direction.x > 0){
            jumpedRight = true;
        }
    }

    void EnableRhythm(){
        rhythmController.enabled = true;
        strumPattern.SetActive(true);
        rhythmVisualisation.enabled = true;
    }

    void EnablePolice(){
        police.SetActive(true);
        policeController.enabled = true;
    }
}
