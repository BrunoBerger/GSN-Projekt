using System;
using UnityEngine;
using UnityEngine.UI;

public class PoliceController : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    ThePoLice policePrefab;
    [SerializeField]
    Canvas endScreenCanvas;
    [SerializeField]
    Slider positionSlider;
    [SerializeField]
    PoliceSpeedIndicator speedIndicator;
    [SerializeField]
    float worstPoliceCatchupSeconds = 60;
    [SerializeField]
    float earliestPoliceEncounter = 20;
    [SerializeField]
    float policeAccelerationSeconds = 7.5f;
    [SerializeField]
    float hitPenaltySeconds = 10f;
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
        var targetSpeed = 1 / worstPoliceCatchupSeconds / Mathf.Min(gameState.speed, 2.0f);
        float newPoliceChange;
        if (gameState.policeChange <= targetSpeed)
        {
            newPoliceChange = Mathf.Min(
                gameState.policeChange + (Time.fixedDeltaTime / worstPoliceCatchupSeconds / policeAccelerationSeconds),
                targetSpeed);
        }
        else
        {
            newPoliceChange = Mathf.Max(
                gameState.policeChange - (Time.fixedDeltaTime / worstPoliceCatchupSeconds / hitPenaltySeconds),
                targetSpeed);
        }
        UpdatePolice(MathF.Max(gameState.police + (Time.fixedDeltaTime * gameState.policeChange), 0.0f), newPoliceChange);
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
        UpdatePolice(0.0f, 1 / worstPoliceCatchupSeconds);
    }

    private void UpdatePolice(float relativePosition, float relativeSpeed)
    {
        gameState.police = relativePosition;
        gameState.policeChange = relativeSpeed;

        if (Time.fixedTime < _cooldownTime)
        {
            positionSlider.value = 0.0f;
            speedIndicator.SetValue(0.0f);
        }
        else
        {
            positionSlider.value = Math.Min(1.0f, Math.Max(0.0f, gameState.police));
            var relativeSpeedNorm = Math.Min(1.0f, Math.Max(-1.0f, gameState.policeChange * worstPoliceCatchupSeconds));
            speedIndicator.SetValue(relativeSpeedNorm * Mathf.Abs(relativeSpeedNorm));
        }
    }
}
