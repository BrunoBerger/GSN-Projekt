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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
