using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    TMP_Text speed;
    [SerializeField]
    TMP_Text beerCounter;
    [SerializeField]
    TMP_Text danceRush;
    [SerializeField]
    GameObject danceRushVisuals;
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
}
