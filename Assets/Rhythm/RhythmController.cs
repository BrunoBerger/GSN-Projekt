using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    PlayRhythm playRhythm;
    [SerializeField]
    AudioClips audioClips;
    [SerializeField]
    RhythmVisualisation rhythmVisualisation;
    int beatPos = 0;
    bool nextTact = false;
    float slowestTactSpeed;
    // Start is called before the first frame update
    void Start()
    {
        nextTact = true;
        beatPos = 0;
        slowestTactSpeed = 1 / gameState.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTact)
            StartCoroutine(tact());
    }

    public void play()
    {
        rhythmVisualisation.check(beatPos);
        playRhythm.play(beatPos);
    }

    public void up()
    {
        rhythmVisualisation.check(beatPos);
        playRhythm.up();
    }

    public void down()
    {
        rhythmVisualisation.check(beatPos);
        playRhythm.down();
    }

    public void throwBeer()
    {
        rhythmVisualisation.check(beatPos);
        playRhythm.throwBeer();
    }

    void endOfRhythm()
    {
        if (rhythmVisualisation.rhythmCorrect() && gameState.beerCounter > 0)
        {
            gameState.speed *= 1.25f;
            gameState.beerCounter -= 1;
            gameState.danceRush = 3;
        }
        else if (gameState.danceRush == 0 && 1 / gameState.speed < slowestTactSpeed && gameState.speed > slowestTactSpeed)
        {
            gameState.speed /= 1.25f;
        }
        else if(gameState.danceRush > 0)
        {
            gameState.danceRush -= 1;
        }

        if (gameState.chord < audioClips.chords.Count - 1)
            gameState.chord++;
        else
            gameState.chord = 0;
    }

    IEnumerator tact()
    {
        if(beatPos == 0)
            rhythmVisualisation.resetAllColors();

        nextTact = false;
        rhythmVisualisation.onTact(beatPos);
        yield return new WaitForSeconds(1 / (2 * gameState.speed));
        playRhythm.onTact(beatPos);
        yield return new WaitForSeconds(1 / (2 * gameState.speed));

        if (beatPos == 7)
        {
            beatPos = 0;
            endOfRhythm();
        }
        else
            beatPos++;

        nextTact = true;
    }
}
