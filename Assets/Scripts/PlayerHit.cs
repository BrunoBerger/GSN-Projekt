using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] RhythmController rhythmController;
    [SerializeField] ShakePreset shakePreset;

    Camera cam;
    float normalFOV = 60f;



    private void Start()
    {
        cam = Camera.main;
        cam.fieldOfView = normalFOV;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            //shakeInstance.Start(0.2f);
            Shaker.ShakeAll(shakePreset);
            if (gameState.speed > 1)
                rhythmController.getSober();
        }
        if (other.gameObject.tag == "Collectable")
        {
            gameState.beerCounter++;
            gameState.beersColectedTotal++;
            // gameState.speed += 1;
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false; // Doesnt work with keg-prefab
            other.gameObject.transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(speedEffect());
            // TODO: some sound effect
        }
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
