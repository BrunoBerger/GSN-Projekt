using System;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    ThePoLice policePrefab;
    [SerializeField]
    Canvas endScreenCanvas;
    [SerializeField]
    float worstPoliceCatchupSeconds = 60;
    [SerializeField]
    float earliestPoliceEncounter = 20;
    [SerializeField]
    float policeAccelerationSeconds = 7.5f;
    [SerializeField]
    Vector3 sirenNear = new(0f, 0.7f, -3.84f);
    [SerializeField]
    Vector3 sirenFar = new(0f, 0.7f, -16f);
    [SerializeField]
    float sirenLightItensityMax = 1.4f;
    [SerializeField]
    float sirenLightItensityMin = 0.2f;

    private float _cooldownTime;
    
    void Awake()
    {
        Cooldown(earliestPoliceEncounter);
    }

    void FixedUpdate()
    {
        if (Time.fixedTime < _cooldownTime) return;
        gameState.policeChange = MathF.Min(
            gameState.policeChange + (Time.fixedDeltaTime / worstPoliceCatchupSeconds / policeAccelerationSeconds),
            (1 / worstPoliceCatchupSeconds) / gameState.speed);
        gameState.police = MathF.Max(gameState.police + (Time.fixedDeltaTime * gameState.policeChange), 0.0f);
        if (gameState.police >= 1.0f)
        {
            if (!endScreenCanvas.isActiveAndEnabled)
            {
                // Game Over
                endScreenCanvas.gameObject.SetActive(true);
                gameState.speed = 0f;
                gameState.SetState(States.End);
            }
        }
        else
        {
            policePrefab.lightIntensity = Mathf.Lerp(sirenLightItensityMin, sirenLightItensityMax, gameState.police);
            policePrefab.transform.localPosition = Vector3.LerpUnclamped(sirenFar, sirenNear, gameState.police);
        }
    }

    public void Cooldown(float timeSec)
    {
        _cooldownTime = Time.fixedTime + earliestPoliceEncounter;
        policePrefab.lightIntensity = 0.0f;
        gameState.police = 0.0f;
        gameState.policeChange = 1 / worstPoliceCatchupSeconds;
    }
}
