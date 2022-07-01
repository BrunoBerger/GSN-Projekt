using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveMetrics : MonoBehaviour
{
    [SerializeField] GameState gameState;
    string path;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.isEditor) 
            path = Application.streamingAssetsPath;
        else
            path = Application.persistentDataPath;

        if(!File.Exists(path + "/metrics.csv")) 
            File.WriteAllText(path + "/metrics.csv", "Distance;Beer Counter;Beer Collected in Total;Times Hit;Run Time;Average Speed;Input Count;Speed Ups;Speed Downs;Thrown Beer\n");
    }

    public void WriteMetrics() 
    {
        File.AppendAllText(path + "/metrics.csv", gameState.distance + ";" + gameState.beerCounter + ";" + gameState.beersColectedTotal + ";" + gameState.timesHit + ";" + gameState.runTime + ";" + gameState.avargeSpeed + ";" +gameState.inputCount + ";" + gameState.speedUps + ";" + gameState.speedDowns + ";" + gameState.thrownBeer + "\n");
    }
}
