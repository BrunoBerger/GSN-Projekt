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

    [SerializeField] JumpPreset jumpPreset;
    int beatPos = 0;
    bool nextTact = false;
    float slowestTactSpeed;

    Camera cam;
    float normalFOV = 60f;

    // Start is called before the first frame update
    void Start()
    {
        nextTact = true;
        beatPos = 0;
        slowestTactSpeed = 1 / gameState.speed;
        cam = Camera.main;
        cam.fieldOfView = normalFOV;
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
        if (gameState.chord < audioClips.chords.Count - 1)
            gameState.chord++;
        else
            gameState.chord = 0;
    }

    public void drinkBeer()
    {
        gameState.speed += 0.25f;
        gameState.beerCounter -= 1;
        jumpPreset.jumpDuration = 1 / gameState.speed / 2;
        StartCoroutine(speedEffect());
    }

    public void getSober()
    {
        gameState.speed -= 0.25f;
        jumpPreset.jumpDuration = 1 / gameState.speed / 2;
    }

    void onTact()
    {
        if (rhythmVisualisation.rhythmCorrect() && gameState.beerCounter > 0)
        {
            drinkBeer();
            rhythmVisualisation.resetAllColors();
        }
    }

    IEnumerator tact()
    {
        nextTact = false;

        onTact();

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

    IEnumerator speedEffect()
    {
        float fov_transition = 0f;
        float effectFOV = normalFOV + 10f;
        //Vector3 offset = new Vector3(0, 0, 0.2f);
        //Vector3 relativeCamPos = transform.position - cam.transform.position;

        while (fov_transition <= 1)
        {
            cam.fieldOfView = Mathf.Lerp(normalFOV, effectFOV, fov_transition);
            //cam.transform.position = Vector3.Lerp(
            //    cam.transform.position, 
            //    cam.transform.position + offset, 
            //    fov_transition
            //);

            fov_transition += Time.deltaTime * 6;
            yield return null;
        }
        fov_transition = 0f;
        while (fov_transition <= 1)
        {
            cam.fieldOfView = Mathf.Lerp(effectFOV, normalFOV, fov_transition);
            //cam.transform.position = Vector3.Lerp(
            //    cam.transform.position - offset, 
            //    cam.transform.position, 
            //    fov_transition
            //);

            fov_transition += Time.deltaTime * 1f;
            yield return null;
        }
        //cam.fieldOfView = normalFOV;
        //cam.transform.position = transform.position + relativeCamPos;
    }
}
