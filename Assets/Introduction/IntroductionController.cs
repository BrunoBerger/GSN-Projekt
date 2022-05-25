using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] TMP_Text introductionText;
    [SerializeField] JumpPreset jumpPreset;

    ///////////////////////////////////

    bool jumpedLeft = false;
    bool jumpedRight = false;
    bool throwedBeer = false;
    float defaultObstacleSpawnRate;
    float defaulScrollSpeedFactor;
    public bool playerHitCar = false;
    bool performedDanceMove = false;
    float countDownTimestamp;

    void Start()
    {
        rhythmController.enabled = false;
        rhythmVisualisation.enabled = false;
        strumPattern.SetActive(false);
        distanceElement.SetActive(false);

        defaultObstacleSpawnRate = streetGenerator.ObstacleSpawnRate;
        streetGenerator.ObstacleSpawnRate = 0;
        defaulScrollSpeedFactor = StreetGenerator.ScrollSpeedFactor;
        // streetGenerator.ScrollSpeedFactor = 1.1f;

        beerGiver.SetActive(false);
        beerCounter.SetActive(false);

        policeController.enabled = false;
        police.SetActive(false);
        introductionText.text = "PRESS LEFT/RIGHT KEY TO JUMP";
    }

    void Update()
    {
        if(performedDanceMove){
            if(countDownTimestamp - Time.time < 0)
                SceneManager.LoadScene(0);
            introductionText.text = "LEVEL WILL START IN " + (int)(countDownTimestamp - Time.time);
        }
        if(!beerGiver.activeSelf && jumpedLeft && jumpedRight){
            EnableBeerGiver();
        }
        else if(streetGenerator.ObstacleSpawnRate == 0 && gameState.beerCounter > 0){
            EnableStreet();
        }
        else if(!police.activeSelf && playerHitCar) {
            EnablePolice();
        }
        else if(!strumPattern.activeSelf && throwedBeer)
        {
            EnableRhythm();
        }
        else if(throwedBeer && rhythmVisualisation.rhythmCorrect()){
            performedDanceMove = true;
            countDownTimestamp = Time.time + 3;
        }
        jumpPreset.jumpDuration = 1 / gameState.speed / 2;
    }

    public void DirectionInput(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();

        if(direction.x < 0){
            jumpedLeft = true;
        }
        else if(direction.x > 0){
            jumpedRight = true;
        }
        else if(police.activeSelf && !strumPattern.activeSelf && direction.y < 0){
            throwedBeer = true;
        }
    }

    void EnableBeerGiver(){
        beerGiver.SetActive(true);
        beerCounter.SetActive(true);
        introductionText.text = "COLLECT BEER TO\nINCREASE THE BEER COUNTER";
    }

    void EnableStreet(){
        streetGenerator.ObstacleSpawnRate = defaultObstacleSpawnRate;
        StreetGenerator.ScrollSpeedFactor = defaulScrollSpeedFactor;
        distanceElement.SetActive(true);
        gameState.speed += 0.25f;
        introductionText.text = "IF YOU HIT A CAR\nYOU WILL SLOW DOWN";
    }

    void EnablePolice(){
        police.SetActive(true);
        policeController.enabled = true;
        introductionText.text = "PRESS DOWN KEY TO THROW BEER\nAND SLOW DOWN THE POLICE";
    }
    
    void EnableRhythm(){
        rhythmController.enabled = true;
        strumPattern.SetActive(true);
        rhythmVisualisation.enabled = true;
        introductionText.text = "PRESS A KEY AT HIGHLIGHTED NOTES\nDON'T PRESS A KEY AT HIGHLIGHTED POINTS\n\nTO COMSUME BEER AND\nINCREASE YOUR SPEED";
    }
}
