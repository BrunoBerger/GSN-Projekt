using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Preset", menuName = "ScriptableObjects/Jump Preset")]
public class JumpPreset : ScriptableObject
{
    public float jumpDuration = 1;
    public AnimationCurve verticalMovement;
    public AnimationCurve horizontalMovement;
    public Vector3 offsetOffLane;
}
