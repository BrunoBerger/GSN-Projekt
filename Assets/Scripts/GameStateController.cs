using TMPro;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    SaveMetrics saveMetrics;
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
    float _totalDistance = 0;
    // Start is called before the first frame update
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
        gameState.timesHit = 0;
        gameState.runTime = 0;
        gameState.avargeSpeed = 0;
        gameState.distance = 0;
        gameState.inputCount = 0;
        gameState.beersColectedTotal = 0;
        gameState.speedUps = 0;
        gameState.thrownBeer = 0;
        gameState.speedDowns = 0;
        gameState.stateChanged.AddListener(GameEnded);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.GetState() == States.Playing)
            gameState.runTime += Time.deltaTime;

        _totalDistance += gameState.speed * Time.deltaTime * StreetGenerator.ScrollSpeedFactor;
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
        if(gameState.GetState() == States.End)
        {
            gameState.avargeSpeed = _totalDistance / gameState.runTime;
            gameState.distance = _totalDistance;
            saveMetrics.WriteMetrics();
        }

    }
}
