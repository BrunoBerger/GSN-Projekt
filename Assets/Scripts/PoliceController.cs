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
    Slider speedSlider;
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
        UpdatePolice(MathF.Max(gameState.police + (Time.fixedDeltaTime * gameState.policeChange), 0.0f),
            MathF.Min(
                gameState.policeChange + (Time.fixedDeltaTime / worstPoliceCatchupSeconds / policeAccelerationSeconds),
                (1 / worstPoliceCatchupSeconds) / gameState.speed));
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
            speedSlider.value = 0.5f;
        }
        else
        {
            positionSlider.value = Math.Min(1.0f, Math.Max(0.0f, gameState.police));
            speedSlider.value =
                (Math.Min(1.0f, Math.Max(-1.0f, gameState.policeChange * worstPoliceCatchupSeconds)) / 2.0f) + 0.5f;
        }
    }
}
