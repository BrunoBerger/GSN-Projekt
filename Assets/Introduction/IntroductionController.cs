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
    [SerializeField] GameObject policeIndicator;
    [SerializeField] TMP_Text introductionText;
    [SerializeField] JumpPreset jumpPreset;

    ///////////////////////////////////

    bool jumpedLeft = false;
    bool jumpedRight = false;
    bool throwedBeer = false;
    float defaulScrollSpeedFactor;
    public bool playerHitCar = false;
    bool performedDanceMove = false;
    float countDownTimestamp;

    void Start()
    {
        // rhythmController.enabled = false;
        // rhythmVisualisation.enabled = false;
        strumPattern.SetActive(false);
        distanceElement.SetActive(false);

        streetGenerator.ObstacleSpawnRate = 0;
        defaulScrollSpeedFactor = StreetGenerator.ScrollSpeedFactor;
        // streetGenerator.ScrollSpeedFactor = 1.1f;

        beerGiver.SetActive(false);
        beerCounter.SetActive(false);

        policeIndicator.SetActive(false);
        policeController.enabled = false;
        police.SetActive(false);
        introductionText.text = "PRESS LEFT/RIGHT KEY TO JUMP";
    }

    void Update()
    {
        if(!distanceElement.activeSelf)
            streetGenerator.ObstacleSpawnRate = 0;
        if(performedDanceMove){
            if(countDownTimestamp - Time.time < 0)
                SceneManager.LoadScene(1);
            if(countDownTimestamp - Time.time < 3)
                introductionText.text = "LEVEL WILL START IN " + (int)(countDownTimestamp - Time.time);
            else
                introductionText.text = "YOU DRANK A BEER AND\nINCREASED YOUR SPEED\n";
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
        else if(throwedBeer && rhythmVisualisation.rhythmCorrect() && !performedDanceMove){
            performedDanceMove = true;
            countDownTimestamp = Time.time + 6;
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
        streetGenerator.ObstacleSpawnRate = 0.5f;
        StreetGenerator.ScrollSpeedFactor = defaulScrollSpeedFactor;
        distanceElement.SetActive(true);
        gameState.speed += 0.25f;
        introductionText.text = "IF YOU HIT A CAR\nYOU WILL SLOW DOWN";
    }

    void EnablePolice(){
        policeIndicator.SetActive(true);
        police.SetActive(true);
        policeController.enabled = true;
        introductionText.text = "PRESS DOWN KEY TO THROW BEER\nAND SLOW DOWN THE POLICE";
    }
    
    void EnableRhythm(){
        rhythmVisualisation.resetAllColors();
        rhythmController.enabled = true;
        strumPattern.SetActive(true);
        rhythmVisualisation.enabled = true;
        introductionText.text = "PRESS A KEY ONLY AT HIGHLIGHTED NOTES\n";
    }

    public void SkipIntro(){
        performedDanceMove = true;
        countDownTimestamp = Time.time + 3;
    }
}
