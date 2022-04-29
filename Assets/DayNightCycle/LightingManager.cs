using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// "Inspiered" by https://github.com/Glynn-Taylor/Tutorials
// -> https://www.youtube.com/watch?v=m9hj9PdO328

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{

    [SerializeField] Light DirectionalLight = null;
    [SerializeField] LightingPreset preset = null;

    [SerializeField, Range(0, 24)] float TimeOfDay = 12;
    [SerializeField] float dayNightSpeed = 1;

    void OnValidate()
    {
        if (preset == null || DirectionalLight == null)
        {
            Debug.LogWarning("Lighting References not set!");
            enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * dayNightSpeed;
            TimeOfDay %= 24;
        }
        float timePercent = TimeOfDay / 24f;
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
        DirectionalLight.transform.rotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 60f, 0f, 0f));

        //DirectionalLight.transform.l = DirectionalLight.transform.rotation(0, timePercent * 1f, 0, Space.Self);

    }
}
