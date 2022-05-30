using TMPro;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    JumpPreset jumpPreset;
    [SerializeField]
    TMP_Text speed;
    [SerializeField]
    TMP_Text beerCounter;
    [SerializeField]
    TMP_Text danceRush;
    [SerializeField]
    GameObject danceRushVisuals;
    [SerializeField]
    AnimationStateController animationStateController;

    //Endscreen
    [SerializeField] TMP_Text distanceEnd;
    [SerializeField] TMP_Text beersEnd;
    [SerializeField] TMP_Text comboEnd;


    void Awake()
    {
        gameState.chord = 0;
        gameState.speed = 1;
        speed.text = "1";
        gameState.beerCounter = 0;
        beerCounter.text = "0";
        gameState.danceRush = 0;
        danceRush.text = "0";
        danceRush.enabled = false;
        danceRushVisuals.SetActive(false);
        jumpPreset.jumpDuration = 0.5f;
        gameState.police = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.speed.ToString("F2") != speed.text)
        {
            speed.text = gameState.speed.ToString("F2");
        }
        if(gameState.beerCounter != int.Parse(beerCounter.text))
        {
            beerCounter.text = gameState.beerCounter.ToString();
        }
        if(gameState.danceRush != int.Parse(danceRush.text) && gameState.danceRush > 0)
        {
            danceRush.text = gameState.danceRush.ToString();
            danceRush.enabled = true;
            danceRushVisuals.SetActive(true);
        }
        else if(gameState.danceRush == 0 && danceRush.enabled)
        {
            danceRush.enabled = false;
            danceRushVisuals.SetActive(false);
        }
    }

    private void GameEnded()
    {
        animationStateController.die();
        distanceEnd.text = _totalDistance.ToString("F0");
        beersEnd.text = gameState.beersColectedTotal.ToString();
        comboEnd.text = gameState.maxCombo.ToString();
    }
}
