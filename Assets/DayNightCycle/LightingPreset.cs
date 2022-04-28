using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lighting Preset", menuName = "ScriptableObjects/Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient DirectionalColor;
    public Gradient AmbientColor;
}
